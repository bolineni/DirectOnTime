// Copyright (c) Direct General Corporation. All rights reserved.
//  
// Reproduction or transmission in whole or in part, in any form or 
// by any means, electronic, mechanical or otherwise, is prohibited 
// without the prior written consent of the copyright owner.
//  
// SolutionName : DirectOnTime
// Project/Filename: DirectOnTime.ProcessCoordinator / Program.cs
// Created By (Date): Shibu K. Raj - {pgskr} (20/09/2011 10:00 AM)
// Last Modified By (Date) : Shibu K. Raj - {pgskr} (20/09/2011 10:00 AM)

namespace DirectOnTime.ProcessCoordinator {
    using System;
    using System.Diagnostics;
    using System.IO;
    using MassTransit.Saga;
    using StructureMap.Pipeline;
    using log4net.Config;
    using Magnum;
    using Magnum.StateMachine;
    using Topshelf;
    using StructureMap;
    using MassTransit;


    internal static class Program {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// <param name="args"></param>
        [STAThread]
        private static void Main(string[] args) {

            XmlConfigurator.ConfigureAndWatch(new FileInfo(AppDomain.CurrentDomain.BaseDirectory + "processcoordinator.log4net.xml"));
            var container = BootStrapContainer();
            HostFactory.Run(c => {
                c.SetServiceName("DirectOnTime.ProcessCoordinator");
                c.SetDisplayName("Direct OnTime - Process Corordinator");
                c.SetDescription("Process Coordinator for the Real Time Policy Integration System.");
                c.RunAsLocalSystem();
                //c.AddDependency()
                DisplayStateMachine();
                c.Service<ProcessCoordinatorService>(s => {
                    s.ConstructUsing(builder => container.GetInstance<ProcessCoordinatorService>());
                    s.WhenStarted(o => o.Start());
                    s.WhenStopped(o => {
                        o.Stop();
                        //container.Dispose();
                    });
                });
            });
        }

        private static void DisplayStateMachine() {
            Trace.Listeners.Add(new TextWriterTraceListener(Console.Out));
            StateMachineInspector.Trace(new ProcessOrchestrationSaga(CombGuid.Generate()));
        }

        private static IContainer BootStrapContainer() {

            var container =
                new Container(x => x.For<ProcessCoordinatorService>()
                                    .LifecycleIs(new SingletonLifecycle())
                                    .Use<ProcessCoordinatorService>());

            //container.Configure( cfg => cfg.For<ProcessOrchestrationSaga>());
            container.Configure(cfg => cfg.For(typeof(ISagaRepository<ProcessOrchestrationSaga>))
                .LifecycleIs(new SingletonLifecycle())
                .Use(typeof(InMemorySagaRepository<ProcessOrchestrationSaga>))
                );

            container.Configure(x=> x.For<ProcessOrchestrationSaga>()
                .Use<ProcessOrchestrationSaga>());
            
            container.Configure(cfg => cfg.For<IServiceBus>()
                                           .LifecycleIs(new SingletonLifecycle())
                                           .Use(context => ServiceBusFactory.New(sbc => {
                                               sbc.ReceiveFrom(
                                                   "rabbitmq://localhost/ProcessCoordinator");
                                               sbc.UseRabbitMq();
                                               sbc.UseRabbitMqRouting();
                                               sbc.Subscribe(subs=> { subs.LoadFrom(container);});
                                           }
                                                               )));
            
            return container;
        }
    }
}
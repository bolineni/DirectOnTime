// Copyright (c) Direct General Corporation. All rights reserved.
//  
// Reproduction or transmission in whole or in part, in any form or 
// by any means, electronic, mechanical or otherwise, is prohibited 
// without the prior written consent of the copyright owner.
//  
// SolutionName : DirectOnTime
// Project/Filename: DirectOnTime.PaymentProcessor / Program.cs
// Created By (Date): Shibu K. Raj - {pgskr} (20/09/2011 10:00 AM)
// Last Modified By (Date) : Shibu K. Raj - {pgskr} (20/09/2011 10:00 AM)

namespace DirectOnTime.PaymentProcessor {
    using System;
    using System.Diagnostics;
    using System.IO;

    using MassTransit;
    using MassTransit.Saga;
    using Magnum;
    using Magnum.StateMachine;
    using StructureMap;
    using StructureMap.Pipeline;
    using log4net.Config;
    using Topshelf;

    internal static class Program {

        [STAThread]
        private static void Main(string[] args) {
            XmlConfigurator.ConfigureAndWatch(new FileInfo(AppDomain.CurrentDomain.BaseDirectory + "PaymentProcessor.log4net.xml"));
            var container = BootStrapContainer();
            HostFactory.Run(c => {
                c.SetServiceName("DirectOnTime.PaymentProcessor");
                c.SetDisplayName("Direct OnTime - Payment Processor");
                c.SetDescription("Real Time Monthly Payment Processor");
                c.RunAsLocalSystem();
                //c.AddDependency()
                DisplayStateMachine();
                c.Service<PaymentProcessorService>(s => {
                    s.ConstructUsing(builder => container.GetInstance<PaymentProcessorService>());
                    s.WhenStarted(o => o.Start());
                    s.WhenStopped(o => o.Stop());
                });
            });

        }

        private static IContainer BootStrapContainer() {
            // configure the service first.
            var container = new Container(x => x.For<PaymentProcessorService>()
                .LifecycleIs(new SingletonLifecycle())
                .Use<PaymentProcessorService>());

            // If saga - Configure the saga repository
            container.Configure(cfg => cfg.For(typeof(ISagaRepository<PaymentProcessorSaga>))
                .LifecycleIs(new SingletonLifecycle())
                .Use(typeof(InMemorySagaRepository<PaymentProcessorSaga>)));

            // If saga -- configure the saga itself
            container.Configure(x => x.For<PaymentProcessorSaga>()
                .Use<PaymentProcessorSaga>());

            // configure the service bus.
            container.Configure(cfg => cfg.For<IServiceBus>()
                .LifecycleIs(new SingletonLifecycle())
                .Use(context => ServiceBusFactory.New(sbc => {
                    sbc.ReceiveFrom("rabbitmq://localhost/PaymentProcessor");
                    sbc.UseRabbitMq();
                    sbc.UseRabbitMqRouting();
                    sbc.UseControlBus();
                    sbc.Subscribe(subs => {
                        subs.LoadFrom(container);
                    });
                }))
                );
            return container;
        }

        private static void DisplayStateMachine() {
            Trace.Listeners.Add(new TextWriterTraceListener(Console.Out));
            StateMachineInspector.Trace(new PaymentProcessorSaga(CombGuid.Generate()));
        }
    }
}
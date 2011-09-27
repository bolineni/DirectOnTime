// Copyright (c) Direct General Corporation. All rights reserved.
//  
// Reproduction or transmission in whole or in part, in any form or 
// by any means, electronic, mechanical or otherwise, is prohibited 
// without the prior written consent of the copyright owner.
//  
// SolutionName : DirectOnTime
// Project/Filename: DirectOnTime.Infrastructure / Program.cs
// Created By (Date): Shibu K. Raj - {pgskr} (20/09/2011 9:59 AM)
// Last Modified By (Date) : Shibu K. Raj - {pgskr} (20/09/2011 9:59 AM)

namespace DirectOnTime.Infrastructure {
    using System;
    using System.IO;
    
    using MassTransit;
    using StructureMap;
    using StructureMap.Pipeline;
    using log4net.Config;
    using Topshelf;

    using Messages.Audit;
    using Messages.Audit.Payment;
    
    internal static class Program {
        [STAThread]
        private static void Main(string[] args) {
            XmlConfigurator.ConfigureAndWatch(new FileInfo(AppDomain.CurrentDomain.BaseDirectory + "Infrastructure.log4net.xml"));
            var container = BootStrapContainer();
            HostFactory.Run(c => {
                c.SetServiceName("DirectOnTime.Infrastructure");
                c.SetDisplayName("Direct OnTime - Infrastructure");
                c.SetDescription("Real Time - Infrascture Service");
                c.RunAsLocalSystem();
                c.Service<InfrastructureService>(s => {
                                                         s.ConstructUsing(
                                                             builder =>
                                                             container.GetInstance
                                                                 <InfrastructureService>());
                                                         s.WhenStarted(o => o.Start());
                                                         s.WhenStopped(o => o.Stop());
                                                     });
            });
        }

        private static IContainer BootStrapContainer() {
            var container = new Container();

            // Configure the service bus.
            container.Configure(cfg => cfg.For<IServiceBus>()
                .LifecycleIs(new SingletonLifecycle())
                .Use(context => ServiceBusFactory.New(sbc => {
                                                             sbc.ReceiveFrom("");
                                                             sbc.UseRabbitMq();
                                                             sbc.UseRabbitMqRouting();
                                                             sbc.Subscribe(subs => subs.LoadFrom(container));
                                                         })));
            return container;
        }
    }

}
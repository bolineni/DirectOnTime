// Copyright (c) Direct General Corporation. All rights reserved.
//  
// Reproduction or transmission in whole or in part, in any form or 
// by any means, electronic, mechanical or otherwise, is prohibited 
// without the prior written consent of the copyright owner.
//  
// SolutionName : DirectOnTime
// Project/Filename: DirectOnTime.Infrastructure / InfrastructureService.cs
// Created By (Date): Shibu K. Raj - {pgskr} (27/09/2011 3:00 PM)
// Last Modified By (Date) : Shibu K. Raj - {pgskr} (27/09/2011 3:00 PM)

namespace DirectOnTime.Infrastructure {
    using MassTransit;
    using log4net;

    using Messages.Audit;
    

    public class InfrastructureService : Consumes<IAudit>.All {
        private readonly IServiceBus _bus;
        private UnsubscribeAction _unsubscribe;


        public InfrastructureService(IServiceBus bus) {
            _bus = bus;
        }

        public void Start() {
            //_unsubscribe = _bus.Su
        }

        public void Stop() {
            _unsubscribe();
            _bus.Dispose();
        }

        public void Consume(IAudit message) {
            new AuditMessageProcessor<IAudit>().ProcessMessage(message);
        }
    }
}
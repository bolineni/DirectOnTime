// Copyright (c) Direct General Corporation. All rights reserved.
//  
// Reproduction or transmission in whole or in part, in any form or 
// by any means, electronic, mechanical or otherwise, is prohibited 
// without the prior written consent of the copyright owner.
//  
// SolutionName : DirectOnTime
// Project/Filename: DirectOnTime.ProcessCoordinator / ProcessCoordinatorService.cs
// Created By (Date): Shibu K. Raj - {pgskr} (21/09/2011 1:15 PM)
// Last Modified By (Date) : Shibu K. Raj - {pgskr} (21/09/2011 1:15 PM)

namespace DirectOnTime.ProcessCoordinator {
    using MassTransit;
    using MassTransit.Saga;


    public class ProcessCoordinatorService {
        private IServiceBus _bus;
        private ISagaRepository<ProcessOrchestrationSaga> _sagaRepository;
        private UnsubscribeAction _unsubscribeAction;


        public ProcessCoordinatorService(IServiceBus bus, ISagaRepository<ProcessOrchestrationSaga> sagaRepository) {
            _bus = bus;
            _sagaRepository = sagaRepository;
        }

        public void Start()
        {
            _unsubscribeAction = _bus.SubscribeSaga(_sagaRepository);
        }


        public void Stop()
        {
            _unsubscribeAction();
            _bus.Dispose();
        }
    }
}
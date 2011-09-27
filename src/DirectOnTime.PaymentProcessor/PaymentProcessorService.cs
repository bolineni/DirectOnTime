// Copyright (c) Direct General Corporation. All rights reserved.
//  
// Reproduction or transmission in whole or in part, in any form or 
// by any means, electronic, mechanical or otherwise, is prohibited 
// without the prior written consent of the copyright owner.
//  
// SolutionName : DirectOnTime
// Project/Filename: DirectOnTime.PaymentProcessor / PaymentProcessorService.cs
// Created By (Date): Shibu K. Raj - {pgskr} (26/09/2011 4:44 PM)
// Last Modified By (Date) : Shibu K. Raj - {pgskr} (26/09/2011 4:45 PM)

namespace DirectOnTime.PaymentProcessor
{
    using MassTransit;
    using MassTransit.Saga;
    using log4net;

    public class PaymentProcessorService
    {
        private readonly IServiceBus _bus;
        private readonly ISagaRepository<PaymentProcessorSaga> _sagaRepository; 
        private UnsubscribeAction _unsubscribe;
        private readonly ILog _log = LogManager.GetLogger(typeof (PaymentProcessorService));

        public PaymentProcessorService(IServiceBus bus, ISagaRepository<PaymentProcessorSaga> sagaRepository)
        {
            _bus = bus;
            _sagaRepository = sagaRepository;
        }

        public void Start()
        {
           _unsubscribe = _bus.SubscribeSaga(_sagaRepository);
        }

        public  void Stop()
        {
            _unsubscribe();
            _bus.Dispose();
        }

    }
}
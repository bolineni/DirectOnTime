// Copyright (c) Direct General Corporation. All rights reserved.
//  
// Reproduction or transmission in whole or in part, in any form or 
// by any means, electronic, mechanical or otherwise, is prohibited 
// without the prior written consent of the copyright owner.
//  
// SolutionName : DirectOnTime
// Project/Filename: DirectOnTime.ProcessCoordinator / ProcessOrchestrationSaga.cs
// Created By (Date): Shibu K. Raj - {pgskr} (22/09/2011 3:32 PM)
// Last Modified By (Date) : Shibu K. Raj - {pgskr} (22/09/2011 3:32 PM)

namespace DirectOnTime.ProcessCoordinator {
    using System;
    using MassTransit;
    using MassTransit.Saga;
    using Magnum.StateMachine;
    using Messages.Inbound;
    using Messages.OutBound;
    using log4net;

    public class ProcessOrchestrationSaga : SagaStateMachine<ProcessOrchestrationSaga>, ISaga
    {

        private readonly ILog _log = LogManager.GetLogger(typeof (ProcessOrchestrationSaga));
        public IServiceBus Bus { get; set; }
        public Guid CorrelationId { get; set; }


        // Define the saga states.
        public static State Initial { get; set; }
        public static State Completed { get; set; }
        public static State WaitingForProcessingComplete { get; set; }

        // Define Saga Events - Initiating Events.
        public static Event<MonthlyPaymentCompleteMessage> NewMonthlyPaymentRecevied { get; set; }
        //public static Event<NewBusinessCompleteMessage> NewBusinessTransactionReceived { get; set; }
        //public static Event<EndorsementBusinessCompleteMessage> EndorsementTransactionReceived { get; set; }
        //public static Event<RenewalBusinessCompleteMessage> RenewalTransactionReceived { get; set; }

        // Closing Events ....
        public static Event<FinishMonthlyPaymentProcessorMessage> PaymentProcessorCompleted { get; set; }


        public ProcessOrchestrationSaga(Guid id) {
            this.CorrelationId = id;
        }

        static ProcessOrchestrationSaga()
        {
            Define(() =>
                       {
                           Initially(
                                When(NewMonthlyPaymentRecevied)
                                    .Then((saga, message) => saga.InitiateMonthlyPaymentProcessing(message))
                                    .TransitionTo(WaitingForProcessingComplete)
                               );
                            
                           During(WaitingForProcessingComplete,
                                 When(PaymentProcessorCompleted)
                                    .Then((saga,message) => saga.CloseMonthlyPaymentProcessing())
                                    .Complete()
                                 );

                       }
                );
        }

        public void InitiateMonthlyPaymentProcessing(MonthlyPaymentCompleteMessage message) {
            Console.WriteLine("Thios is a test");

        }

        public void CloseMonthlyPaymentProcessing() {
            Console.WriteLine("Montly Payment Processing is now completed.");
            //_log.Error(string.Format("'{0}' threw an exception publishing message '{1}'",
            //    consumer.GetType().FullName, message.GetType().FullName), ex);
        }


        // New Business Processing ...
        public void InitiateNewBusinessTransactionProcessing(NewBusinessCompleteMessage message) {
        }

        public void CloseNewBusinessTransactionProcessing() {
        }

        // Endorsement Tranaction Processing .....
        public void InitiateEndorsementTransactionProcessing(EndorsementBusinessCompleteMessage message) {
        }

        public void CloseEndorsementTransactionProcessing() {
        }

        // Renewal Business Processing ...
        public void InitiateRenewalBusinessTransactionProcessing(RenewalBusinessCompleteMessage message) {
        }

        public void CloseRenewalBusinessTransactionProcessing() {
        }



    }
}
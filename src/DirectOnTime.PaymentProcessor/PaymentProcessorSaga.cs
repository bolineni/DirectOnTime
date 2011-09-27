// Copyright (c) Direct General Corporation. All rights reserved.
//  
// Reproduction or transmission in whole or in part, in any form or 
// by any means, electronic, mechanical or otherwise, is prohibited 
// without the prior written consent of the copyright owner.
//  
// SolutionName : DirectOnTime
// Project/Filename: DirectOnTime.PaymentProcessor / PaymentProcessorSaga.cs
// Created By (Date): Shibu K. Raj - {pgskr} (26/09/2011 5:29 PM)
// Last Modified By (Date) : Shibu K. Raj - {pgskr} (26/09/2011 5:29 PM)


namespace DirectOnTime.PaymentProcessor {
    using System;

    using MassTransit;
    using MassTransit.Saga;
    using Magnum.StateMachine;
    using log4net;

    using Messages.Payment;
    using Messages.Audit.Payment;

    public class PaymentProcessorSaga : SagaStateMachine<PaymentProcessorSaga>, ISaga 
    {
        private readonly ILog _log = LogManager.GetLogger(typeof(PaymentProcessorSaga));

        public IServiceBus Bus { get; set; }
        public Guid CorrelationId { get; set; }

        // Define Saga Events
        public static State Initial { get; set; }
        public static State WaitingForLoadPaymentDataMessage { get; set; }
        public static State WaitingForPostPaymentDataMessage { get; set; }
        public static State WaitingForEndPaymentProcessMessage { get; set; }
        public static State Completed { get; set; }

        // Define saga Evenets / Transitions
        public static Event<BeginPaymentProcess> BeginPaymentProcessMessageReceived { get; set; }
        public static Event<LoadPaymentData> LoadPaymentDataMessageReceived { get; set; }
        public static Event<PostPaymentData> PostPaymentDataMessageReceived { get; set; }
        public static Event<EndPaymentProcess> EndPaymentProcessMessageReceived { get; set; }


        public PaymentProcessorSaga(Guid id) {
            this.CorrelationId = id;
        }

        static PaymentProcessorSaga() {
            Define(() => {
                            Initially(
                                When(BeginPaymentProcessMessageReceived)
                                .Then((saga, message) => saga.BeginPaymentProcessing(message))
                                .TransitionTo(WaitingForLoadPaymentDataMessage)
                                );
                            During(WaitingForLoadPaymentDataMessage,
                                When(LoadPaymentDataMessageReceived)
                                .Then((saga, message) => saga.LoadPaymentDataProcessing(message))
                                .TransitionTo(WaitingForPostPaymentDataMessage)
                                );
                            During(WaitingForPostPaymentDataMessage,
                                When(PostPaymentDataMessageReceived)
                                .Then((saga, message) => saga.PostPaymentDataProcessing(message))
                                .TransitionTo(WaitingForEndPaymentProcessMessage)
                                );
                            During(WaitingForEndPaymentProcessMessage,
                                When(EndPaymentProcessMessageReceived)
                                .Then((saga, message) => saga.EndPaymentProcessing(message))
                                .Complete()
                                );
                        });

        }

        public void BeginPaymentProcessing(BeginPaymentProcess message) {
            // First Publish the Audit Messages.
            this.Bus.Publish(new BeginPaymentAudit());
            // Pubslish the Load Payment Data Message.
            this.Bus.Publish(new LoadPaymentData());
        }

        public void LoadPaymentDataProcessing(LoadPaymentData message) {
            this.Bus.Publish(new LoadPaymentAudit());
            //TODO  Do the business action here.
            this.Bus.Publish(new PostPaymentData());

        }

        public void PostPaymentDataProcessing(PostPaymentData message) {
            this.Bus.Publish(new PostPaymentAudit());
            // Do the business process here
            this.Bus.Publish(new EndPaymentProcess());
        }

        public void EndPaymentProcessing(EndPaymentProcess message) {
            this.Bus.Publish(new EndPaymentAudit());
            // Notify any one about the completion - if required.
        }
    }
}
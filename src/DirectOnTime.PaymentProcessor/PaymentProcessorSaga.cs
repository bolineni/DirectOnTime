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
    using Messages.Audit;

    public class PaymentProcessorSaga : SagaStateMachine<PaymentProcessorSaga>, ISaga {
        private readonly ILog _log = LogManager.GetLogger(typeof(PaymentProcessorSaga));

        public IServiceBus Bus { get; set; }
        public Guid CorrelationId { get; set; }

        // Define Saga Events
        public static State Initial { get; set; }
        public static State WaitingForBeginPaymentProcessMessage { get; set; }
        public static State WaitingForLoadPaymentDataMessage { get; set; }
        public static State WaitingForPostPaymentDataMessage { get; set; }
        public static State WaitingForEndPaymentProcessMessage { get; set; }
        public static State Completed { get; set; }

        // Define saga Evenets / Transitions
        public static Event<InitPaymentProcess> InitPaymentProcessMessageReceived { get; set; }
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
                        When(InitPaymentProcessMessageReceived)
                            .Then((saga, message) => saga.InitializePaymentProcessing(message))
                            .TransitionTo(WaitingForBeginPaymentProcessMessage)
                    );
                During(WaitingForBeginPaymentProcessMessage,
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

        public void InitializePaymentProcessing(InitPaymentProcess message) {
            // publish begin Audit
            this.Bus.Publish(
                new AuditBegin {
                    CorrelationId = message.CorrelationId,
                    BusinessUnit = message.BusinessUnit,
                    UserName = message.UserName,
                    MessageId = new Guid(),
                    RequestTime = DateTime.Now.ToShortDateString(),
                    AuditMessage = "Init Payment Processes  Step Started ..... "
                });
            //TODO -- Do the processing here ..
            this.Bus.Publish(new BeginPaymentProcess());
            this.Bus.Publish(
                    new AuditEnd {
                        CorrelationId = message.CorrelationId,
                        BusinessUnit = message.BusinessUnit,
                        UserName = message.UserName,
                        MessageId = new Guid(),
                        RequestTime = DateTime.Now.ToShortDateString(),
                        AuditMessage = "Init Payment Process Step Completed Sucessfully .... "
                    });
        }

        public void BeginPaymentProcessing(BeginPaymentProcess message) {

            this.Bus.Publish(
                new AuditBegin {
                    CorrelationId = message.CorrelationId,
                    BusinessUnit = message.BusinessUnit,
                    UserName = message.UserName,
                    MessageId = new Guid(),
                    RequestTime = DateTime.Now.ToShortDateString(),
                    AuditMessage = "Begin Payment Processing Step Started ....."
                });
            //TODO -- Do the processing here ..
            this.Bus.Publish(new LoadPaymentData());
            this.Bus.Publish(
                   new AuditEnd {
                       CorrelationId = message.CorrelationId,
                       BusinessUnit = message.BusinessUnit,
                       UserName = message.UserName,
                       MessageId = new Guid(),
                       RequestTime = DateTime.Now.ToShortDateString(),
                       AuditMessage = "Begin Payment Process Step Completed Sucessfully ...."
                   });
        }

        public void LoadPaymentDataProcessing(LoadPaymentData message) {

            this.Bus.Publish(
                new AuditBegin {
                    CorrelationId = message.CorrelationId,
                    BusinessUnit = message.BusinessUnit,
                    UserName = message.UserName,
                    MessageId = new Guid(),
                    RequestTime = DateTime.Now.ToShortDateString(),
                    AuditMessage = "Load Payment  Data Processing Step Started ....."
                });
            //TODO -- Do the processing here ..
            this.Bus.Publish(new PostPaymentData());
            this.Bus.Publish(
                   new AuditEnd {
                       CorrelationId = message.CorrelationId,
                       BusinessUnit = message.BusinessUnit,
                       UserName = message.UserName,
                       MessageId = new Guid(),
                       RequestTime = DateTime.Now.ToShortDateString(),
                       AuditMessage = "Load Payment Data Processing Step Completed Sucessfully ...."
                   });

        }

        public void PostPaymentDataProcessing(PostPaymentData message) {
            this.Bus.Publish(
                new AuditBegin {
                    CorrelationId = message.CorrelationId,
                    BusinessUnit = message.BusinessUnit,
                    UserName = message.UserName,
                    MessageId = new Guid(),
                    RequestTime = DateTime.Now.ToShortDateString(),
                    AuditMessage = "Post Payment  Data Processing Step Started ....."
                });
            //TODO Do the business process here
            this.Bus.Publish(new EndPaymentProcess());
            this.Bus.Publish(
                   new AuditEnd {
                       CorrelationId = message.CorrelationId,
                       BusinessUnit = message.BusinessUnit,
                       UserName = message.UserName,
                       MessageId = new Guid(),
                       RequestTime = DateTime.Now.ToShortDateString(),
                       AuditMessage = "Post Payment Data Processing Step Completed Sucessfully ...."
                   });
        }

        public void EndPaymentProcessing(EndPaymentProcess message) {
            this.Bus.Publish(
                   new AuditEnd {
                       CorrelationId = message.CorrelationId,
                       BusinessUnit = message.BusinessUnit,
                       UserName = message.UserName,
                       MessageId = new Guid(),
                       RequestTime = DateTime.Now.ToShortDateString(),
                       AuditMessage = "Payment Processing - All Steps Completed Sucessfully"
                   });
            // Notify any one about the completion - if required.
        }
    }
}
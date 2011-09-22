// Copyright (c) Direct General Corporation. All rights reserved.
//  
// Reproduction or transmission in whole or in part, in any form or 
// by any means, electronic, mechanical or otherwise, is prohibited 
// without the prior written consent of the copyright owner.
//  
// SolutionName : DirectOnTime
// Project/Filename: DirectOnTime.Messages / MonthlyPaymentCompleteMessage.cs
// Created By (Date): Shibu K. Raj - {pgskr} (22/09/2011 3:28 PM)
// Last Modified By (Date) : Shibu K. Raj - {pgskr} (22/09/2011 3:28 PM)

namespace DirectOnTime.Messages.Inbound
{
    using System;
    using MassTransit;

    [Serializable]
    public class MonthlyPaymentCompleteMessage : CorrelatedBy<Guid>
    {
        public Guid CorrelationId { get; set; }
        public string ClientId { get; set; }
        public string ReceiptId { get; set; }
        public string BusinessUnit { get; set; }
        public string UserName { get; set; }
    }
}
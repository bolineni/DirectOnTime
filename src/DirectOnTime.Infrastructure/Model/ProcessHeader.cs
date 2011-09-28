// Copyright (c) Direct General Corporation. All rights reserved.
//  
// Reproduction or transmission in whole or in part, in any form or 
// by any means, electronic, mechanical or otherwise, is prohibited 
// without the prior written consent of the copyright owner.
//  
// SolutionName : DirectOnTime
// Project/Filename: DirectOnTime.Infrastructure / ProcessHeader.cs
// Created By (Date): Shibu K. Raj - {pgskr} (28/09/2011 5:08 PM)
// Last Modified By (Date) : Shibu K. Raj - {pgskr} (28/09/2011 5:08 PM)

namespace DirectOnTime.Infrastructure.Model
{
    using System;

    public class ProcessHeader
    {
        public Guid CorrelationId { get; set; }
        public Guid MessageId { get; set; }
        public string ProcessType { get; set; }
        public string BusinessUnit { get; set; }
        public string UserName { get; set; }
        public string RequestTime { get; set; }
        public string ReceiptId { get; set; }
        public string ClientId { get; set; }
    }
}
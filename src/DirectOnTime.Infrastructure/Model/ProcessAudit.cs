// Copyright (c) Direct General Corporation. All rights reserved.
//  
// Reproduction or transmission in whole or in part, in any form or 
// by any means, electronic, mechanical or otherwise, is prohibited 
// without the prior written consent of the copyright owner.
//  
// SolutionName : DirectOnTime
// Project/Filename: DirectOnTime.Infrastructure / ProcessAudit.cs
// Created By (Date): Shibu K. Raj - {pgskr} (29/09/2011 8:18 AM)
// Last Modified By (Date) : Shibu K. Raj - {pgskr} (29/09/2011 8:18 AM)

namespace DirectOnTime.Infrastructure.Model
{
    using System;

    public class ProcessAudit
    {
        public Guid CorrelationId { get; set; }
        public string BusinessUnit { get; set; }
        public string UserName { get; set; }
        public string RequestTime { get; set; }
        public string AuditStatus { get; set; }
        public string AuditMessage { get; set; }

    }
}
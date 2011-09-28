// Copyright (c) Direct General Corporation. All rights reserved.
//  
// Reproduction or transmission in whole or in part, in any form or 
// by any means, electronic, mechanical or otherwise, is prohibited 
// without the prior written consent of the copyright owner.
//  
// SolutionName : DirectOnTime
// Project/Filename: DirectOnTime.Messages / AuditBase.cs
// Created By (Date): Shibu K. Raj - {pgskr} (27/09/2011 9:00 AM)
// Last Modified By (Date) : Shibu K. Raj - {pgskr} (27/09/2011 10:00 AM)

namespace DirectOnTime.Messages.Audit
{
    using System;
    [Serializable]
    public abstract class AuditBase : IAudit
    {
        public Guid MessageId { get; set; }
        public string BusinessUnit { get; set; }
        public string UserName { get; set; }
        public string RequestTime { get; set; }
        public Guid CorrelationId { get; set; }
        
        public string AuditMessage { get; set; }
    }
}
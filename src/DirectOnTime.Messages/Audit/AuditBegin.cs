// Copyright (c) Direct General Corporation. All rights reserved.
//  
// Reproduction or transmission in whole or in part, in any form or 
// by any means, electronic, mechanical or otherwise, is prohibited 
// without the prior written consent of the copyright owner.
//  
// SolutionName : DirectOnTime
// Project/Filename: DirectOnTime.Messages / AuditBegin.cs
// Created By (Date): Shibu K. Raj - {pgskr} (27/09/2011 3:16 PM)
// Last Modified By (Date) : Shibu K. Raj - {pgskr} (27/09/2011 3:16 PM)

namespace DirectOnTime.Messages.Audit
{
    public class AuditBegin : AuditBase
    {
        public AuditBegin()
        {
            this.AuditStatus = "Start";
        }
        public string AuditStatus { get; private set; }
    }
}
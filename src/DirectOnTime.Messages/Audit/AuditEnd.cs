// Copyright (c) Direct General Corporation. All rights reserved.
//  
// Reproduction or transmission in whole or in part, in any form or 
// by any means, electronic, mechanical or otherwise, is prohibited 
// without the prior written consent of the copyright owner.
//  
// SolutionName : DirectOnTime
// Project/Filename: DirectOnTime.Messages / AuditEnd.cs
// Created By (Date): Shibu K. Raj - {pgskr} (27/09/2011 3:16 PM)
// Last Modified By (Date) : Shibu K. Raj - {pgskr} (27/09/2011 3:16 PM)

namespace DirectOnTime.Messages.Audit
{
    public class AuditEnd : AuditBase
    {
        public AuditEnd()
        {
            this.AuditStatus = "End";
        }
        public string AuditStatus { get; private set; }
        public string AuditMessge { get; set; }
    }
}
// Copyright (c) Direct General Corporation. All rights reserved.
//  
// Reproduction or transmission in whole or in part, in any form or 
// by any means, electronic, mechanical or otherwise, is prohibited 
// without the prior written consent of the copyright owner.
//  
// SolutionName : DirectOnTime
// Project/Filename: DirectOnTime.Messages / AuditFailure.cs
// Created By (Date): Shibu K. Raj - {pgskr} (28/09/2011 9:27 AM)
// Last Modified By (Date) : Shibu K. Raj - {pgskr} (28/09/2011 9:27 AM)

namespace DirectOnTime.Messages.Audit
{
    public class AuditFailure : AuditBase
    {

        public AuditFailure()
        {
            this.AuditStatus = "Failed";
        }
        public string AuditStatus { get; private set; }
    }
}
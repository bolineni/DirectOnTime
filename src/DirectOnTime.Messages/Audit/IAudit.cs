// Copyright (c) Direct General Corporation. All rights reserved.
//  
// Reproduction or transmission in whole or in part, in any form or 
// by any means, electronic, mechanical or otherwise, is prohibited 
// without the prior written consent of the copyright owner.
//  
// SolutionName : DirectOnTime
// Project/Filename: DirectOnTime.Messages / IAudit.cs
// Created By (Date): Shibu K. Raj - {pgskr} (27/09/2011 9:00 AM)
// Last Modified By (Date) : Shibu K. Raj - {pgskr} (27/09/2011 10:00 AM)

namespace DirectOnTime.Messages.Audit
{
    public interface IAudit : IMessage
    {
        string ClientId { get; set; }
        string CompnayNumber { get; set; }
        string PolicyPrefix { get; set; }
        string PolicySequenceNumber { get; set; }
    }
}
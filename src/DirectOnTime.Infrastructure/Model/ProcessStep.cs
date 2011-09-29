// Copyright (c) Direct General Corporation. All rights reserved.
//  
// Reproduction or transmission in whole or in part, in any form or 
// by any means, electronic, mechanical or otherwise, is prohibited 
// without the prior written consent of the copyright owner.
//  
// SolutionName : DirectOnTime
// Project/Filename: DirectOnTime.Infrastructure / ProcessStep.cs
// Created By (Date): Shibu K. Raj - {pgskr} (29/09/2011 8:18 AM)
// Last Modified By (Date) : Shibu K. Raj - {pgskr} (29/09/2011 8:18 AM)

namespace DirectOnTime.Infrastructure.Model
{
    using System;

    public class ProcessStep
    {
        public Guid CorrelationId { get; set; }
        public Guid MessageId { get; set; }
        public string MessageType { get; set; }
        public string StepName { get; set; }
        public string StepStatus { get; set; }
        public string StepStartTime { get; set; }
    }
}
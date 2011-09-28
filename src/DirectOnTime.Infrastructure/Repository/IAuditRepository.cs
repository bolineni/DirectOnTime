// Copyright (c) Direct General Corporation. All rights reserved.
//  
// Reproduction or transmission in whole or in part, in any form or 
// by any means, electronic, mechanical or otherwise, is prohibited 
// without the prior written consent of the copyright owner.
//  
// SolutionName : DirectOnTime
// Project/Filename: DirectOnTime.Infrastructure / IAuditRepository.cs
// Created By (Date): Shibu K. Raj - {pgskr} (28/09/2011 10:33 AM)
// Last Modified By (Date) : Shibu K. Raj - {pgskr} (28/09/2011 10:33 AM)

namespace DirectOnTime.Infrastructure.Repository
{
    using Model;
    public interface IAuditRepository
    {
        // Fetch Process Header.
        ProcessHeader GetProcessHeader(string clientId);

    }
}
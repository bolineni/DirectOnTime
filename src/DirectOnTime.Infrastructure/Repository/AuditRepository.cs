// Copyright (c) Direct General Corporation. All rights reserved.
//  
// Reproduction or transmission in whole or in part, in any form or 
// by any means, electronic, mechanical or otherwise, is prohibited 
// without the prior written consent of the copyright owner.
//  
// SolutionName : DirectOnTime
// Project/Filename: DirectOnTime.Infrastructure / AuditRepository.cs
// Created By (Date): Shibu K. Raj - {pgskr} (28/09/2011 10:33 AM)
// Last Modified By (Date) : Shibu K. Raj - {pgskr} (28/09/2011 10:33 AM)

namespace DirectOnTime.Infrastructure.Repository
{
    using System;
    using System.Data;
    using System.Data.Common;
    using System.Data.SqlClient;
    using System.Linq;
    using log4net;
    using Dapper;
    using Microsoft.Practices.EnterpriseLibrary.Data;

    using Model;

    public class AuditRepository : IAuditRepository
    {
        private readonly ILog _log = LogManager.GetLogger(typeof (AuditRepository));
        private readonly Database _database; 
        public AuditRepository(Database database)
        {
            _database = database;
        }




        public ProcessHeader GetProcessHeader(string clientId)
        {

            var conn = _database.CreateConnection();
            conn.Open();
            var header = conn.Query<ProcessHeader>("Select * from ProcessHeaders");
            return header.First();

        }
    }
}
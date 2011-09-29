// Copyright (c) Direct General Corporation. All rights reserved.
//  
// Reproduction or transmission in whole or in part, in any form or 
// by any means, electronic, mechanical or otherwise, is prohibited 
// without the prior written consent of the copyright owner.
//  
// SolutionName : DirectOnTime
// Project/Filename: DirectOnTime.Infrastructure / ProcessHeaderRepository.cs
// Created By (Date): Shibu K. Raj - {pgskr} (29/09/2011 12:10 PM)
// Last Modified By (Date) : Shibu K. Raj - {pgskr} (29/09/2011 12:10 PM)

namespace DirectOnTime.Infrastructure.Repository {
    using System.Data;

    using Microsoft.Practices.EnterpriseLibrary.Data;
    using Dapper;
    using log4net;

    using Model;

    public class ProcessHeaderRepository : IAuditRepository<ProcessHeader> {

        private readonly Database _database;
        private readonly ILog _log = LogManager.GetLogger(typeof(ProcessHeaderRepository));

        public ProcessHeaderRepository(Database database) {
            _database = database;
        }

        public void Add(ProcessHeader entity) {
            using (var conn = _database.CreateConnection()) {
                conn.Open();
                var param = new DynamicParameters();
                param.Add("@processType", entity.ProcessType);
                param.Add("@correlationId", entity.CorrelationId);
                param.Add("@messageId", entity.MessageId);
                param.Add("@businessUnit", entity.BusinessUnit);
                param.Add("@userName", entity.UserName);
                param.Add("@requestTime", entity.RequestTime);
                param.Add("@receiptId", entity.ReceiptId);
                param.Add("@clientId", entity.ClientId);
                conn.Execute("usp_DirectOnTime_Audit_SaveProcessHeaders", param, commandType: CommandType.StoredProcedure);
                conn.Close();
            }
        }

        public ProcessHeader GetByMessageId(string messageId) {
            throw new System.NotImplementedException();
        }

        public ProcessHeader GetByCorrelationId(string correlationId) {
            throw new System.NotImplementedException();
        }
    }
}
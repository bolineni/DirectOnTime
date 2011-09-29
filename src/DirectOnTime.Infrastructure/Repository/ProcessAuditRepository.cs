// Copyright (c) Direct General Corporation. All rights reserved.
//  
// Reproduction or transmission in whole or in part, in any form or 
// by any means, electronic, mechanical or otherwise, is prohibited 
// without the prior written consent of the copyright owner.
//  
// SolutionName : DirectOnTime
// Project/Filename: DirectOnTime.Infrastructure / ProcessAuditRepository.cs
// Created By (Date): Shibu K. Raj - {pgskr} (29/09/2011 10:03 AM)
// Last Modified By (Date) : Shibu K. Raj - {pgskr} (29/09/2011 10:03 AM)

namespace DirectOnTime.Infrastructure.Repository {
    using System.Data;

    using Microsoft.Practices.EnterpriseLibrary.Data;
    using Dapper;

    using Model;

    public class ProcessAuditRepository : IAuditRepository<ProcessAudit> {

        private readonly Database _database;
        public ProcessAuditRepository(Database database) {
            _database = database;
        }

        public void Add(ProcessAudit entity) {
            using (var conn = _database.CreateConnection()) {
                conn.Open();
                var param = new DynamicParameters();
                param.Add("@correlationId", entity.CorrelationId);
                param.Add("@businessUnit", entity.BusinessUnit);
                param.Add("@userName", entity.UserName);
                param.Add("@requestTime", entity.RequestTime);
                param.Add("@auditStatus", entity.AuditStatus);
                param.Add("@auditMessage", entity.AuditMessage);
                conn.Execute("usp_DirectOnTime_Audit_SaveProcessAudit", param, commandType: CommandType.StoredProcedure);
                conn.Close();
            }
        }


        public ProcessAudit GetByMessageId(string messageId) {
            throw new System.NotImplementedException();
        }

        public ProcessAudit GetByCorrelationId(string correlationId) {
            throw new System.NotImplementedException();
        }
    }
}
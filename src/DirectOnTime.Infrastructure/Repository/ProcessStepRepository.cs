// Copyright (c) Direct General Corporation. All rights reserved.
//  
// Reproduction or transmission in whole or in part, in any form or 
// by any means, electronic, mechanical or otherwise, is prohibited 
// without the prior written consent of the copyright owner.
//  
// SolutionName : DirectOnTime
// Project/Filename: DirectOnTime.Infrastructure / ProcessStepRepository.cs
// Created By (Date): Shibu K. Raj - {pgskr} (29/09/2011 11:44 AM)
// Last Modified By (Date) : Shibu K. Raj - {pgskr} (29/09/2011 11:44 AM)

namespace DirectOnTime.Infrastructure.Repository {
    using System.Data;

    using Microsoft.Practices.EnterpriseLibrary.Data;
    using Dapper;

    using Model;

    public class ProcessStepRepository : IAuditRepository<ProcessStep> {
        private readonly Database _database;

        public ProcessStepRepository(Database database) {
            _database = database;
        }

        public void Add(ProcessStep entity) {
            using (var conn = _database.CreateConnection()) {
                conn.Open();
                var param = new DynamicParameters();
                
                param.Add("@correlationId", entity.CorrelationId);
                param.Add("@messageId", entity.MessageId);
                param.Add("@messageType", entity.MessageType);
                param.Add("@stepName", entity.StepName);
                param.Add("@stepStatus", entity.StepStatus);
                param.Add("@stepStartTime", entity.StepStartTime);
                conn.Execute("usp_DirectOnTime_Audit_SaveProcessStep", param, commandType: CommandType.StoredProcedure);
                conn.Close();
            }
        }

        public ProcessStep GetByMessageId(string messageId) {
            throw new System.NotImplementedException();
        }

        public ProcessStep GetByCorrelationId(string correlationId) {
            throw new System.NotImplementedException();
        }
    }
}
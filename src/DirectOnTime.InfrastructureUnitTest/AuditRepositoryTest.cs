// Copyright (c) Direct General Corporation. All rights reserved.
//  
// Reproduction or transmission in whole or in part, in any form or 
// by any means, electronic, mechanical or otherwise, is prohibited 
// without the prior written consent of the copyright owner.
//  
// SolutionName : DirectOnTime
// Project/Filename: DirectOnTime.InfrastructureUnitTest / AuditRepositoryTest.cs
// Created By (Date): Shibu K. Raj - {pgskr} (28/09/2011 1:16 PM)
// Last Modified By (Date) : Shibu K. Raj - {pgskr} (28/09/2011 1:16 PM)

namespace DirectOnTime.InfrastructureUnitTest {

    using System.Configuration;
    using System.Linq;
    using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
    using NUnit.Framework;
    using StructureMap;
    using Microsoft.Practices.EnterpriseLibrary.Data;
    using StructureMap.Pipeline;
    using Dapper;
    using DirectOnTime.Infrastructure.Model;

    [TestFixture]
    public class AuditRepositoryTest {

        [Test]
        public void TestContainerConfiguration() {
            var container = new Container();
            container.Configure(x => x.Scan(s => {
                s.Assembly("Microsoft.Practices.EnterpriseLibrary.Data");
                s.AddAllTypesOf(
                    typeof(Database));
            }));
            
            container.Configure(x=>x.For<Database>()
                .LifecycleIs(new SingletonLifecycle())
                .Use(new SqlDatabase(ConfigurationManager.ConnectionStrings["DirectOnTime"].ToString())));
            var db = container.GetInstance<Database>();

            var conn = db.CreateConnection();
            conn.Open();

            var header = conn.Query<ProcessHeader>("Select * from ProcessHeaders where ClientID=@id", new {id = "12334444"});
            
            
        }


   
    }
}
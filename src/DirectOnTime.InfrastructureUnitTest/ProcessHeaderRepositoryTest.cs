// Copyright (c) Direct General Corporation. All rights reserved.
//  
// Reproduction or transmission in whole or in part, in any form or 
// by any means, electronic, mechanical or otherwise, is prohibited 
// without the prior written consent of the copyright owner.
//  
// SolutionName : DirectOnTime
// Project/Filename: DirectOnTime.InfrastructureUnitTest / ProcessHeaderRepositoryTest.cs
// Created By (Date): Shibu K. Raj - {pgskr} (29/09/2011 12:23 PM)
// Last Modified By (Date) : Shibu K. Raj - {pgskr} (29/09/2011 12:23 PM)

namespace DirectOnTime.InfrastructureUnitTest
{
    using System;
    using System.Configuration;
    using System.Data.Common;

    using Microsoft.Practices.EnterpriseLibrary.Data;
    using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
    using NUnit.Framework;
    using StructureMap;
    using StructureMap.Pipeline;

    using Infrastructure.Model;
    using Infrastructure.Repository;
    

    [TestFixture]
    public class ProcessHeaderRepositoryTest
    {

        private IContainer BootStrapContainer() {
            var container = new Container();
            container.Configure(x => x.Scan(s => {
                s.Assembly("Microsoft.Practices.EnterpriseLibrary.Data");
                s.AddAllTypesOf(
                    typeof(Database));
            }));

            container.Configure(x => x.For<Database>()
                .LifecycleIs(new SingletonLifecycle())
                .Use(new SqlDatabase(ConfigurationManager.ConnectionStrings["DirectOnTime"].ToString())));
            return container;
        }

        [Test]
        public void AddTest() {
            // Arrange
            var container = BootStrapContainer();
            IAuditRepository<ProcessHeader> processHeaderRepo = new ProcessHeaderRepository(container.GetInstance<Database>());
            var header = new ProcessHeader
                                       {
                                           ProcessType = "Test Process",
                                           CorrelationId = Guid.NewGuid(),
                                           MessageId = Guid.NewGuid(),
                                           BusinessUnit = "2009",
                                           UserName = "PGSKR",
                                           RequestTime = DateTime.Now.ToShortDateString(),
                                           ReceiptId = "CLARCPT1001",
                                           ClientId = "CLAID10002"
                                       };
            // Act
            processHeaderRepo.Add(header);
            
        }
    }
}
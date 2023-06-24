// --------------------------------------------------------------------------
// <copyright file="SqlExecutorTests.cs" author="Andrii Odeychuk">
//
// Copyright (c) Andrii Odeychuk. ALL RIGHTS RESERVED
// The entire contents of this file is protected by International Copyright Laws.
// </copyright>
// --------------------------------------------------------------------------

using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OdeyTech.SqlProvider.Executor;

namespace OdeyTech.SqlProvider.Test.Executor
{
    [TestClass]
    public class SqlExecutorTests
    {
        private Mock<IDbConnection> mockConnection;
        private Mock<IDbCommand> mockCommand;
        private SqlExecutor executor;

        [TestInitialize]
        public void TestInitialize()
        {
            var transaction = new Mock<IDbTransaction>();
            transaction.Setup(t => t.Commit());

            this.mockConnection = new Mock<IDbConnection>();
            this.mockConnection.Setup(c => c.BeginTransaction()).Returns(transaction.Object);

            this.mockCommand = new Mock<IDbCommand>();
            this.mockConnection.Setup(m => m.CreateCommand()).Returns(this.mockCommand.Object);
            this.executor = new SqlExecutor(this.mockConnection.Object);
        }

        [TestMethod]
        public void Query_ExecutesNonQuery()
        {
            this.executor.Query("SELECT * FROM test");
            this.mockCommand.Verify(m => m.ExecuteNonQuery(), Times.Once);
        }

        [TestMethod]
        public void Select_ExecutesReader()
        {
            var mockReader = new Mock<IDataReader>();
            this.mockCommand.Setup(m => m.ExecuteReader()).Returns(mockReader.Object);

            DataTable result = this.executor.Select("SELECT * FROM test");
            this.mockCommand.Verify(m => m.ExecuteReader(), Times.Once);
        }

        [TestMethod]
        public void StoreProcedure_ExecutesNonQueryAndReturnsOutputParameters()
        {
            var mockParameter = new Mock<DbParameter>();
            mockParameter.Setup(m => m.Direction).Returns(ParameterDirection.Output);
            this.mockCommand.Setup(m => m.ExecuteNonQuery());

            AddSupportParametersProperty(this.mockCommand);

            List<DbParameter> result = this.executor.StoreProcedure("TestProcedure", new DbParameter[] { mockParameter.Object });

            this.mockCommand.Verify(m => m.ExecuteNonQuery(), Times.Once);
            this.mockCommand.VerifySet(m => m.CommandText = It.IsAny<string>(), Times.Once);
            this.mockCommand.VerifySet(m => m.CommandType = CommandType.StoredProcedure, Times.Once);
            this.mockConnection.Verify(m => m.Open(), Times.Once);
            this.mockConnection.Verify(m => m.Close(), Times.Once);

            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(ParameterDirection.Output, result[0].Direction);
        }

        [TestMethod]
        public void StoreFunction_ExecutesNonQueryAndReturnsReturnValue()
        {
            var mockParameter = new Mock<DbParameter>();
            mockParameter.Setup(m => m.Direction).Returns(ParameterDirection.ReturnValue);
            mockParameter.Setup(m => m.Value).Returns("test");
            this.mockCommand.Setup(m => m.ExecuteNonQuery());

            AddSupportParametersProperty(this.mockCommand);

            var result = this.executor.StoreFunction("TestFunction", new DbParameter[] { mockParameter.Object });

            this.mockCommand.Verify(m => m.ExecuteNonQuery(), Times.Once);
            this.mockCommand.VerifySet(m => m.CommandText = It.IsAny<string>(), Times.Once);
            this.mockCommand.VerifySet(m => m.CommandType = CommandType.StoredProcedure, Times.Once);
            this.mockConnection.Verify(m => m.Open(), Times.Once);
            this.mockConnection.Verify(m => m.Close(), Times.Once);

            Assert.AreEqual("test", result);
        }

        private void AddSupportParametersProperty(Mock<IDbCommand> mockCommand)
        {
            var mockParameterCollection = new DataParameterCollectionMock();
            mockCommand.Setup(m => m.Parameters).Returns(mockParameterCollection);
        }
    }
}

// --------------------------------------------------------------------------
// <copyright file="BasicQueryGeneratorTests.cs" author="Andrii Odeychuk">
//
// Copyright (c) Andrii Odeychuk. ALL RIGHTS RESERVED
// The entire contents of this file is protected by International Copyright Laws.
// </copyright>
// --------------------------------------------------------------------------

using Microsoft.VisualStudio.TestTools.UnitTesting;
using OdeyTech.SqlProvider.Entity.Table;
using OdeyTech.SqlProvider.Entity.Table.Column;
using OdeyTech.SqlProvider.Entity.Table.Column.DataType;
using OdeyTech.SqlProvider.Query;

namespace OdeyTech.SqlProvider.Test.Query
{
    [TestClass]
    public class BasicQueryGeneratorTests
    {
        private const string TableName = "TestTable";
        private const string IdColumnName = "Id";
        private const string SecondColumnName = "Name";
        private const int SecondColumnSize = 50;
        private const int IdValue = 1;
        private const string TextValue = "Test";

        private ISqlQueryGenerator queryGenerator;
        private SqlTable table;

        [TestInitialize]
        public void TestInitialize()
        {
            this.queryGenerator = new BasicQueryGenerator();

            this.table = new SqlTable();
            this.table.SetName(TableName);
            this.table.Columns.Add(new SqlColumn(IdColumnName, new SqlServerDataType(SqlServerDataType.DataType.Int)));
            this.table.Columns.Add(new SqlColumn(SecondColumnName, new SqlServerDataType(SqlServerDataType.DataType.VarChar, SecondColumnSize.ToString())));
        }

        [TestMethod]
        public void Create_GeneratesCorrectQuery()
        {
            var query = this.queryGenerator.Create(this.table);

            var expectedQuery = $"CREATE TABLE {TableName} ({IdColumnName} INT, {SecondColumnName} VARCHAR({SecondColumnSize}));";
            Assert.AreEqual(expectedQuery, query);
        }

        [TestMethod]
        public void Select_GeneratesCorrectQuery()
        {
            var query = this.queryGenerator.Select(this.table);

            var expectedQuery = $"SELECT {IdColumnName}, {SecondColumnName} FROM {TableName};";
            Assert.AreEqual(expectedQuery, query);
        }

        [TestMethod]
        public void Insert_GeneratesCorrectQuery()
        {
            this.table.Columns.SetValue(IdColumnName, IdValue);
            this.table.Columns.SetValue(SecondColumnName, TextValue);
            var query = this.queryGenerator.Insert(this.table);

            var expectedQuery = $"INSERT INTO {TableName} ({IdColumnName}, {SecondColumnName}) VALUES ({IdValue}, '{TextValue}');";
            Assert.AreEqual(expectedQuery, query);
        }

        [TestMethod]
        public void Update_GeneratesCorrectQuery()
        {
            this.table.Columns.SetValue(IdColumnName, IdValue);
            this.table.Columns.SetValue(SecondColumnName, TextValue);
            this.table.Columns.Get(IdColumnName).IsExcluded = true;
            this.table.AddConditions($"{IdColumnName} = {IdValue}");
            var query = this.queryGenerator.Update(this.table);

            var expectedQuery = $"UPDATE {TableName} SET {SecondColumnName} = '{TextValue}' WHERE {IdColumnName} = {IdValue};";
            Assert.AreEqual(expectedQuery, query);
        }

        [TestMethod]
        public void Delete_GeneratesCorrectQuery()
        {
            this.table.AddConditions($"{IdColumnName} = {IdValue}");
            var query = this.queryGenerator.Delete(this.table);

            var expectedQuery = $"DELETE FROM {TableName} WHERE {IdColumnName} = {IdValue};";
            Assert.AreEqual(expectedQuery, query);
        }
    }
}

// --------------------------------------------------------------------------
// <copyright file="SqlTableTests.cs" author="Andrii Odeychuk">
//
// Copyright (c) Andrii Odeychuk. ALL RIGHTS RESERVED
// The entire contents of this file is protected by International Copyright Laws.
// </copyright>
// --------------------------------------------------------------------------

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OdeyTech.SqlProvider.Entity.Table;
using OdeyTech.SqlProvider.Entity.Table.Column.DataType;
using OdeyTech.SqlProvider.Entity.Table.Column.ValueConverter;
using OdeyTech.SqlProvider.Enum;

namespace OdeyTech.SqlProvider.Test.Entity.Table
{
    [TestClass]
    public class SqlTableTests
    {
        [TestMethod]
        public void SetName_SetsNameCorrectly()
        {
            var table = new SqlTable();
            table.SetName("test", "prefix");
            Assert.AreEqual("test prefix", table.GetName(true));
        }

        [TestMethod]
        public void AddJoins_AddsJoinsCorrectly()
        {
            var table = new SqlTable();
            table.AddJoins("join1", "join2");
            Assert.AreEqual(" join1 join2", table.GetJoins());
        }

        [TestMethod]
        public void AddConditions_AddsConditionsCorrectly()
        {
            var table = new SqlTable();
            table.AddConditions("condition1", "condition2");
            Assert.AreEqual(" WHERE condition1 AND condition2", table.GetConditions());
        }

        [TestMethod]
        public void AddOrderBy_AddsOrderByCorrectly()
        {
            var table = new SqlTable();
            table.AddOrderBy("order1", "order2");
            Assert.AreEqual(" ORDER BY order1, order2", table.GetOrderBy());
        }

        [TestMethod]
        public void Validate_ThrowsException_WhenTableNameIsNull()
        {
            var table = new SqlTable();
            Assert.ThrowsException<ArgumentException>(() => table.Validate(SqlQueryType.Create));
        }

        [TestMethod]
        public void Clone_CreatesCorrectClone()
        {
            var table = new SqlTable();
            table.SetName("test", "prefix");
            table.Columns.Add("column", new SQLiteDataType(SQLiteDataType.DataType.Text), new SQLiteValueConverter());
            var clone = (SqlTable)table.Clone();
            Assert.AreEqual(table.GetName(true), clone.GetName(true));
            Assert.AreEqual(table.Columns.GetColumnsDataType(), clone.Columns.GetColumnsDataType());
        }

        [TestMethod]
        public void Equals_ReturnsTrue_WhenObjectsAreEqual()
        {
            var table1 = new SqlTable();
            table1.SetName("test", "prefix");
            table1.Columns.Add("column", new SQLiteDataType(SQLiteDataType.DataType.Text), new SQLiteValueConverter());

            var table2 = new SqlTable();
            table2.SetName("test", "prefix");
            table2.Columns.Add("column", new SQLiteDataType(SQLiteDataType.DataType.Text), new SQLiteValueConverter());

            Assert.IsTrue(table1.Equals(table2));
        }
    }
}

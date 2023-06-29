// --------------------------------------------------------------------------
// <copyright file="SqlColumnTests.cs" author="Andrii Odeychuk">
//
// Copyright (c) Andrii Odeychuk. ALL RIGHTS RESERVED
// The entire contents of this file is protected by International Copyright Laws.
// </copyright>
// --------------------------------------------------------------------------

using Microsoft.VisualStudio.TestTools.UnitTesting;
using OdeyTech.SqlProvider.Entity.Table.Column;
using OdeyTech.SqlProvider.Entity.Table.Column.DataType;
using OdeyTech.SqlProvider.Entity.Table.Column.NameConverter;
using OdeyTech.SqlProvider.Entity.Table.Column.ValueConverter;
using OdeyTech.SqlProvider.Enum;

namespace OdeyTech.SqlProvider.Test.Entity.Table.Column
{
    [TestClass]
    public class SqlColumnTests
    {
        private const string ColumnName = "test";
        private const string ColumnAlias = "alias";

        [TestMethod]
        public void GetName_ReturnsCorrectString()
        {
            var column = new SqlColumn(ColumnName, new SQLiteDataType(SQLiteDataType.DataType.Text), ColumnAlias, new SQLiteValueConverter());
            var result = column.GetName(SqlQueryType.Select);
            Assert.AreEqual($"{ColumnName} AS {ColumnAlias}", result);
        }

        [TestMethod]
        public void GetName_ReturnsDateName()
        {
            var column = new SqlColumn(ColumnName, new SQLiteDataType(SQLiteDataType.DataType.Date), ColumnAlias, new SQLiteValueConverter(), new SQLiteDateNameConverter());
            var result = column.GetName(SqlQueryType.Select);
            Assert.AreEqual($"date({ColumnName}, 'unixepoch') AS {ColumnAlias}", result);
        }

        [TestMethod]
        public void GetValue_ReturnsCorrectString()
        {
            var column = new SqlColumn(ColumnName, new SQLiteDataType(SQLiteDataType.DataType.Text), ColumnAlias, new SQLiteValueConverter());
            column.SetValue("test");
            var result = column.GetValue();
            Assert.AreEqual("'test'", result);
        }
    }
}

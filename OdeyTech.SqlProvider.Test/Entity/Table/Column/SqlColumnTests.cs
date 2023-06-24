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
        [TestMethod]
        public void GetName_ReturnsCorrectString()
        {
            var column = new SqlColumn("test", new SQLiteDataType(SQLiteDataType.DataType.Text), "alias", new SQLiteValueConverter());
            var result = column.GetName(SqlQueryType.Select);
            Assert.AreEqual("test AS alias", result);
        }

        [TestMethod]
        public void GetName_ReturnsDateName()
        {
            var column = new SqlColumn("test", new SQLiteDataType(SQLiteDataType.DataType.Date), "alias", new SQLiteValueConverter(), new SQLiteDateNameConverter());
            var result = column.GetName(SqlQueryType.Select);
            Assert.AreEqual("date(test, 'unixepoch') AS alias", result);
        }

        [TestMethod]
        public void GetValue_ReturnsCorrectString()
        {
            var column = new SqlColumn("test", new SQLiteDataType(SQLiteDataType.DataType.Text), "alias", new SQLiteValueConverter());
            column.SetValue("test");
            var result = column.GetValue();
            Assert.AreEqual("'test'", result);
        }


        [TestMethod]
        public void IsExcluded_GetAndSet()
        {
            var column = new SqlColumn("test", new SQLiteDataType(SQLiteDataType.DataType.Text), "alias", new SQLiteValueConverter());
            column.IsExcluded = true;
            Assert.IsTrue(column.IsExcluded);
        }
    }
}

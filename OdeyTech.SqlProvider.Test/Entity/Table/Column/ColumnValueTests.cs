// --------------------------------------------------------------------------
// <copyright file="ColumnValueTests.cs" author="Andrii Odeychuk">
//
// Copyright (c) Andrii Odeychuk. ALL RIGHTS RESERVED
// The entire contents of this file is protected by International Copyright Laws.
// </copyright>
// --------------------------------------------------------------------------

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OdeyTech.SqlProvider.Entity.Table.Column;
using OdeyTech.SqlProvider.Entity.Table.Column.DataType;
using OdeyTech.SqlProvider.Entity.Table.Column.ValueConverter;

namespace OdeyTech.SqlProvider.Test.Entity.Table.Column
{
    [TestClass]
    public class ColumnValueTests
    {
        [TestMethod]
        public void GetValue_ReturnsCorrectString()
        {
            var columnValue = new ColumnValue
            {
                DataType = new SQLiteDataType(SQLiteDataType.DataType.Text),
                ValueConverter = new SQLiteValueConverter()
            };
            columnValue.SetValue("test");
            var result = columnValue.GetValue();
            Assert.AreEqual("'test'", result);
        }

        [TestMethod]
        public void SetValue_QuoteEscapingy()
        {
            var columnValue = new ColumnValue
            {
                DataType = new SQLiteDataType(SQLiteDataType.DataType.Text),
                ValueConverter = new SQLiteValueConverter()
            };
            columnValue.SetValue("test's");
            var result = columnValue.GetValue();
            Assert.AreEqual("'test''s'", result);
        }

        [TestMethod]
        public void SetValue_ThrowsException_WhenDataTypeIsNull()
        {
            var columnValue = new ColumnValue { ValueConverter = new SQLiteValueConverter() };
            Assert.ThrowsException<InvalidOperationException>(() => columnValue.SetValue("test"));
        }

        [TestMethod]
        public void GetValue_ThrowsException_WhenDataTypeIsNull()
        {
            var columnValue = new ColumnValue { ValueConverter = new SQLiteValueConverter() };
            Assert.ThrowsException<InvalidOperationException>(() => columnValue.GetValue());
        }
    }
}

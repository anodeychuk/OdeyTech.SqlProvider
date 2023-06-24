// --------------------------------------------------------------------------
// <copyright file="SQLiteValueConverterTests.cs" author="Andrii Odeychuk">
//
// Copyright (c) Andrii Odeychuk. ALL RIGHTS RESERVED
// The entire contents of this file is protected by International Copyright Laws.
// </copyright>
// --------------------------------------------------------------------------

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OdeyTech.SqlProvider.Entity.Table.Column.DataType;
using OdeyTech.SqlProvider.Entity.Table.Column.ValueConverter;

namespace OdeyTech.SqlProvider.Test.Entity.Table.Column.ValueConverter
{
    [TestClass]
    public class SQLiteValueConverterTests
    {
        private readonly SQLiteValueConverter converter = new();

        [TestMethod]
        public void ConvertToDbValue_Null_ReturnsNullString()
        {
            var result = this.converter.ConvertToDbValue(null, DbDataTypeCategory.String);
            Assert.AreEqual("NULL", result);
        }

        [TestMethod]
        public void ConvertToDbValue_String_ReturnsQuotedString()
        {
            var result = this.converter.ConvertToDbValue("test", DbDataTypeCategory.String);
            Assert.AreEqual("'test'", result);
        }

        [TestMethod]
        public void ConvertToDbValue_DateTime_ReturnsUnixEpochString()
        {
            var dateTime = new DateTime(2023, 6, 22, 12, 0, 0);
            var result = this.converter.ConvertToDbValue(dateTime, DbDataTypeCategory.DateTime);
            Assert.AreEqual("unixepoch('2023-06-22 12:00:00')", result);
        }

        [TestMethod]
        public void ConvertToDbValue_Date_ReturnsUnixEpochDateString()
        {
            var date = new DateTime(2023, 6, 22);
            var result = this.converter.ConvertToDbValue(date, DbDataTypeCategory.Date);
            Assert.AreEqual("unixepoch('2023-06-22')", result);
        }

        [TestMethod]
        public void ConvertToDbValue_BooleanTrue_ReturnsOne()
        {
            var result = this.converter.ConvertToDbValue(true, DbDataTypeCategory.Boolean);
            Assert.AreEqual("1", result);
        }

        [TestMethod]
        public void ConvertToDbValue_BooleanFalse_ReturnsZero()
        {
            var result = this.converter.ConvertToDbValue(false, DbDataTypeCategory.Boolean);
            Assert.AreEqual("0", result);
        }

        [TestMethod]
        public void ConvertToDbValue_Other_ReturnsToString()
        {
            var result = this.converter.ConvertToDbValue(123, DbDataTypeCategory.Int);
            Assert.AreEqual("123", result);
        }
    }
}

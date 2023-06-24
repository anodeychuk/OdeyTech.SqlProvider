// --------------------------------------------------------------------------
// <copyright file="BasicDbValueConverterTests.cs" author="Andrii Odeychuk">
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
    public class BasicDbValueConverterTests
    {
        private BasicDbValueConverter converter;

        [TestInitialize]
        public void TestInitialize() => this.converter = new BasicDbValueConverter();

        [TestMethod]
        public void ConvertToDbValue_String_ReturnsCorrectValue()
        {
            var result = this.converter.ConvertToDbValue("test", DbDataTypeCategory.String);
            Assert.AreEqual("'test'", result);
        }

        [TestMethod]
        public void ConvertToDbValue_DateTime_ReturnsCorrectValue()
        {
            var dateTime = new DateTime(2023, 6, 21, 12, 0, 0);
            var result = this.converter.ConvertToDbValue(dateTime, DbDataTypeCategory.DateTime);
            Assert.AreEqual("'2023-06-21 12:00:00'", result);
        }

        [TestMethod]
        public void ConvertToDbValue_Date_ReturnsCorrectValue()
        {
            var date = new DateTime(2023, 6, 21);
            var result = this.converter.ConvertToDbValue(date, DbDataTypeCategory.Date);
            Assert.AreEqual("'2023-06-21'", result);
        }

        [TestMethod]
        public void ConvertToDbValue_Boolean_ReturnsCorrectValue()
        {
            var result = this.converter.ConvertToDbValue(true, DbDataTypeCategory.Boolean);
            Assert.AreEqual("1", result);

            result = this.converter.ConvertToDbValue(false, DbDataTypeCategory.Boolean);
            Assert.AreEqual("0", result);
        }

        [TestMethod]
        public void ConvertToDbValue_Null_ReturnsNull()
        {
            var result = this.converter.ConvertToDbValue(null, DbDataTypeCategory.String);
            Assert.AreEqual("NULL", result);
        }

        [TestMethod]
        public void ConvertToDbValue_EmptyString_ReturnsEmptyString()
        {
            var result = this.converter.ConvertToDbValue("", DbDataTypeCategory.String);
            Assert.AreEqual("''", result);
        }

        [TestMethod]
        public void ConvertToDbValue_DateTimeWithTimeComponent_ReturnsDateOnly()
        {
            var dateTime = new DateTime(2023, 6, 21, 12, 0, 0);
            var result = this.converter.ConvertToDbValue(dateTime, DbDataTypeCategory.Date);
            Assert.AreEqual("'2023-06-21'", result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ConvertToDbValue_InvalidType_ThrowsException()
        {
            this.converter.ConvertToDbValue("2023-06-21", DbDataTypeCategory.Date);
        }

        [TestMethod]
        public void ConvertToDbValue_PerformanceTest()
        {
            DateTime startTime = DateTime.Now;

            for (var i = 0; i < 1000000; i++)
            {
                this.converter.ConvertToDbValue(i.ToString(), DbDataTypeCategory.String);
            }

            DateTime endTime = DateTime.Now;
            TimeSpan duration = endTime - startTime;

            Assert.IsTrue(duration.TotalSeconds < 5, "Performance test failed. Conversion took too long.");
        }
    }
}

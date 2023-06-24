// --------------------------------------------------------------------------
// <copyright file="SQLiteNameConverterTests.cs" author="Andrii Odeychuk">
//
// Copyright (c) Andrii Odeychuk. ALL RIGHTS RESERVED
// The entire contents of this file is protected by International Copyright Laws.
// </copyright>
// --------------------------------------------------------------------------

using Microsoft.VisualStudio.TestTools.UnitTesting;
using OdeyTech.SqlProvider.Entity.Table.Column.NameConverter;

namespace OdeyTech.SqlProvider.Test.Entity.Table.Column.NameConverter
{
    [TestClass]
    public class SQLiteNameConverterTests
    {
        [TestMethod]
        public void SQLiteDateNameConverter_ConvertName_ReturnsCorrectString()
        {
            var converter = new SQLiteDateNameConverter();
            var result = converter.ConvertName("test", "alias");
            Assert.AreEqual("date(test, 'unixepoch') AS alias", result);
        }

        [TestMethod]
        public void SQLiteDateNameConverter_ConvertName_NoAlias_ReturnsCorrectString()
        {
            var converter = new SQLiteDateNameConverter();
            var result = converter.ConvertName("test", "");
            Assert.AreEqual("date(test, 'unixepoch') AS test", result);
        }

        [TestMethod]
        public void SQLiteDateTimeNameConverter_ConvertName_ReturnsCorrectString()
        {
            var converter = new SQLiteDateTimeNameConverter();
            var result = converter.ConvertName("test", "alias");
            Assert.AreEqual("datetime(test, 'unixepoch') AS alias", result);
        }

        [TestMethod]
        public void SQLiteDateTimeNameConverter_ConvertName_NoAlias_ReturnsCorrectString()
        {
            var converter = new SQLiteDateTimeNameConverter();
            var result = converter.ConvertName("test", "");
            Assert.AreEqual("datetime(test, 'unixepoch') AS test", result);
        }

        [TestMethod]
        public void SQLiteTimeNameConverter_ConvertName_ReturnsCorrectString()
        {
            var converter = new SQLiteTimeNameConverter();
            var result = converter.ConvertName("test", "alias");
            Assert.AreEqual("time(test, 'unixepoch') AS alias", result);
        }

        [TestMethod]
        public void SQLiteTimeNameConverter_ConvertName_NoAlias_ReturnsCorrectString()
        {
            var converter = new SQLiteTimeNameConverter();
            var result = converter.ConvertName("test", "");
            Assert.AreEqual("time(test, 'unixepoch') AS test", result);
        }
    }
}

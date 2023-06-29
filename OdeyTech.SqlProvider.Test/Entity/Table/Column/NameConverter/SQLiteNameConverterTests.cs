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
        private const string ColumnName = "test";
        private const string ColumnAlias = "alias";

        [TestMethod]
        public void SQLiteDateNameConverter_ConvertName_ReturnsCorrectString()
        {
            var converter = new SQLiteDateNameConverter();
            var result = converter.ConvertName(ColumnName, ColumnAlias);
            Assert.AreEqual($"date({ColumnName}, 'unixepoch') AS {ColumnAlias}", result);
        }

        [TestMethod]
        public void SQLiteDateNameConverter_ConvertName_NoAlias_ReturnsCorrectString()
        {
            var converter = new SQLiteDateNameConverter();
            var result = converter.ConvertName(ColumnName, "");
            Assert.AreEqual($"date({ColumnName}, 'unixepoch') AS {ColumnName}", result);
        }

        [TestMethod]
        public void SQLiteDateTimeNameConverter_ConvertName_ReturnsCorrectString()
        {
            var converter = new SQLiteDateTimeNameConverter();
            var result = converter.ConvertName(ColumnName, ColumnAlias);
            Assert.AreEqual($"datetime({ColumnName}, 'unixepoch') AS {ColumnAlias}", result);
        }

        [TestMethod]
        public void SQLiteDateTimeNameConverter_ConvertName_NoAlias_ReturnsCorrectString()
        {
            var converter = new SQLiteDateTimeNameConverter();
            var result = converter.ConvertName(ColumnName, "");
            Assert.AreEqual($"datetime({ColumnName}, 'unixepoch') AS {ColumnName}", result);
        }

        [TestMethod]
        public void SQLiteTimeNameConverter_ConvertName_ReturnsCorrectString()
        {
            var converter = new SQLiteTimeNameConverter();
            var result = converter.ConvertName(ColumnName, ColumnAlias);
            Assert.AreEqual($"time({ColumnName}, 'unixepoch') AS {ColumnAlias}", result);
        }

        [TestMethod]
        public void SQLiteTimeNameConverter_ConvertName_NoAlias_ReturnsCorrectString()
        {
            var converter = new SQLiteTimeNameConverter();
            var result = converter.ConvertName(ColumnName, "");
            Assert.AreEqual($"time({ColumnName}, 'unixepoch') AS {ColumnName}", result);
        }
    }
}

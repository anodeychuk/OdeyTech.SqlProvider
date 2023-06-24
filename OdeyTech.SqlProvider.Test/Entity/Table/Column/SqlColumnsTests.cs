// --------------------------------------------------------------------------
// <copyright file="SqlColumnsTests.cs" author="Andrii Odeychuk">
//
// Copyright (c) Andrii Odeychuk. ALL RIGHTS RESERVED
// The entire contents of this file is protected by International Copyright Laws.
// </copyright>
// --------------------------------------------------------------------------

using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OdeyTech.ProductivityKit;
using OdeyTech.SqlProvider.Entity.Table.Column;
using OdeyTech.SqlProvider.Entity.Table.Column.Constraint;
using OdeyTech.SqlProvider.Entity.Table.Column.DataType;
using OdeyTech.SqlProvider.Entity.Table.Column.ValueConverter;
using OdeyTech.SqlProvider.Enum;

namespace OdeyTech.SqlProvider.Test.Entity.Table.Column
{
    [TestClass]
    public class SqlColumnsTests
    {
        private const string ColumnName = "test";
        private const string ColumnValue = "value";

        [TestMethod]
        public void AddColumn_AddsColumnCorrectly()
        {
            var columns = new SqlColumns();
            columns.Add(ColumnName, new SQLiteDataType(SQLiteDataType.DataType.Text), new SQLiteValueConverter());
            Assert.AreEqual(1, columns.Count);
        }

        [TestMethod]
        public void GetColumn_ReturnsCorrectColumn()
        {
            var columns = new SqlColumns();
            columns.Add(ColumnName, new SQLiteDataType(SQLiteDataType.DataType.Text), new SQLiteValueConverter());
            SqlColumn column = columns.Get(ColumnName);
            Assert.IsNotNull(column);
            Assert.AreEqual(ColumnName, column.GetName());
        }

        [TestMethod]
        public void SetValue_SetsValueCorrectly()
        {
            var columns = new SqlColumns();
            columns.Add(ColumnName, new SQLiteDataType(SQLiteDataType.DataType.Text), new SQLiteValueConverter());
            columns.SetValue(ColumnName, ColumnValue);
            SqlColumn column = columns.Get(ColumnName);
            Assert.AreEqual($"'{ColumnValue}'", column.GetValue());
        }

        [TestMethod]
        public void GetColumnsValue_ReturnsCorrectString()
        {
            var columns = new SqlColumns();
            columns.Add(ColumnName, new SQLiteDataType(SQLiteDataType.DataType.Text), new SQLiteValueConverter());
            columns.SetValue(ColumnName, ColumnValue);
            var result = columns.GetColumnsValue();
            Assert.AreEqual($"{ColumnName} = '{ColumnValue}'", result);
        }

        [TestMethod]
        public void GetColumnsName_ReturnsCorrectString()
        {
            var columns = new SqlColumns();
            columns.Add(ColumnName, new SQLiteDataType(SQLiteDataType.DataType.Text), new SQLiteValueConverter());
            var result = columns.GetColumnsName();
            Assert.AreEqual(ColumnName, result);
        }

        [TestMethod]
        public void GetColumnsDataType_ReturnsCorrectString()
        {
            var columns = new SqlColumns();
            columns.Add(ColumnName, new SQLiteDataType(SQLiteDataType.DataType.Text), new SQLiteValueConverter());
            var result = columns.GetColumnsDataType();
            Assert.AreEqual($"{ColumnName} TEXT", result);
        }

        [TestMethod]
        public void GetValues_ReturnsCorrectString()
        {
            var columns = new SqlColumns();
            columns.Add(ColumnName, new SQLiteDataType(SQLiteDataType.DataType.Text), new SQLiteValueConverter());
            columns.SetValue(ColumnName, ColumnValue);
            var result = columns.GetValues();
            Assert.AreEqual($"'{ColumnValue}'", result);
        }

        [TestMethod]
        public void Clear_ClearsColumns()
        {
            var columns = new SqlColumns();
            columns.Add(ColumnName, new SQLiteDataType(SQLiteDataType.DataType.Text), new SQLiteValueConverter());
            columns.Clear();
            Assert.AreEqual(0, columns.Count);
        }

        [TestMethod]
        public void AddConstraints_AddsConstraintsCorrectly()
        {
            var columns = new SqlColumns();
            columns.Add(ColumnName, new SQLiteDataType(SQLiteDataType.DataType.Text), new SQLiteValueConverter());
            columns.AddConstraints(new PrimaryKeyConstraint { ConstraintName = "PK_Test", ColumnNames = new List<string> { ColumnName } });

            // Use reflection to access the private 'constraints' field
            var constraints = (List<IConstraint>)Accessor.GetFieldValue(columns, "constraints");

            Assert.AreEqual(1, constraints.Count);
            Assert.AreEqual("PK_Test", constraints[0].ConstraintName);
            Assert.AreEqual(ColumnName, ((PrimaryKeyConstraint)constraints[0]).ColumnNames[0]);
        }
    }
}

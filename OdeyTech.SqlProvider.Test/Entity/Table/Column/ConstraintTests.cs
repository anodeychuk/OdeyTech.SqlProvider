// --------------------------------------------------------------------------
// <copyright file="ConstraintTests.cs" author="Andrii Odeychuk">
//
// Copyright (c) Andrii Odeychuk. ALL RIGHTS RESERVED
// The entire contents of this file is protected by International Copyright Laws.
// </copyright>
// --------------------------------------------------------------------------

using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OdeyTech.SqlProvider.Entity.Table.Column.Constraint;

namespace OdeyTech.SqlProvider.Test.Entity.Table.Column
{
    [TestClass]
    public class ConstraintTests
    {
        [TestMethod]
        public void ForeignKeyConstraint_ToString_ReturnsCorrectString()
        {
            var constraint = new ForeignKeyConstraint
            {
                ConstraintName = "FK_Test",
                ColumnName = "TestColumn",
                ReferenceTable = "RefTable",
                ReferenceColumn = "RefColumn"
            };
            var result = constraint.ToString();
            Assert.AreEqual("CONSTRAINT FK_Test Foreign KEY (TestColumn) REFERENCES RefTable(RefColumn)", result);
        }

        [TestMethod]
        public void PrimaryKeyConstraint_ToString_ReturnsCorrectString()
        {
            var constraint = new PrimaryKeyConstraint
            {
                ConstraintName = "PK_Test",
                ColumnNames = new List<string> { "TestColumn1", "TestColumn2" }
            };
            var result = constraint.ToString();
            Assert.AreEqual("CONSTRAINT PK_Test Primary KEY (TestColumn1, TestColumn2)", result);
        }

        [TestMethod]
        public void UniqueConstraint_ToString_ReturnsCorrectString()
        {
            var constraint = new UniqueConstraint
            {
                ConstraintName = "UQ_Test",
                ColumnNames = new List<string> { "TestColumn1", "TestColumn2" }
            };
            var result = constraint.ToString();
            Assert.AreEqual("CONSTRAINT UQ_Test Unique (TestColumn1, TestColumn2)", result);
        }
    }
}

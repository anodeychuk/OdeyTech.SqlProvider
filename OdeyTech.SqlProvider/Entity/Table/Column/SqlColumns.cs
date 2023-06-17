// --------------------------------------------------------------------------
// <copyright file="SqlColumns.cs" author="Andrii Odeychuk">
//
// Copyright (c) Andrii Odeychuk. ALL RIGHTS RESERVED
// The entire contents of this file is protected by International Copyright Laws.
// </copyright>
// --------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using OdeyTech.ProductivityKit.Extension;
using OdeyTech.SqlProvider.Entity.Table.Column.Constraint;
using OdeyTech.SqlProvider.Entity.Table.Column.DataType;
using OdeyTech.SqlProvider.Entity.Table.Column.ValueConverter;
using OdeyTech.SqlProvider.Enum;

namespace OdeyTech.SqlProvider.Entity.Table.Column
{
    /// <summary>
    /// Represents the columns of a SQL table.
    /// </summary>
    public class SqlColumns : ICloneable
    {
        private Dictionary<string, SqlColumn> columnsSource;
        private List<IConstraint> constraints;

        /// <summary>
        /// Initializes a new instance of the <see cref="SqlColumns"/> class.
        /// </summary>
        public SqlColumns()
        {
            this.columnsSource = new Dictionary<string, SqlColumn>();
        }

        /// <summary>
        /// Gets the number of columns.
        /// </summary>
        public int Count => this.columnsSource.Count;

        private IEnumerable<KeyValuePair<string, SqlColumn>> ActiveColumns => this.columnsSource.Where(c => !c.Value.IsExcluded);

        /// <summary>
        /// Adds a new column to the <see cref="SqlColumns"/> instance using the specified parameters.
        /// </summary>
        /// <param name="columnName">The name of the column to be added. This should be unique within the <see cref="SqlColumns"/> instance.</param>
        /// <param name="dataType">The data type of the column to be added. This defines the type of data the column can hold.</param>
        /// <param name="valueConverter">An optional parameter that specifies the value converter for the column. This is used to convert the column's value to a specific format or data type.</param>
        /// <exception cref="ArgumentException">Thrown when a column with the same name already exists in the <see cref="SqlColumns"/> instance.</exception>

        public void AddColumn(string columnName, IDbDataType dataType, IDbValueConverter valueConverter = null)
            => AddColumn(new SqlColumn(columnName, dataType, null, valueConverter));

        /// <summary>
        /// Adds a new <see cref="SqlColumn"/> to the <see cref="SqlColumns"/> instance.
        /// </summary>
        /// <param name="column">The <see cref="SqlColumn"/> to be added. This should be a valid instance of <see cref="SqlColumn"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when the provided <see cref="SqlColumn"/> is null.</exception>
        /// <exception cref="ArgumentException">Thrown when a column with the same name already exists in the <see cref="SqlColumns"/> instance.</exception>
        public void AddColumn(SqlColumn column)
        {
            if (column == null)
            {
                throw new ArgumentNullException(nameof(column));
            }

            if (this.columnsSource.ContainsKey(column.GetName()))
            {
                throw new ArgumentException($"A column with the name {column.GetName()} already exists.");
            }

            this.columnsSource.Add(column.GetName(), column);
        }

        /// <summary>
        /// Retrieves a <see cref="SqlColumn"/> from the <see cref="SqlColumns"/> instance by its name.
        /// </summary>
        /// <param name="columnName">The name of the column to retrieve.</param>
        /// <returns>The <see cref="SqlColumn"/> with the specified name.</returns>
        /// <exception cref="ArgumentException">Thrown when no column with the specified name exists in the <see cref="SqlColumns"/> instance.</exception>
        public SqlColumn GetColumn(string columnName)
            => this.columnsSource.TryGetValue(columnName, out SqlColumn column)
                ? column
                : throw new ArgumentException($"No column with the name {columnName} exists.");

        /// <summary>
        /// Sets the value of a specific column in the <see cref="SqlColumns"/> instance.
        /// </summary>
        /// <param name="columnName">The name of the column for which the value is to be set.</param>
        /// <param name="value">The value to be set for the specified column.</param>
        /// <exception cref="ArgumentException">Thrown when no column with the specified name exists in the <see cref="SqlColumns"/> instance.</exception>
        public void SetValue(string columnName, object value)
        {
            if (!this.columnsSource.ContainsKey(columnName))
            {
                throw new ArgumentException($"No column with the name {nameof(columnName)} exists.");
            }

            this.columnsSource[columnName].SetValue(value);
        }

        /// <summary>
        /// Gets the column names and their values in the format "columnName1 = value1, columnName2 = value2, ...".
        /// </summary>
        /// <returns>A string representation of the column names and their values.</returns>
        public string GetColumnsValue() => string.Join(", ", ActiveColumns.Select(p => $"{p.Value.GetName(SqlQueryType.Update)} = {p.Value.GetValue()}"));

        /// <summary>
        /// Gets the names of the columns, separated by commas.
        /// </summary>
        /// <param name="sqlQueryType">The type of the SQL query.</param>
        /// <returns>A string representation of the column names.</returns>
        public string GetColumnsName(SqlQueryType sqlQueryType) => string.Join(", ", ActiveColumns.Select(p => p.Value.GetName(sqlQueryType)));

        /// <summary>
        /// Gets the column names and their data types for a create query.
        /// </summary>
        /// <returns>A string representation of the column names and their data types.</returns>
        public string GetColumnsDataType()
        {
            var columns = string.Join(", ", this.columnsSource.Select(p => $"{p.Value.GetName()} {p.Value.DataType}"));
            return this.constraints.IsNullOrEmpty() ? columns : $"{columns}, {string.Join(", ", this.constraints)}";
        }

        /// <summary>
        /// Gets the values of the columns, separated by commas.
        /// </summary>
        /// <returns>A string representation of the column values.</returns>
        public string GetValues() => string.Join(", ", this.columnsSource.Select(p => p.Value.GetValue()));

        /// <summary>
        /// Creates a copy of this <see cref="SqlColumns"/> object.
        /// </summary>
        /// <returns>A new SqlColumns object with the same column values.</returns>
        public object Clone() => new SqlColumns { columnsSource = new(this.columnsSource) };

        /// <summary>
        /// Removes all columns and excluded columns.
        /// </summary>
        public void Clear() => this.columnsSource.Clear();

        /// <summary>
        /// Adds constraints to the SqlColumns.
        /// </summary>
        /// <param name="constraints">The constraints to add.</param>
        public void AddConstraints(params IConstraint[] constraints)
        {
            if (this.constraints.IsNullOrEmpty())
            {
                this.constraints = new List<IConstraint>(constraints);
            }
            else
            {
                this.constraints.AddRange(constraints);
            }
        }
    }
}

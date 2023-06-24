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
        private readonly Dictionary<string, SqlColumn> columnsSource;
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
        public void Add(string columnName, IDbDataType dataType, IDbValueConverter valueConverter = null)
            => Add(new SqlColumn(columnName, dataType, null, valueConverter));

        /// <summary>
        /// Adds a new <see cref="SqlColumn"/> to the <see cref="SqlColumns"/> instance.
        /// </summary>
        /// <param name="column">The <see cref="SqlColumn"/> to be added. This should be a valid instance of <see cref="SqlColumn"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when the provided <see cref="SqlColumn"/> is null.</exception>
        /// <exception cref="ArgumentException">Thrown when a column with the same name already exists in the <see cref="SqlColumns"/> instance.</exception>
        public void Add(SqlColumn column)
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
        public SqlColumn Get(string columnName)
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
        /// Gets a string representation of the active columns and their values in the format "columnName1 = value1, columnName2 = value2, ...".
        /// </summary>
        /// <returns>A string representation of the active column names and their values.</returns>
        /// <exception cref="ArgumentException">Thrown when there are no active columns.</exception>
        public string GetColumnsValue()
        {
            CheckNumberActiveColumns();
            return string.Join(", ", ActiveColumns.Select(p => $"{p.Value.GetName(SqlQueryType.Update)} = {p.Value.GetValue()}"));
        }

        /// <summary>
        /// Gets a string representation of the active column names, separated by commas.
        /// </summary>
        /// <param name="sqlQueryType">The type of the SQL query. This determines how the column names are formatted.</param>
        /// <returns>A string representation of the active column names.</returns>
        /// <exception cref="ArgumentException">Thrown when there are no active columns.</exception>
        public string GetColumnsName(SqlQueryType sqlQueryType = SqlQueryType.Create)
        {
            CheckNumberActiveColumns();
            return string.Join(", ", ActiveColumns.Select(p => p.Value.GetName(sqlQueryType)));
        }

        /// <summary>
        /// Gets a string representation of the column names and their data types, formatted for a CREATE TABLE query.
        /// </summary>
        /// <returns>A string representation of the column names and their data types.</returns>
        /// <exception cref="ArgumentException">Thrown when there are no columns.</exception>
        public string GetColumnsDataType()
        {
            CheckNumberColumns();
            var columns = string.Join(", ", this.columnsSource.Select(p => $"{p.Value.GetName()} {p.Value.DataType.ToString().ToUpper()}"));
            return this.constraints.IsNullOrEmpty() ? columns : $"{columns}, {string.Join(", ", this.constraints)}";
        }

        /// <summary>
        /// Gets a string representation of the column values, separated by commas.
        /// </summary>
        /// <returns>A string representation of the column values.</returns>
        /// <exception cref="ArgumentException">Thrown when there are no columns.</exception>
        public string GetValues()
        {
            CheckNumberColumns();
            return string.Join(", ", this.columnsSource.Select(p => p.Value.GetValue()));
        }

        /// <inheritdoc/>
        public object Clone()
        {
            var columns = new SqlColumns();
            this.columnsSource.ForEach(c => columns.Add((SqlColumn)c.Value.Clone()));
            return columns;
        }

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

        private void CheckNumberColumns()
        {
            if (this.columnsSource.Count == 0)
            {
                throw new ArgumentException("The number of columns is 0");
            }
        }

        private void CheckNumberActiveColumns()
        {
            CheckNumberColumns();
            if (this.ActiveColumns.Count() == 0)
            {
                throw new ArgumentException("The number of active columns is 0");
            }
        }
    }
}

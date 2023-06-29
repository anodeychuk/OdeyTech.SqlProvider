// --------------------------------------------------------------------------
// <copyright file="SqlTable.cs" author="Andrii Odeychuk">
//
// Copyright (c) Andrii Odeychuk. ALL RIGHTS RESERVED
// The entire contents of this file is protected by International Copyright Laws.
// </copyright>
// --------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using OdeyTech.ProductivityKit;
using OdeyTech.ProductivityKit.Extension;
using OdeyTech.SqlProvider.Entity.Table.Column;
using OdeyTech.SqlProvider.Enum;

namespace OdeyTech.SqlProvider.Entity.Table
{
    /// <summary>
    /// Represents a SQL table in a database.
    /// </summary>
    public class SqlTable : ICloneable
    {
        private string tableName;
        private string tablePrefix;
        private List<string> joins;
        private List<string> conditions;
        private List<string> orderBy;

        /// <summary>
        /// Gets or sets the columns of the SQL table.
        /// </summary>
        public SqlColumns Columns { get; private set; } = new();

        /// <summary>
        /// Sets the name and prefix of the SQL table.
        /// </summary>
        /// <param name="tableName">The name of the table.</param>
        /// <param name="tablePrefix">The prefix of the table.</param>
        /// <exception cref="ArgumentException">Thrown when table name is null.</exception>
        public void SetName(string tableName, string tablePrefix = null)
        {
            ThrowHelper.ThrowIfNullOrEmpty(tableName, nameof(tableName), "The table name cannot be null");

            this.tableName = tableName;
            this.tablePrefix = tablePrefix;
        }

        /// <summary>
        /// Gets the name of the SQL table.
        /// </summary>
        /// <param name="withPrefix">Indicates whether to include the table prefix in the name.</param>
        /// <returns>The table name.</returns>
        public string GetName(bool withPrefix = false)
        {
            var prefix = withPrefix && !string.IsNullOrEmpty(this.tablePrefix) ? $" {this.tablePrefix}" : string.Empty;
            return $"{this.tableName}{prefix}";
        }

        /// <summary>
        /// Clears the join statements associated with the SQL table.
        /// </summary>
        public void ClearJoins() => this.joins = null;

        /// <summary>
        /// Adds join statements to the SQL table.
        /// </summary>
        /// <param name="joins">The join statements to add.</param>
        public void AddJoins(params string[] joins)
        {
            if (this.joins.IsNullOrEmpty())
            {
                this.joins = new List<string>(joins);
            }
            else
            {
                this.joins.AddRange(joins);
            }
        }

        /// <summary>
        /// Gets the join statements associated with the SQL table.
        /// </summary>
        /// <returns>The join statements.</returns>
        public string GetJoins()
             => this.joins.IsNullOrEmpty()
                ? string.Empty
                : $" {string.Join(" ", this.joins)}";

        /// <summary>
        /// Clears the condition statements associated with the SQL table.
        /// </summary>
        public void ClearConditions() => this.conditions = null;

        /// <summary>
        /// Adds condition statements to the SQL table.
        /// </summary>
        /// <param name="conditions">The condition statements to add.</param>
        public void AddConditions(params string[] conditions)
        {
            if (this.conditions.IsNullOrEmpty())
            {
                this.conditions = new List<string>(conditions);
            }
            else
            {
                this.conditions.AddRange(conditions);
            }
        }

        /// <summary>
        /// Gets the condition statements associated with the SQL table.
        /// </summary>
        /// <returns>The condition statements.</returns>
        public string GetConditions()
             => this.conditions.IsNullOrEmpty()
                ? string.Empty
                : $" WHERE {string.Join(" AND ", this.conditions)}";

        /// <summary>
        /// Clears the order by statements associated with the SQL table.
        /// </summary>
        public void ClearOrderBy() => this.orderBy = null;

        /// <summary>
        /// Adds order by statements to the SQL table.
        /// </summary>
        /// <param name="orderBy">The order by statements to add.</param>
        public void AddOrderBy(params string[] orderBy)
        {
            if (this.orderBy.IsNullOrEmpty())
            {
                this.orderBy = new List<string>(orderBy);
            }
            else
            {
                this.orderBy.AddRange(orderBy);
            }
        }

        /// <summary>
        /// Gets the order by statements associated with the SQL table.
        /// </summary>
        /// <returns>The order by statements.</returns>
        public string GetOrderBy()
            => this.orderBy.IsNullOrEmpty()
                ? string.Empty
                : $" ORDER BY {string.Join(", ", this.orderBy)}";

        /// <summary>
        /// Validates the SQL table for a specific SQL query type.
        /// </summary>
        /// <param name="sqlType">The type of the SQL query.</param>
        /// <exception cref="ArgumentException">Thrown when table_name is null or columns is null or number of columns is 0.</exception>
        public void Validate(SqlQueryType sqlType)
        {
            ThrowHelper.ThrowIfNullOrEmpty(this.tableName, nameof(this.tableName));

            if (sqlType is SqlQueryType.Insert or SqlQueryType.Update && (Columns == null || Columns.Count == 0))
            {
                throw new ArgumentException(nameof(Columns));
            }
        }

        /// <summary>
        /// Creates a deep copy of the SQL table.
        /// </summary>
        /// <returns>A copy of the SQL table.</returns>
        public object Clone()
            => new SqlTable
            {
                tableName = this.tableName,
                tablePrefix = this.tablePrefix,
                Columns = (SqlColumns)Columns.Clone(),
                joins = this.joins,
                conditions = this.conditions,
                orderBy = this.orderBy
            };

        /// <summary>
        /// Determines whether the specified object is equal to the current object based on the hash code.
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns>True if the objects are equal based on the hash code, otherwise false.</returns>
        public override bool Equals(object obj) => obj is SqlTable query && query.GetHashCode() == GetHashCode();

        /// <summary>
        /// Gets the hash code of the SQL table based on its properties.
        /// </summary>
        /// <returns>The hash code of the SQL table.</returns>
        public override int GetHashCode() => (this.tableName, this.tablePrefix, Columns.GetColumnsDataType(), GetJoins(), GetConditions(), GetOrderBy()).GetHashCode();
    }
}

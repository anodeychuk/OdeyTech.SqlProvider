// --------------------------------------------------------------------------
// <copyright file="BasicQueryGenerator.cs" author="Andrii Odeychuk">
//
// Copyright (c) Andrii Odeychuk. ALL RIGHTS RESERVED
// The entire contents of this file is protected by International Copyright Laws.
// </copyright>
// --------------------------------------------------------------------------

using System;
using OdeyTech.ProductivityKit;
using OdeyTech.SqlProvider.Entity.Table;
using OdeyTech.SqlProvider.Enum;

namespace OdeyTech.SqlProvider.Query
{
    /// <summary>
    /// SQL queries generator.
    /// </summary>
    internal class BasicQueryGenerator : ISqlQueryGenerator
    {
        /// <inheritdoc/>
        /// <exception cref="ArgumentNullException">Thrown when the table is null.</exception>
        public string Create(SqlTable table)
        {
            ThrowHelper.ThrowIfNull(table, nameof(table));
            table.Validate(SqlQueryType.Create);
            return $"CREATE TABLE {table.GetName()} ({table.Columns.GetColumnsDataType()});";
        }

        /// <inheritdoc/>
        /// <exception cref="ArgumentNullException">Thrown when the table is null.</exception>
        public string Select(SqlTable table)
        {
            ThrowHelper.ThrowIfNull(table, nameof(table));
            table.Validate(SqlQueryType.Select);
            return $"SELECT {table.Columns.GetColumnsName(SqlQueryType.Select)} FROM {table.GetName(true)}{table.GetJoins()}{table.GetConditions()}{table.GetOrderBy()};";
        }

        /// <inheritdoc/>
        /// <exception cref="ArgumentNullException">Thrown when the table is null.</exception>
        public string Insert(SqlTable table)
        {
            ThrowHelper.ThrowIfNull(table, nameof(table));
            table.Validate(SqlQueryType.Insert);
            return $"INSERT INTO {table.GetName()} ({table.Columns.GetColumnsName(SqlQueryType.Insert)}) VALUES ({table.Columns.GetValues()});";
        }

        /// <inheritdoc/>
        /// <exception cref="ArgumentNullException">Thrown when the table is null.</exception> 
        public string Update(SqlTable table)
        {
            ThrowHelper.ThrowIfNull(table, nameof(table));
            table.Validate(SqlQueryType.Update);
            var conditions = table.GetConditions();
            ThrowHelper.ThrowIfNullOrEmpty(conditions, nameof(conditions), "The condition is not set");
            return $"UPDATE {table.GetName()} SET {table.Columns.GetColumnsValue()}{table.GetConditions()};";
        }

        /// <inheritdoc/>
        /// <exception cref="ArgumentNullException">Thrown when the table is null.</exception>
        /// <exception cref="ArgumentException">Thrown when the condition is not set or is null or empty.</exception>
        public string Delete(SqlTable table)
        {
            ThrowHelper.ThrowIfNull(table, nameof(table));
            table.Validate(SqlQueryType.Delete);
            var conditions = table.GetConditions();
            ThrowHelper.ThrowIfNullOrEmpty(conditions, nameof(conditions), "The condition is not set");
            return $"DELETE FROM {table.GetName()}{conditions};";
        }
    }
}

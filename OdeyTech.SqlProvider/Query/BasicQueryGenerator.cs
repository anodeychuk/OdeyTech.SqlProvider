// --------------------------------------------------------------------------
// <copyright file="BasicQueryGenerator.cs" author="Andrii Odeychuk">
//
// Copyright (c) Andrii Odeychuk. ALL RIGHTS RESERVED
// The entire contents of this file is protected by International Copyright Laws.
// </copyright>
// --------------------------------------------------------------------------

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
        public string Create(SqlTable table)
        {
            table.Validate(SqlQueryType.Create);
            return $"CREATE TABLE {table.GetName()} ({table.Columns.GetColumnsDataType()});";
        }

        /// <inheritdoc/>
        public string Select(SqlTable table)
        {
            table.Validate(SqlQueryType.Select);
            return $"SELECT {table.Columns.GetColumnsName(SqlQueryType.Select)} FROM {table.GetName(true)}{table.GetJoins()}{table.GetConditions()}{table.GetOrderBy()};";
        }

        /// <inheritdoc/>
        public string Insert(SqlTable table)
        {
            table.Validate(SqlQueryType.Insert);
            return $"INSERT INTO {table.GetName()} ({table.Columns.GetColumnsName(SqlQueryType.Insert)}) VALUES ({table.Columns.GetValues()});";
        }

        /// <inheritdoc/>
        public string Update(SqlTable table)
        {
            table.Validate(SqlQueryType.Update);
            return $"UPDATE {table.GetName()} SET {table.Columns.GetColumnsValue()}{table.GetConditions()};";
        }

        /// <inheritdoc/>
        public string Delete(SqlTable table)
        {
            table.Validate(SqlQueryType.Delete);
            return $"DELETE FROM {table.GetName()}{table.GetConditions()};";
        }
    }
}

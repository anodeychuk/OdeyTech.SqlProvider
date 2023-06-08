// --------------------------------------------------------------------------
// <copyright file="SqlQueryGenerator.cs" author="Andrii Odeychuk">
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
    public static class SqlQueryGenerator
    {
        /// <summary>
        /// Generates a CREATE TABLE query.
        /// </summary>
        /// <param name="table">The <see cref="SqlTable"/> representing the table to create.</param>
        /// <returns>A string representation of the generated query.</returns>
        public static string Create(SqlTable table)
        {
            table.Validate(SqlQueryType.Create);
            return $"CREATE TABLE {table.GetName()} ({table.Columns.GetColumnsDataType()});";
        }

        /// <summary>
        /// Generates a SELECT query.
        /// </summary>
        /// <param name="table">The <see cref="SqlTable"/> to generate the query from.</param>
        /// <returns>A string representation of the generated query.</returns>
        public static string Select(SqlTable table)
        {
            table.Validate(SqlQueryType.Select);
            return $"SELECT {table.Columns.GetColumnsName(SqlQueryType.Select)} FROM {table.GetName(true)}{table.GetJoins()}{table.GetConditions()}{table.GetOrderBy()};";
        }

        /// <summary>
        /// Generates an INSERT query.
        /// </summary>
        /// <param name="table">The <see cref="SqlTable"/> to generate the query from.</param>
        /// <returns>A string representation of the generated query.</returns>
        public static string Insert(SqlTable table)
        {
            table.Validate(SqlQueryType.Insert);
            return $"INSERT INTO {table.GetName()} ({table.Columns.GetColumnsName(SqlQueryType.Insert)}) VALUES ({table.Columns.GetValues()});";
        }

        /// <summary>
        /// Generates an UPDATE query.
        /// </summary>
        /// <param name="table">The <see cref="SqlTable"/> to generate the query from.</param>
        /// <returns>A string representation of the generated query.</returns>
        public static string Update(SqlTable table)
        {
            table.Validate(SqlQueryType.Update);
            return $"UPDATE {table.GetName()} SET {table.Columns.GetColumnsValue()}{table.GetConditions()};";
        }

        /// <summary>
        /// Generates a DELETE query.
        /// </summary>
        /// <param name="table">The <see cref="SqlTable"/> to generate the query from.</param>
        /// <returns>A string representation of the generated query.</returns>
        public static string Delete(SqlTable table)
        {
            table.Validate(SqlQueryType.Delete);
            return $"DELETE FROM {table.GetName()}{table.GetConditions()};";
        }
    }
}

// --------------------------------------------------------------------------
// <copyright file="SqlQueryGenerator.cs" author="Andrii Odeychuk">
//
// Copyright (c) Andrii Odeychuk. ALL RIGHTS RESERVED
// The entire contents of this file is protected by International Copyright Laws.
// </copyright>
// --------------------------------------------------------------------------

using OdeyTech.SqlProvider.Entity.Table;

namespace OdeyTech.SqlProvider.Query
{
    public interface ISqlQueryGenerator
    {
        /// <summary>
        /// Generates a CREATE TABLE query.
        /// </summary>
        /// <param name="table">The <see cref="SqlTable"/> representing the table to create.</param>
        /// <returns>A string representation of the generated query.</returns>
        string Create(SqlTable table);

        /// <summary>
        /// Generates a DELETE query.
        /// </summary>
        /// <param name="table">The <see cref="SqlTable"/> to generate the query from.</param>
        /// <returns>A string representation of the generated query.</returns>
        string Delete(SqlTable table);

        /// <summary>
        /// Generates an INSERT query.
        /// </summary>
        /// <param name="table">The <see cref="SqlTable"/> to generate the query from.</param>
        /// <returns>A string representation of the generated query.</returns>
        string Insert(SqlTable table);

        /// <summary>
        /// Generates a SELECT query.
        /// </summary>
        /// <param name="table">The <see cref="SqlTable"/> to generate the query from.</param>
        /// <returns>A string representation of the generated query.</returns>
        string Select(SqlTable table);

        /// <summary>
        /// Generates an UPDATE query.
        /// </summary>
        /// <param name="table">The <see cref="SqlTable"/> to generate the query from.</param>
        /// <returns>A string representation of the generated query.</returns>
        string Update(SqlTable table);
    }
}

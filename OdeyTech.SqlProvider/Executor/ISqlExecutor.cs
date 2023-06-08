// --------------------------------------------------------------------------
// <copyright file="ISqlExecutor.cs" author="Andrii Odeychuk">
//
// Copyright (c) Andrii Odeychuk. ALL RIGHTS RESERVED
// The entire contents of this file is protected by International Copyright Laws.
// </copyright>
// --------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace OdeyTech.SqlProvider.Executor
{
    /// <summary>
    /// Represents an SQL query executor.
    /// </summary>
    public interface ISqlExecutor : IDisposable
    {
        /// <summary>
        /// Executes multiple SQL queries that are insert, update, or delete statements.
        /// </summary>
        /// <param name="queries">The collection of SQL queries to execute.</param>
        /// <param name="parameters">The parameters for the queries.</param>
        void Query(IEnumerable<string> queries, params DbParameter[] parameters);

        /// <summary>
        /// Executes a single SQL query that is an insert, update, or delete statement.
        /// </summary>
        /// <param name="query">The SQL query to execute.</param>
        /// <param name="parameters">The parameters for the query.</param>
        void Query(string query, params DbParameter[] parameters);

        /// <summary>
        /// Executes an SQL query that selects data from a database.
        /// </summary>
        /// <param name="query">The SQL query to execute.</param>
        /// <param name="parameters">The parameters for the query.</param>
        /// <returns>The selected data as a <see cref="DataTable"/> object.</returns>
        DataTable Select(string query, params DbParameter[] parameters);

        /// <summary>
        /// Executes a stored function in a database.
        /// </summary>
        /// <param name="storeFunctionName">The name of the stored function to execute.</param>
        /// <param name="parameters">The parameters for the stored function.</param>
        /// <returns>The result returned by the stored function.</returns>
        object StoreFunction(string storeFunctionName, params DbParameter[] parameter);

        /// <summary>
        /// Executes a stored procedure in a database.
        /// </summary>
        /// <param name="storeProcedureName">The name of the stored procedure to execute.</param>
        /// <param name="parameters">The parameters for the stored procedure.</param>
        /// <returns>A list of output parameters returned by the stored procedure.</returns>
        List<DbParameter> StoreProcedure(string storeProcedureName, params DbParameter[] parameters);
    }
}

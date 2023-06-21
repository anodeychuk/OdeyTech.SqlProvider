// --------------------------------------------------------------------------
// <copyright file="SqlServerChecker.cs" author="Andrii Odeychuk">
//
// Copyright (c) Andrii Odeychuk. ALL RIGHTS RESERVED
// The entire contents of this file is protected by International Copyright Laws.
// </copyright>
// --------------------------------------------------------------------------

using System;
using System.Data;

namespace OdeyTech.SqlProvider.Entity.Database.Checker
{
    /// <summary>
    /// Represents a base class for checking the existence of a database and its items.
    /// </summary>
    internal class SqlServerChecker : DbChecker
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SqlServerChecker"/> class with the specified database connection.
        /// </summary>
        /// <param name="dbConnection">The connection to the SQL Server database to check.</param>
        public SqlServerChecker(IDbConnection dbConnection) : base(DatabaseType.SqlServer, dbConnection)
        { }

        /// <inheritdoc/>
        protected override bool CheckDatabaseItemExistInternal(string itemName)
        {
            using IDbCommand command = DbConnection.CreateCommand();
            command.CommandText = "SELECT COUNT(*) FROM sys.tables WHERE name = @tableName";
            IDbDataParameter parameter = command.CreateParameter();
            parameter.ParameterName = "@tableName";
            parameter.DbType = DbType.String;
            parameter.Value = itemName;
            command.Parameters.Add(parameter);
            var count = Convert.ToInt32(command.ExecuteScalar());
            return count > 0;
        }
    }
}

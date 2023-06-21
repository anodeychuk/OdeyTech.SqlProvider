// --------------------------------------------------------------------------
// <copyright file="MySqlChecker.cs" author="Andrii Odeychuk">
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
    internal class MySqlChecker : DbChecker
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MySqlChecker"/> class with the specified database connection.
        /// </summary>
        /// <param name="dbConnection">The connection to the MySQL database to check.</param>
        public MySqlChecker(IDbConnection dbConnection) : base(DatabaseType.MySql, dbConnection)
        { }

        /// <inheritdoc/>
        protected override bool CheckDatabaseItemExistInternal(string itemName)
        {
            using IDbCommand command = DbConnection.CreateCommand();
            command.CommandText = "SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = @tableName";
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

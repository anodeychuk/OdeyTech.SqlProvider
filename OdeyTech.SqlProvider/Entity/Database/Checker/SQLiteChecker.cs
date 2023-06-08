// --------------------------------------------------------------------------
// <copyright file="SQLiteChecker.cs" author="Andrii Odeychuk">
//
// Copyright (c) Andrii Odeychuk. ALL RIGHTS RESERVED
// The entire contents of this file is protected by International Copyright Laws.
// </copyright>
// --------------------------------------------------------------------------

using System;
using System.Data;
using System.IO;

namespace OdeyTech.SqlProvider.Entity.Database.Checker
{
    /// <summary>
    /// Represents a base class for checking the existence of a database and its items.
    /// </summary>
    public class SQLiteChecker : DbChecker
    {
        /// <inheritdoc/>
        protected override bool CheckDatabaseFileExists()
        {
            var databasePath = GetConnectionStringDataSource();
            return File.Exists(databasePath);
        }

        /// <inheritdoc/>
        protected override bool CheckDatabaseItemExistInternal(string itemName)
        {
            using IDbCommand command = DbConnection.CreateCommand();
            command.CommandText = "SELECT COUNT(*) FROM sqlite_master WHERE type='table' AND name=@tableName";
            IDbDataParameter parameter = command.CreateParameter();
            parameter.ParameterName = "@tableName";
            parameter.DbType = DbType.String;
            parameter.Value = itemName;
            command.Parameters.Add(parameter);
            var count = Convert.ToInt32(command.ExecuteScalar());
            return count > 0;
        }

        /// <inheritdoc/>
        protected override void CreateDatabase()
        {
            var databasePath = GetConnectionStringDataSource();
            using FileStream _ = File.Create(databasePath);
        }

        /// <summary>
        /// Retrieves the data source from the connection string.
        /// </summary>
        /// <returns>The data source (file path) from the connection string.</returns>
        /// <exception cref="InvalidOperationException">Thrown when the connection string does not contain the "Data Source=" keyword.</exception>
        private string GetConnectionStringDataSource()
        {
            var connectionString = DbConnection.ConnectionString;
            var dataSourceKeyword = "Data Source=";
            var dataSourceStartIndex = connectionString.IndexOf(dataSourceKeyword);
            if (dataSourceStartIndex >= 0)
            {
                var filePathStartIndex = dataSourceStartIndex + dataSourceKeyword.Length;
                var filePathEndIndex = connectionString.IndexOf(';', filePathStartIndex);
                return filePathEndIndex >= 0
                  ? connectionString.Substring(filePathStartIndex, filePathEndIndex - filePathStartIndex)
                  : connectionString.Substring(filePathStartIndex);
            }

            throw new InvalidOperationException("Invalid connection string: Data Source keyword not found.");
        }
    }
}

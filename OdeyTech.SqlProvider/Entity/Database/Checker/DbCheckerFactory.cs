// --------------------------------------------------------------------------
// <copyright file="DbCheckerFactory.cs" author="Andrii Odeychuk">
//
// Copyright (c) Andrii Odeychuk. ALL RIGHTS RESERVED
// The entire contents of this file is protected by International Copyright Laws.
// </copyright>
// --------------------------------------------------------------------------

using System;
using System.Data;
using OdeyTech.ProductivityKit;

namespace OdeyTech.SqlProvider.Entity.Database.Checker
{
    /// <summary>
    /// Provides a factory method for creating instances of <see cref="IDbChecker"/> based on the specified <see cref="DatabaseType"/>.
    /// </summary>
    public static class DbCheckerFactory
    {
        /// <summary>
        /// Returns an instance of <see cref="IDbChecker"/> that corresponds to the specified <see cref="DatabaseType"/>.
        /// </summary>
        /// <param name="databaseType">The type of the database for which to get the <see cref="IDbChecker"/>.</param>
        /// <param name="dbConnection">The connection to the database for which to get the <see cref="IDbChecker"/>.</param>
        /// <returns>An instance of <see cref="IDbChecker"/> that corresponds to the specified <see cref="DatabaseType"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the dbConnection is null.</exception>
        /// <exception cref="ArgumentException">Thrown when there is no <see cref="IDbChecker"/> corresponding to the specified <see cref="DatabaseType"/>.</exception>
        public static IDbChecker GetDbChecker(DatabaseType databaseType, IDbConnection dbConnection)
        {
            ThrowHelper.ThrowIfNull(dbConnection, nameof(dbConnection));

            return databaseType switch
            {
                DatabaseType.MySql => new MySqlChecker(dbConnection),
                DatabaseType.Oracle => new OracleChecker(dbConnection),
                DatabaseType.SQLite => new SQLiteChecker(dbConnection),
                DatabaseType.PostgreSql => new PostgreSqlChecker(dbConnection),
                DatabaseType.SqlServer => new SqlServerChecker(dbConnection),
                _ => throw new ArgumentException($"{nameof(IDbChecker)} corresponding to the specified database type is not found."),
            };
        }
    }
}

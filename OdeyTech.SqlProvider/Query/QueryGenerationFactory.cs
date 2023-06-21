// --------------------------------------------------------------------------
// <copyright file="QueryGenerationFactory.cs" author="Andrii Odeychuk">
//
// Copyright (c) Andrii Odeychuk. ALL RIGHTS RESERVED
// The entire contents of this file is protected by International Copyright Laws.
// </copyright>
// --------------------------------------------------------------------------

using System;
using OdeyTech.SqlProvider.Entity.Database;

namespace OdeyTech.SqlProvider.Query
{
    /// <summary>
    /// Provides a factory method for creating instances of <see cref="ISqlQueryGenerator"/> based on the specified <see cref="DatabaseType"/>.
    /// </summary>
    public static class QueryGenerationFactory
    {
        /// <summary>
        /// Returns an instance of <see cref="ISqlQueryGenerator"/> that corresponds to the specified <see cref="DatabaseType"/>.
        /// </summary>
        /// <param name="databaseType">The type of the database for which to get the <see cref="ISqlQueryGenerator"/>.</param>
        /// <returns>An instance of <see cref="ISqlQueryGenerator"/> that corresponds to the specified <see cref="DatabaseType"/>.</returns>
        /// <exception cref="ArgumentException">Thrown when there is no <see cref="ISqlQueryGenerator"/> corresponding to the specified <see cref="DatabaseType"/>.</exception>
        public static ISqlQueryGenerator GetGenerator(DatabaseType databaseType)
        {
            switch (databaseType)
            {
                case DatabaseType.Oracle:
                case DatabaseType.SQLite:
                case DatabaseType.SqlServer:
                case DatabaseType.PostgreSql:
                case DatabaseType.MySql:
                    return new BasicQueryGenerator();
            }

            throw new ArgumentException($"{nameof(ISqlQueryGenerator)} corresponding to the specified database type is not found.");
        }
    }
}

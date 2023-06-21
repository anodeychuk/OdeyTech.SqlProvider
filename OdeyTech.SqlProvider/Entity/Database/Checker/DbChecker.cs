// --------------------------------------------------------------------------
// <copyright file="DbChecker.cs" author="Andrii Odeychuk">
//
// Copyright (c) Andrii Odeychuk. ALL RIGHTS RESERVED
// The entire contents of this file is protected by International Copyright Laws.
// </copyright>
// --------------------------------------------------------------------------

using System;
using System.Data;
using System.Data.SqlClient;
using OdeyTech.SqlProvider.Entity.Table;
using OdeyTech.SqlProvider.Executor;
using OdeyTech.SqlProvider.Query;

namespace OdeyTech.SqlProvider.Entity.Database.Checker
{
    /// <summary>
    /// Represents a base class for checking the existence of a database and its items.
    /// </summary>
    internal abstract class DbChecker : IDbChecker
    {
        protected readonly ISqlQueryGenerator sqlQueryGenerator;
        private readonly SqlExecutor sqlExecutor;

        /// <summary>
        /// Initializes a new instance of the <see cref="DbChecker"/> class with the specified database type and connection.
        /// </summary>
        /// <param name="databaseType">The type of the database to check.</param>
        /// <param name="dbConnection">The connection to the database to check.</param>
        public DbChecker(DatabaseType databaseType, IDbConnection dbConnection)
        {
            this.sqlQueryGenerator = QueryGenerationFactory.GetGenerator(databaseType);
            this.sqlExecutor = new SqlExecutor(dbConnection);
        }

        protected IDbConnection DbConnection => this.sqlExecutor.Connection;

        /// <inheritdoc/>
        public SqlTable DatabaseItemSource { get; set; }

        /// <summary>
        /// Checks the database and creates the database item if it does not exist.
        /// </summary>
        /// <exception cref="InvalidOperationException">Thrown when the database does not exist.</exception>
        public void CheckDatabase()
        {
            if (!CheckDatabaseExists())
            {
                CreateDatabase();
            }

            if (!CheckDatabaseItemExist())
            {
                CreateDatabaseItem();
            }
        }

        /// <summary>
        /// Checks if the database exists.
        /// </summary>
        /// <returns><c>true</c> if the database exists; otherwise, <c>false</c>.</returns>
        protected bool CheckDatabaseExists()
        {
            if (!CheckDatabaseFileExists())
            {
                return false;
            }

            try
            {
                DbConnection.Open();
                return true;
            }
            catch (SqlException)
            {
                return false;
            }
            finally
            {
                if (DbConnection.State != ConnectionState.Closed)
                {
                    DbConnection.Close();
                }
            }
        }

        /// <summary>
        /// Checks if the database file exists.
        /// </summary>
        /// <returns><c>true</c> if the database file exists, otherwise, <c>false</c>.</returns>
        protected virtual bool CheckDatabaseFileExists() => true;

        /// <summary>
        /// Creates the database.
        /// </summary>
        protected virtual void CreateDatabase() => throw new InvalidOperationException("Database does not exist.");

        /// <summary>
        /// Checks if the database item exists.
        /// </summary>
        /// <returns><c>true</c> if the database item exists; otherwise, <c>false</c>.</returns>
        protected bool CheckDatabaseItemExist()
        {
            try
            {
                DbConnection.Open();
                return CheckDatabaseItemExistInternal(DatabaseItemSource.GetName());
            }
            catch (SqlException)
            {
                return false;
            }
            finally
            {
                if (DbConnection.State != ConnectionState.Closed)
                {
                    DbConnection.Close();
                }
            }
        }

        /// <summary>
        /// Checks if the database item exists internally.
        /// </summary>
        /// <param name="itemName">The name of the database item.</param>
        /// <returns><c>true</c> if the database item exists; otherwise, <c>false</c>.</returns>
        protected abstract bool CheckDatabaseItemExistInternal(string itemName);

        private void CreateDatabaseItem()
        {
            var query = this.sqlQueryGenerator.Create(DatabaseItemSource);
            this.sqlExecutor.Query(query);
        }
    }
}

// --------------------------------------------------------------------------
// <copyright file="SqlExecutor.cs" author="Andrii Odeychuk">
//
// Copyright (c) Andrii Odeychuk. ALL RIGHTS RESERVED
// The entire contents of this file is protected by International Copyright Laws.
// </copyright>
// --------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using OdeyTech.ProductivityKit.Extension;

namespace OdeyTech.SqlProvider.Executor
{
    /// <summary>
    /// Represents a SQL query executor.
    /// </summary>
    public class SqlExecutor : ISqlExecutor
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SqlExecutor"/> class with the specified database connection.
        /// </summary>
        /// <param name="connection">The <see cref="IDbConnection"/> object representing the database connection.</param>
        /// <exception cref="ArgumentException">Thrown when connection is null.</exception>
        public SqlExecutor(IDbConnection connection)
        {
            Connection = connection ?? throw new ArgumentException("Connection cannot be null.");
        }

        /// <inheritdoc/>
        public IDbConnection Connection { get; }

        /// <inheritdoc/>
        /// <exception cref="ArgumentException">Thrown when query is null or empty.</exception>
        /// <exception cref="SqlExecutorException">Thrown when an exception occurs during query execution.</exception>
        public void Query(string query, params DbParameter[] parameters)
        {
            if (string.IsNullOrEmpty(query))
            {
                throw new ArgumentException("Query cannot be null.");
            }

            OpenConnection();

            using IDbCommand command = this.Connection.CreateCommand();
            using IDbTransaction transaction = this.Connection.BeginTransaction();
            command.Transaction = transaction;
            command.CommandText = query;
            AddParameters(command.Parameters, parameters);

            try
            {
                command.ExecuteNonQuery();
                transaction.Commit();
            }
            catch (SqlException ex)
            {
                transaction.Rollback();
                throw new SqlExecutorException($"Exception while execute Query: {command.CommandText}", ex);
            }
            finally
            {
                CloseConnection();
            }
        }

        /// <inheritdoc/>
        /// <exception cref="ArgumentException">Thrown when queries is null.</exception>
        /// <exception cref="SqlExecutorException">Thrown when an exception occurs during query execution.</exception>
        public void Query(IEnumerable<string> queries, params DbParameter[] parameters)
        {
            if (queries.IsNullOrEmpty())
            {
                throw new ArgumentException("Queries cannot be null or have no elements.");
            }

            OpenConnection();
            using IDbTransaction transaction = this.Connection.BeginTransaction();

            try
            {
                foreach (var query in queries)
                {
                    using IDbCommand command = this.Connection.CreateCommand();
                    command.Transaction = transaction;
                    command.CommandText = query;
                    AddParameters(command.Parameters, parameters);
                    command.ExecuteNonQuery();
                }

                transaction.Commit();
            }
            catch (SqlException ex)
            {
                transaction.Rollback();
                throw new SqlExecutorException($"Exception while execute query: {ex.Message}", ex);
            }
            finally
            {
                CloseConnection();
            }
        }

        /// <inheritdoc/>
        /// <exception cref="ArgumentException">Thrown when select-query is null or empty.</exception>
        /// <exception cref="SqlExecutorException">Thrown when an exception occurs during query execution.</exception>
        public DataTable Select(string query, params DbParameter[] parameters)
        {
            if (string.IsNullOrEmpty(query))
            {
                throw new ArgumentException("Query cannot be null.");
            }

            var dataTable = new DataTable();
            OpenConnection();

            try
            {
                using IDbCommand command = this.Connection.CreateCommand();
                command.CommandText = query;
                AddParameters(command.Parameters, parameters);

                using IDataReader dataReader = command.ExecuteReader();
                dataTable.Load(dataReader);
            }
            catch (SqlException ex)
            {
                throw new SqlExecutorException($"Exception while execute Select: {ex.Message}", ex);
            }
            finally
            {
                CloseConnection();
            }

            return dataTable;
        }

        /// <inheritdoc/>
        /// <exception cref="ArgumentException">Thrown when store procedure name is null or empty.</exception>
        /// <exception cref="SqlExecutorException">Thrown when an exception occurs during store procedure execution.</exception>
        public List<DbParameter> StoreProcedure(string storeProcedureName, params DbParameter[] parameters)
        {
            if (string.IsNullOrEmpty(storeProcedureName))
            {
                throw new ArgumentException("Store procedure name cannot be null.");
            }

            List<DbParameter> outputParameters = new();
            OpenConnection();

            try
            {
                using IDbCommand command = this.Connection.CreateCommand();
                command.CommandText = storeProcedureName;
                command.CommandType = CommandType.StoredProcedure;
                AddParameters(command.Parameters, parameters);
                command.ExecuteNonQuery();

                foreach (DbParameter item in command.Parameters)
                {
                    if (item.Direction == ParameterDirection.Output)
                    {
                        outputParameters.Add(item);
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new SqlExecutorException($"Exception while execute StoreProcedure: {ex.Message}", ex);
            }
            finally
            {
                CloseConnection();
            }

            return outputParameters;
        }

        /// <inheritdoc/>
        /// <exception cref="ArgumentException">Thrown when store function name is null or empty.</exception>
        /// <exception cref="SqlExecutorException">Thrown when an exception occurs during store function execution.</exception>
        public object StoreFunction(string storeFunctionName, params DbParameter[] parameters)
        {
            if (string.IsNullOrEmpty(storeFunctionName))
            {
                throw new ArgumentException("Store function name cannot be null.");
            }

            object result = null;

            try
            {
                OpenConnection();
                using IDbCommand command = this.Connection.CreateCommand();
                command.CommandText = storeFunctionName;
                command.CommandType = CommandType.StoredProcedure;
                AddParameters(command.Parameters, parameters);
                command.ExecuteNonQuery();

                foreach (DbParameter item in command.Parameters)
                {
                    if (item.Direction == ParameterDirection.ReturnValue)
                    {
                        result = item.Value;
                        break;
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new SqlExecutorException($"Exception while execute StoreFunction: {ex.Message}", ex);
            }
            finally
            {
                CloseConnection();
            }

            return result;
        }

        /// <summary>
        /// Opens a connection to the database. This is a necessary step before executing any SQL commands.
        /// </summary>
        /// <exception cref="SqlExecutorException">Thrown when an exception occurs while trying to open the connection to the database.</exception>
        private void OpenConnection()
        {
            try
            {
                this.Connection.Open();
            }
            catch (DbException ex)
            {
                throw new SqlExecutorException($"Exception while opening connection: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Closes the connection to the database. This should be done after all necessary SQL commands have been executed.
        /// </summary>
        /// <exception cref="SqlExecutorException">Thrown when an exception occurs while trying to close the connection to the database.</exception>
        private void CloseConnection()
        {
            try
            {
                this.Connection.Close();
            }
            catch (DbException ex)
            {
                throw new SqlExecutorException($"Exception while closing connection: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Adds a collection of DbParameter objects to an IDataParameterCollection.
        /// </summary>
        /// <param name="parameterCollection">The IDataParameterCollection to add the parameters to.</param>
        /// <param name="parameters">An array of DbParameter objects to add to the collection.</param>
        private void AddParameters(IDataParameterCollection parameterCollection, params DbParameter[] parameters)
        {
            if (parameters.IsNullOrEmpty())
            {
                return;
            }

            parameters.ForEach(parameter => parameterCollection.Add(parameter));
        }
    }
}

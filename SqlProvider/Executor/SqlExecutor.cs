// --------------------------------------------------------------------------
// <copyright file="SqlExecutor.cs" author="Andrii Odeychuk">
//
// Copyright (c) Andrii Odeychuk. ALL RIGHTS RESERVED
// The entire contents of this file is protected by International Copyright Laws.
// </copyright>
// --------------------------------------------------------------------------

using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;

namespace SqlProvider.Executor
{
  /// <summary>
  /// Represents a SQL query executor.
  /// </summary>
  public class SqlExecutor : ISqlExecutor
  {
    private DbConnection connection;

    /// <summary>
    /// Disposes the database connection when this object is no longer needed.
    /// </summary>
    public void Dispose() => this.connection.Dispose();

    /// <summary>
    /// Sets the database connection.
    /// </summary>
    /// <param name="connection">The <see cref="DbConnection"/> object representing the database connection.</param>
    public void SetDbConnection(DbConnection connection) => this.connection = connection;

    /// <summary>
    /// Executes a single SQL query that is an insert, update or delete statement.
    /// </summary>
    /// <param name="query">The SQL query to execute.</param>
    /// <param name="parameters">The parameters for the query.</param>
    public void Query(string query, params DbParameter[] parameters)
    {
      if (string.IsNullOrEmpty(query))
      {
        return;
      }

      OpenConnection();

      using DbCommand command = this.connection.CreateCommand();
      using DbTransaction transaction = this.connection.BeginTransaction();
      command.Transaction = transaction;
      command.CommandText = query;
      command.Parameters.AddRange(parameters);

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

    /// <summary>
    /// Executes multiple SQL queries that are insert, update or delete statements.
    /// </summary>
    /// <param name="queries">The collection of SQL queries to execute.</param>
    /// <param name="parameters">The parameters for the queries.</param>
    public void Query(IEnumerable<string> queries, params DbParameter[] parameters)
    {
      if (queries == null || !queries.Any())
      {
        return;
      }

      OpenConnection();
      using DbTransaction transaction = this.connection.BeginTransaction();

      try
      {
        foreach (var query in queries)
        {
          using DbCommand command = this.connection.CreateCommand();
          command.Transaction = transaction;
          command.CommandText = query;
          command.Parameters.AddRange(parameters);
          command.ExecuteNonQuery();
        }

        transaction.Commit();
      }
      catch (SqlException ex)
      {
        transaction.Rollback();
        throw new SqlExecutorException($"Exception while execute Query: {ex.Message}", ex);
      }
      finally
      {
        CloseConnection();
      }
    }

    /// <summary>
    /// Executes an SQL query that selects data from a database.
    /// </summary>
    /// <param name="query">The SQL query to execute.</param>
    /// <param name="parameters">The parameters for the query.</param>
    /// <returns>The selected data as a DataTable object.</returns>
    public DataTable Select(string query, params DbParameter[] parameters)
    {
      var dataTable = new DataTable();
      OpenConnection();

      try
      {
        using DbCommand command = this.connection.CreateCommand();
        command.CommandText = query;
        command.Parameters.AddRange(parameters);

        using DbDataReader dataReader = command.ExecuteReader();
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

    /// <summary>
    /// Executes a stored procedure in a database.
    /// </summary>
    /// <param name="storeProcedureName">The name of the stored procedure to execute.</param>
    /// <param name="parameters">The parameters for the stored procedure
    /// <returns>A list of output parameters returned by the stored procedure.</returns>
    public List<DbParameter> StoreProcedure(string storeProcedureName, List<DbParameter> parameters)
    {
      List<DbParameter> outputParameters = new();
      OpenConnection();

      try
      {
        using DbCommand command = this.connection.CreateCommand();
        command.CommandText = storeProcedureName;
        command.CommandType = CommandType.StoredProcedure;
        command.Parameters.AddRange(parameters.ToArray());
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

    /// <summary>
    /// Executes a stored function in a database.
    /// </summary>
    /// <param name="storeFunctionName">The name of the stored function to execute.</param>
    /// <param name="parameters">The parameters for the stored function.</param>
    /// <returns>The result returned by the stored function.</returns>
    public object StoreFunction(string storeFunctionName, List<DbParameter> parameters)
    {
      object result = null;

      try
      {
        OpenConnection();
        using DbCommand command = this.connection.CreateCommand();
        command.CommandText = storeFunctionName;
        command.CommandType = CommandType.StoredProcedure;
        command.Parameters.AddRange(parameters.ToArray());
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
    /// Opens a connection to the database.
    /// </summary>
    private void OpenConnection()
    {
      try
      {
        this.connection.Open();
      }
      catch (DbException ex)
      {
        throw new SqlExecutorException($"Exception while opening connection: {ex.Message}", ex);
      }
    }

    /// <summary>
    /// Closes the connection to the database.
    /// </summary>
    private void CloseConnection()
    {
      try
      {
        this.connection.Close();
      }
      catch (DbException ex)
      {
        throw new SqlExecutorException($"Exception while closing connection: {ex.Message}", ex);
      }
    }
  }
}
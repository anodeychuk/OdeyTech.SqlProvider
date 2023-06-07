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
using OdeyTech.ProductivityKit.Extension;

namespace OdeyTech.SqlProvider.Executor
{
  /// <summary>
  /// Represents a SQL query executor.
  /// </summary>
  public class SqlExecutor : ISqlExecutor
  {
    private readonly IDbConnection connection;

    /// <summary>
    /// Disposes the database connection when this object is no longer needed.
    /// </summary>
    public void Dispose() => this.connection.Dispose();

    /// <summary>
    /// Initializes a new instance of the <see cref="SqlExecutor"/> class with the specified database connection.
    /// </summary>
    /// <param name="connection">The <see cref="IDbConnection"/> object representing the database connection.</param>
    public SqlExecutor(IDbConnection connection)
    {
      this.connection = connection;
    }

    /// <inheritdoc/>
    public void Query(string query, params DbParameter[] parameters)
    {
      if (string.IsNullOrEmpty(query))
      {
        return;
      }

      OpenConnection();

      using IDbCommand command = this.connection.CreateCommand();
      using IDbTransaction transaction = this.connection.BeginTransaction();
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
    public void Query(IEnumerable<string> queries, params DbParameter[] parameters)
    {
      if (queries == null || !queries.Any())
      {
        return;
      }

      OpenConnection();
      using IDbTransaction transaction = this.connection.BeginTransaction();

      try
      {
        foreach (var query in queries)
        {
          using IDbCommand command = this.connection.CreateCommand();
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
        throw new SqlExecutorException($"Exception while execute Query: {ex.Message}", ex);
      }
      finally
      {
        CloseConnection();
      }
    }

    /// <inheritdoc/>
    public DataTable Select(string query, params DbParameter[] parameters)
    {
      var dataTable = new DataTable();
      OpenConnection();

      try
      {
        using IDbCommand command = this.connection.CreateCommand();
        command.CommandText = query;
        AddParameters(command.Parameters, parameters);

        using var dataReader = command.ExecuteReader();
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
    public List<DbParameter> StoreProcedure(string storeProcedureName, params DbParameter[] parameters)
    {
      List<DbParameter> outputParameters = new();
      OpenConnection();

      try
      {
        using var command = this.connection.CreateCommand();
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
    public object StoreFunction(string storeFunctionName, params DbParameter[] parameters)
    {
      object result = null;

      try
      {
        OpenConnection();
        using var command = this.connection.CreateCommand();
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
﻿// --------------------------------------------------------------------------
// <copyright file="SqlQueryGenerator.cs" author="Andrii Odeychuk">
//
// Copyright (c) Andrii Odeychuk. ALL RIGHTS RESERVED
// The entire contents of this file is protected by International Copyright Laws.
// </copyright>
// --------------------------------------------------------------------------

using System;

namespace SqlProvider.Query
{
  /// <summary>
  /// SQL queries generator.
  /// </summary>
  public static class SqlQueryGenerator
  {
    private static long LastGenId { get; set; }
    private static readonly object LockGen = new();

    /// <summary>
    /// Generates a new unique identifier.
    /// </summary>
    /// <returns>A new unique identifier.</returns>
    public static long GenerateId()
    {
      lock (LockGen)
      {
        var genId = Convert.ToInt64(DateTime.Now.ToString("yyMMddHHmmssffff"));
        if (genId <= LastGenId)
        {
          genId = ++LastGenId;
        }
        else
        {
          LastGenId = genId;
        }

        return genId;
      }
    }

    /// <summary>
    /// Generates a SELECT query.
    /// </summary>
    /// <param name="source">The SqlQuerySource to generate the query from.</param>
    /// <returns>A string representation of the generated query.</returns>
    public static string Select(SqlQuerySource source)
    {
      source.Validate(SqlType.Select);
      return $"SELECT {source.Columns.GetColumnsName()} FROM {source.GetTable(true)}{source.GetJoins()}{source.GetConditions()}{source.GetOrderBy()};";
    }

    /// <summary>
    /// Generates an INSERT query.
    /// </summary>
    /// <param name="source">The SqlQuerySource to generate the query from.</param>
    /// <returns>A string representation of the generated query.</returns>
    public static string Insert(SqlQuerySource source)
    {
      source.Validate(SqlType.Insert);
      return $"INSERT INTO {source.GetTable()} ({source.Columns.GetColumnsName()}) VALUES ({source.Columns.GetValues()});";
    }

    /// <summary>
    /// Generates an UPDATE query.
    /// </summary>
    /// <param name="source">The SqlQuerySource to generate the query from.</param>
    /// <returns>A string representation of the generated query.</returns>
    public static string Update(SqlQuerySource source)
    {
      source.Validate(SqlType.Update);
      return $"UPDATE {source.GetTable()} SET {source.Columns.GetColumnsValue()}{source.GetConditions()};";
    }

    /// <summary>
    /// Generates a DELETE query.
    /// </summary>
    /// <param name="source">The SqlQuerySource to generate the query from.</param>
    /// <returns>A string representation of the generated query.</returns>
    public static string Delete(SqlQuerySource source)
    {
      source.Validate(SqlType.Delete);
      return $"DELETE FROM {source.GetTable()}{source.GetConditions()};";
    }
  }
}
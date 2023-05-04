// --------------------------------------------------------------------------
// <copyright file="ColumnValues.cs" author="Andrii Odeychuk">
//
// Copyright (c) Andrii Odeychuk. ALL RIGHTS RESERVED
// The entire contents of this file is protected by International Copyright Laws.
// </copyright>
// --------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using OdeyTech.SqlProvider.Query;

/// <summary>
/// Represents a collection of column values to be used in SQL queries.
/// </summary>
public class ColumnValues : ICloneable
{
  private readonly Dictionary<string, SqlValue> showAllColumns = new() { { "*", null } };
  private Dictionary<string, SqlValue> columnsSource = new();
  private readonly HashSet<string> excludedColumns = new();

  /// <summary>
  /// Gets the number of columns.
  /// </summary>
  public int Count => this.columnsSource.Count;

  /// <summary>
  /// Adds one or more columns.
  /// </summary>
  /// <param name="columns">The names of the columns to add.</param>
  public void AddColumns(params string[] columns)
  {
    foreach (var col in columns)
    {
      this.columnsSource.Add(col, null);
    }
  }

  /// <summary>
  /// Adds one or more columns to be excluded.
  /// </summary>
  /// <param name="columns">The names of the columns to exclude.</param>
  public void AddExcludedColumns(params string[] columns) => this.excludedColumns.UnionWith(columns);

  /// <summary>
  /// Adds all columns from another ColumnValues object.
  /// </summary>
  /// <param name="columns">The ColumnValues object to add columns from.</param>
  public void AddColumns(ColumnValues columns) => this.columnsSource.Union(columns.columnsSource);

  /// <summary>
  /// Adds a column and its value.
  /// </summary>
  /// <param name="columnName">The name of the column to add.</param>
  /// <param name="value">The value of the column to add.</param>
  public void AddColumnWithValue(string columnName, SqlValue value) => this.columnsSource.Add(columnName, value);

  /// <summary>
  /// Sets the value of a column.
  /// </summary>
  /// <param name="columnName">The name of the column to set the value for.</param>
  /// <param name="value">The value to set for the column.</param>
  public void SetValue(string columnName, SqlValue value) => this.columnsSource[columnName] = value;

  /// <summary>
  /// Gets the column names and their values in the format "columnName1 = value1, columnName2 = value2, ...".
  /// </summary>
  /// <returns>A string representation of the column names and their values.</returns>
  public string GetColumnsValue() => string.Join(", ", GetColumnsSource().Select(p => $"{p.Key} = {GetValue(p.Value)}"));

  /// <summary>
  /// Gets the names of the columns, separated by commas.
  /// </summary>
  /// <returns>A string representation of the column names.</returns>
  public string GetColumnsName() => string.Join(", ", GetColumnsSource().Select(p => p.Key));

  /// <summary>
  /// Gets the values of the columns, separated by commas.
  /// </summary>
  /// <returns>A string representation of the column values.</returns>
  public string GetValues() => string.Join(", ", GetColumnsSource().Select(p => GetValue(p.Value)));

  private Dictionary<string, SqlValue> GetColumnsSource()
    => this.columnsSource.Count == 0
      ? this.showAllColumns
      : this.excludedColumns.Count > 0
        ? this.columnsSource.Where(i => !this.excludedColumns.Contains(i.Key)).ToDictionary(p => p.Key, p => p.Value)
        : this.columnsSource;

  /// <summary>
  /// Copy of this ColumnValues object.
  /// </summary>
  /// <returns>A new ColumnValues object with the same column values.</returns>
  public object Clone() => new ColumnValues { columnsSource = new(this.columnsSource) };

  /// <summary>
  /// Removes all columns and excluded columns.
  /// </summary>
  public void Clear()
  {
    this.columnsSource.Clear();
    this.excludedColumns.Clear();
  }

  /// <summary>
  /// Returns a string representation of the given SQL value, suitable for use in a SQL query.
  /// </summary>
  /// <param name="sqlValue">The SQL value to get the string representation of.</param>
  /// <returns>A string representation of the given SQL value.</returns>
  /// <exception cref="ArgumentException">Thrown when the given SQL value has an unsupported data type.</exception>
  private string GetValue(SqlValue sqlValue)
  => sqlValue == null
    ? "NULL"
    : sqlValue.DataType switch
    {
      SqlDataType.INT => sqlValue.Value.ToString(),
      SqlDataType.DOUBLE => sqlValue.Value.ToString().Replace(",", "."),
      SqlDataType.VARCHAR => $"'{sqlValue.Value?.ToString().Replace("\'", "\\\'")}'",
      SqlDataType.DATETIME => $"'{sqlValue.Value:yyyy-MM-dd HH:mm:ss}'",
      SqlDataType.DATE => $"'{sqlValue.Value:yyyy-MM-dd}'",
      SqlDataType.BOOL => Convert.ToBoolean(sqlValue.Value) ? "1" : "0",
      _ => throw new ArgumentException("Unsupported data type", sqlValue.DataType.ToString())
    };
}
// --------------------------------------------------------------------------
// <copyright file="SqlColumns.cs" author="Andrii Odeychuk">
//
// Copyright (c) Andrii Odeychuk. ALL RIGHTS RESERVED
// The entire contents of this file is protected by International Copyright Laws.
// </copyright>
// --------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using OdeyTech.ProductivityKit.Extension;
using OdeyTech.SqlProvider.DataType;

namespace OdeyTech.SqlProvider.Query
{
  /// <summary>
  /// Collection of column values to be used in SQL queries.
  /// </summary>
  public class SqlColumns : ICloneable
  {
    private readonly Dictionary<string, SqlColumnParameters> showAllColumns = new() { { "*", null } };
    private Dictionary<string, SqlColumnParameters> columnsSource = new();
    private readonly HashSet<string> excludedColumns = new();

    /// <summary>
    /// Gets the number of columns.
    /// </summary>
    public int Count => this.columnsSource.Count;

    /// <summary>
    /// Adds one or more columns to be excluded.
    /// </summary>
    /// <param name="columns">The names of the columns to exclude.</param>
    public void AddExcludedColumns(params string[] columns) => this.excludedColumns.UnionWith(columns);

    public void RemoveFromExcludedColumns(params string[] columns) => this.excludedColumns.RemoveWhere(column => columns.Contains(column));

    public bool IsExcludedColumn(string columnName) => this.excludedColumns.Contains(columnName);

    /// <summary>
    /// Adds all columns from another SqlColumns object.
    /// </summary>
    /// <param name="columns">The SqlColumns object to add columns from.</param>
    public void AddColumns(SqlColumns columns) => this.columnsSource.Union(columns.columnsSource);

    /// <summary>
    /// Adds a column and its value.
    /// </summary>
    /// <param name="columnName">The name of the column to add.</param>
    /// <param name="parameters">The parameters of the column to add.</param>
    public void AddColumn(string columnName, SqlColumnParameters parameters) => this.columnsSource.Add(columnName, parameters);

    public void AddColumn(string columnName, IDbDataType dataType) => AddColumn(columnName, new SqlColumnParameters(dataType));

    /// <summary>
    /// Sets the value of a column.
    /// </summary>
    /// <param name="columnName">The name of the column to set the value for.</param>
    /// <param name="value">The value to set for the column.</param>
    public void SetValue(string columnName, object value)
      => this.columnsSource[columnName].Value = this.columnsSource.ContainsKey(columnName) ? value : throw new ArgumentException(nameof(columnName));

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

    public string GetColumnsType() => string.Join(", ", GetColumnsSource().Select(p => $"{p.Key} {GetDataType(p.Value)}"));

    /// <summary>
    /// Gets the values of the columns, separated by commas.
    /// </summary>
    /// <returns>A string representation of the column values.</returns>
    public string GetValues() => string.Join(", ", GetColumnsSource().Select(p => GetValue(p.Value)));

    /// <summary>
    /// Gets the columns source based on the columnsSource, showAllColumns and excludedColumns properties.
    /// </summary>
    /// <returns>A Dictionary of string and SqlColumnParameters.</returns>
    private Dictionary<string, SqlColumnParameters> GetColumnsSource()
      => this.columnsSource.Count == 0
        ? this.showAllColumns
        : this.excludedColumns.Count > 0
          ? this.columnsSource.Where(i => !this.excludedColumns.Contains(i.Key)).ToDictionary(p => p.Key, p => p.Value)
          : this.columnsSource;

    /// <summary>
    /// Copy of this SqlColumns object.
    /// </summary>
    /// <returns>A new SqlColumns object with the same column values.</returns>
    public object Clone() => new SqlColumns { columnsSource = new(this.columnsSource) };

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
    private string GetValue(SqlColumnParameters sqlValue)
    {
      if (sqlValue.Value == null)
      {
        return "NULL";
      }

      switch (sqlValue.DataType.Category)
      {
        case DbDataTypeCategory.Int:
        case DbDataTypeCategory.Double:
          return SanitizeSqlValue(sqlValue.Value.ToString());
        case DbDataTypeCategory.String:
          return $"'{SanitizeSqlValue(sqlValue.Value.ToString())}'";
        case DbDataTypeCategory.DateTime:
          return $"'{(DateTime)sqlValue.Value:yyyy-MM-dd HH:mm:ss}'";
        case DbDataTypeCategory.Date:
          return $"'{(DateTime)sqlValue.Value:yyyy-MM-dd}'";
        case DbDataTypeCategory.Boolean:
          return Convert.ToBoolean(sqlValue.Value) ? "1" : "0";
        default:
          return "NULL";
      }
    }

    private string SanitizeSqlValue(string value)
    {
      if (value == null)
      {
        return "NULL";
      }

      // Escape single quotes by doubling them
      value = value.Replace("'", "''");

      // Escape backslashes by doubling them
      value = value.Replace("\\", "\\\\");

      // Ensure numeric values are properly formatted
      if (decimal.TryParse(value, out _))
      {
        // Convert to a valid numeric format based on your database system's requirements
        value = value.Replace(",", ".");
      }

      return value;
    }

    /// <summary>
    /// Gets the SQL data type as a string representation.
    /// </summary>
    /// <param name="sqlValue">The SqlColumnParameters containing the data type information.</param>
    /// <returns>A string representation of the SQL data type.</returns>
    private string GetDataType(SqlColumnParameters sqlValue)
      => sqlValue.DataType.Size.IsNullOrEmpty()
        ? sqlValue.DataType.TypeName
        : $"{sqlValue.DataType.TypeName} ({sqlValue.DataType.Size})";
  }
}
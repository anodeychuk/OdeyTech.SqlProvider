// --------------------------------------------------------------------------
// <copyright file="SqlQuerySource.cs" author="Andrii Odeychuk">
//
// Copyright (c) Andrii Odeychuk. ALL RIGHTS RESERVED
// The entire contents of this file is protected by International Copyright Laws.
// </copyright>
// --------------------------------------------------------------------------

using System;
using System.Linq;

namespace OdeyTech.SqlProvider.Query
{
  /// <summary>
  /// SQL types of query.
  /// </summary>
  public enum SqlType
  {
    Select,
    Insert,
    Update,
    Delete
  }

  /// <summary>
  /// SQL query source.
  /// </summary>
  public class SqlQuerySource : ICloneable
  {
    private string tableName;
    private string tablePrefix;
    private string[] joins;
    private string[] conditions;
    private string[] orderBy;

    /// <summary>
    /// Columns of the SQL query source.
    /// </summary>
    public ColumnValues Columns { get; private set; } = new();

    /// <summary>
    /// Sets the table name and prefix of the SQL query source.
    /// </summary>
    /// <param name="tableName">The name of the table.</param>
    /// <param name="tablePrefix">The prefix of the table.</param>
    public void SetTable(string tableName, string tablePrefix = null)
    {
      this.tableName = tableName;
      this.tablePrefix = tablePrefix;
    }

    /// <summary>
    /// Gets the table name of the SQL query source.
    /// </summary>
    /// <param name="withPrefix">Indicates whether to include the table prefix in the table name.</param>
    /// <returns>The table name.</returns>
    public string GetTable(bool withPrefix = false)
    {
      var prefix = withPrefix && !string.IsNullOrEmpty(this.tablePrefix) ? $" {this.tablePrefix}" : string.Empty;
      return $"{this.tableName}{prefix}";
    }

    /// <summary>
    /// Clears the join statements of the SQL query source.
    /// </summary>
    public void ClearJoins() => this.joins = null;

    /// <summary>
    /// Adds the join statements to the SQL query source.
    /// </summary>
    /// <param name="joins">The join statements.</param>
    public void AddJoins(params string[] joins)
        => this.joins = this.joins == null || this.joins.Length == 0
                          ? joins
                          : this.joins.Concat(joins).ToArray();

    /// <summary>
    /// Gets the join statements of the SQL query source.
    /// </summary>
    /// <returns>The join statements.</returns>
    public string GetJoins()
        => this.joins == null || this.joins.Length == 0
              ? string.Empty
              : $" {string.Join(" ", this.joins)}";

    /// <summary>
    /// Clears the condition statements of the SQL query source.
    /// </summary>
    public void ClearConditions() => this.conditions = null;

    /// <summary>
    /// Adds the condition statements to the SQL query source.
    /// </summary>
    /// <param name="conditions">The condition statements.</param>
    public void AddConditions(params string[] conditions)
        => this.conditions = this.conditions == null || this.conditions.Length == 0
                                ? conditions
                                : this.conditions.Concat(conditions).ToArray();

    /// <summary>
    /// Gets the condition statements of the SQL query source.
    /// </summary>
    /// <returns>The condition statements.</returns>
    public string GetConditions()
        => this.conditions == null || this.conditions.Length == 0
              ? string.Empty
              : $" WHERE {string.Join(" AND ", this.conditions)}";

    /// <summary>
    /// Adds the order by statements to the SQL query source.
    /// </summary>
    /// <param name="orderBy">The order by statements.</param>
    public void AddOrderBy(params string[] orderBy)
        => this.orderBy = this.orderBy == null || this.orderBy.Length == 0
                              ? orderBy
                              : this.orderBy.Concat(orderBy).ToArray();

    /// <summary>
    /// Gets the order by statements of the SQL query source.
    /// </summary>
    /// <returns>The order by statements.</returns>
    public string GetOrderBy()
        => this.orderBy == null || this.orderBy.Length == 0
              ? string.Empty
              : $" ORDER BY {string.Join(", ", this.orderBy)}";

    /// <summary>
    /// Validates the SQL query source.
    /// </summary>
    /// <param name="sqlType">The type of the SQL query.</param>
    public void Validate(SqlType sqlType)
    {
      Check(() => string.IsNullOrEmpty(this.tableName), nameof(this.tableName));
      if (sqlType is SqlType.Insert or SqlType.Update)
      {
        Check(() => Columns == null || Columns.Count == 0, nameof(Columns));
      }
    }

    /// <summary>
    /// Creates a deep copy of the SQL query source.
    /// </summary>
    /// <returns>A copy of the SQL query source.</returns>
    public object Clone()
    {
      var query = new SqlQuerySource
      {
        tableName = this.tableName,
        tablePrefix = this.tablePrefix,
        Columns = (ColumnValues)Columns.Clone(),
        joins = this.joins,
        conditions = this.conditions,
        orderBy = this.orderBy
      };
      return query;
    }

    /// <summary>
    /// Checks the condition, and throws <see cref="ArgumentNullException"/> if the condition is true.
    /// </summary>
    /// <param name="action">The action to check for null.</param>
    /// <param name="paramName">The name of the parameter.</param>
    private void Check(Func<bool> action, string paramName)
    {
      if (action())
      {
        throw new ArgumentNullException(paramName);
      }
    }

    /// <summary>
    /// Determines whether the specified object is equal to the current object based on the hash code.
    /// </summary>
    /// <param name="obj">The object to compare with the current object.</param>
    /// <returns>True if the objects are equal based on the hash code, otherwise false.</returns>
    public override bool Equals(object obj) => obj is SqlQuerySource query && query.GetHashCode() == GetHashCode();

    /// <summary>
    /// Gets the hash code of the SQL query source based on its properties.
    /// </summary>
    /// <returns>The hash code of the SQL query source.</returns>
    public override int GetHashCode() => (this.tableName, this.tablePrefix, Columns.GetColumnsValue(), GetJoins(), GetConditions(), GetOrderBy()).GetHashCode();
  }
}
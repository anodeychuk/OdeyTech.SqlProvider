// --------------------------------------------------------------------------
// <copyright file="SqlQuerySource.cs" author="Andrii Odeychuk">
//
// Copyright (c) Andrii Odeychuk. ALL RIGHTS RESERVED
// The entire contents of this file is protected by International Copyright Laws.
// </copyright>
// --------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using OdeyTech.ProductivityKit.Extension;
using OdeyTech.SqlProvider.Enum;
using OdeyTech.SqlProvider.Query.Constraint;

namespace OdeyTech.SqlProvider.Query
{
  /// <summary>
  /// SQL query source.
  /// </summary>
  public class SqlQuerySource : ICloneable
  {
    private string tableName;
    private string tablePrefix;
    private List<string> joins;
    private List<string> conditions;
    private List<string> orderBy;
    private List<IConstraint> constraints;

    /// <summary>
    /// Columns of the SQL query source.
    /// </summary>
    public SqlColumns Columns { get; private set; } = new();

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
    {

      if (this.joins.IsNullOrEmpty())
      {
        this.joins = new List<string>(joins);
      }
      else
      {
        this.joins.AddRange(joins);
      }
    }

    /// <summary>
    /// Gets the join statements of the SQL query source.
    /// </summary>
    /// <returns>The join statements.</returns>
    public string GetJoins()
      => this.joins.IsNullOrEmpty()
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
    {
      if (this.conditions.IsNullOrEmpty())
      {
        this.conditions = new List<string>(conditions);
      }
      else
      {
        this.conditions.AddRange(conditions);
      }
    }

    /// <summary>
    /// Gets the condition statements of the SQL query source.
    /// </summary>
    /// <returns>The condition statements.</returns>
    public string GetConditions()
      => this.conditions.IsNullOrEmpty()
        ? string.Empty
        : $" WHERE {string.Join(" AND ", this.conditions)}";

    /// <summary>
    /// Adds the order by statements to the SQL query source.
    /// </summary>
    /// <param name="orderBy">The order by statements.</param>
    public void AddOrderBy(params string[] orderBy)
    {
      if (this.orderBy.IsNullOrEmpty())
      {
        this.orderBy = new List<string>(orderBy);
      }
      else
      {
        this.orderBy.AddRange(orderBy);
      }
    }

    /// <summary>
    /// Gets the order by statements of the SQL query source.
    /// </summary>
    /// <returns>The order by statements.</returns>
    public string GetOrderBy()
      => this.orderBy.IsNullOrEmpty()
        ? string.Empty
        : $" ORDER BY {string.Join(", ", this.orderBy)}";

    /// <summary>
    /// Adds constraints to the SqlQuerySource.
    /// </summary>
    /// <param name="constraints">The constraints to add.</param>
    public void AddConstraints(params IConstraint[] constraints)
    {
      if (this.conditions.IsNullOrEmpty())
      {
        this.constraints = new List<IConstraint>(constraints);
      }
      else
      {
        this.constraints.AddRange(constraints);
      }
    }

    /// <summary>
    /// Gets the constraints as a SQL string.
    /// </summary>
    /// <returns>A string representing the SQL constraints.</returns>
    public string GetConstraints()
      => this.constraints.IsNullOrEmpty()
          ? string.Empty
          : $"ALTER TABLE {GetTable()} {string.Join(", ", this.constraints.Select(c => $"ADD {c.GetConstraint()}"))}";

    /// <summary>
    /// Validates the SQL query source.
    /// </summary>
    /// <param name="sqlType">The type of the SQL query.</param>
    public void Validate(SqlQueryType sqlType)
    {
      Check(() => string.IsNullOrEmpty(this.tableName), nameof(this.tableName));
      if (sqlType is SqlQueryType.Insert or SqlQueryType.Update)
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
        Columns = (SqlColumns)Columns.Clone(),
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
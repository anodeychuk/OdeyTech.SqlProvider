// --------------------------------------------------------------------------
// <copyright file="ColumnName.cs" author="Andrii Odeychuk">
//
// Copyright (c) Andrii Odeychuk. ALL RIGHTS RESERVED
// The entire contents of this file is protected by International Copyright Laws.
// </copyright>
// --------------------------------------------------------------------------

using System;
using OdeyTech.SqlProvider.Entity.Table.Column.NameConverter;
using OdeyTech.SqlProvider.Enum;

namespace OdeyTech.SqlProvider.Entity.Table.Column
{
  /// <summary>
  /// Represents the name of a column in a SQL query.
  /// </summary>
  public class ColumnName
  {
    private readonly string name;
    private readonly string alias;
    private readonly INameConverter converter;

    /// <summary>
    /// Initializes a new instance of the ColumnName class with the specified name, alias, and name converter.
    /// </summary>
    /// <param name="name">The name of the column.</param>
    /// <param name="alias">The alias for the column.</param>
    /// <param name="converter">The name converter for the column.</param>
    public ColumnName(string name, string alias, INameConverter converter)
    {
      this.name = name ?? throw new ArgumentException(nameof(name));
      this.alias = alias;
      this.converter = converter;
    }

    /// <summary>
    /// Gets the name of the column based on the SQL query type.
    /// </summary>
    /// <param name="sqlQueryType">The SQL query type.</param>
    /// <returns>The name of the column.</returns>
    public string GetName(SqlQueryType sqlQueryType)
    {
      if (this.converter == null)
      {
        return this.name;
      }

      if (sqlQueryType == SqlQueryType.Select)
      {
        return this.converter.ConvertName(this.name, this.alias);
      }

      return this.name;
    }
  }
}
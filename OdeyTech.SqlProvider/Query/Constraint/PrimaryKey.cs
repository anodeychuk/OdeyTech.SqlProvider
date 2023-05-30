// --------------------------------------------------------------------------
// <copyright file="PrimaryKey.cs" author="Andrii Odeychuk">
//
// Copyright (c) Andrii Odeychuk. ALL RIGHTS RESERVED
// The entire contents of this file is protected by International Copyright Laws.
// </copyright>
// --------------------------------------------------------------------------

using System.Collections.Generic;
using System.Text;
using OdeyTech.ProductivityKit.Extension;
using OdeyTech.SqlProvider.Enum;

namespace OdeyTech.SqlProvider.Query.Constraint
{
  /// <summary>
  /// Primary key constraint.
  /// </summary>
  public class PrimaryKey : IConstraint
  {
    /// <summary>
    /// Gets the type of the SQL constraint.
    /// </summary>
    public SqlConstraintType Type => SqlConstraintType.PRIMARY;

    /// <summary>
    /// Generates a SQL constraint for a primary key.
    /// </summary>
    /// <returns>
    /// A SQL string representing the primary key constraint.
    /// </returns>
    public string GetConstraint()
    {
      var sb = new StringBuilder();

      if (ConstraintName.IsFilled())
      {
        sb.Append($"CONSTRAINT {ConstraintName} ");
      }

      sb.Append($"PRIMARY KEY ({string.Join(", ", ColumnNames)})");

      return sb.ToString();
    }

    /// <summary>
    /// Gets or sets the name of the primary key constraint.
    /// </summary>
    public string ConstraintName { get; set; }

    /// <summary>
    /// Gets or sets the list of column names that make up the primary key.
    /// </summary>
    public List<string> ColumnNames { get; set; }
  }
}
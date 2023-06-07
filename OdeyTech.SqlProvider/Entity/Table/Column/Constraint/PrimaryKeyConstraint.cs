// --------------------------------------------------------------------------
// <copyright file="PrimaryKeyConstraint.cs" author="Andrii Odeychuk">
//
// Copyright (c) Andrii Odeychuk. ALL RIGHTS RESERVED
// The entire contents of this file is protected by International Copyright Laws.
// </copyright>
// --------------------------------------------------------------------------

using System.Collections.Generic;
using System.Text;
using OdeyTech.ProductivityKit.Extension;

namespace OdeyTech.SqlProvider.Entity.Table.Column.Constraint
{
  /// <summary>
  /// Primary key constraint.
  /// </summary>
  public class PrimaryKeyConstraint : IConstraint
  {
    /// <inheritdoc/>
    public SqlConstraintType Type => SqlConstraintType.Unique;

    /// <summary>
    /// Gets or sets the name of the primary key constraint.
    /// </summary>
    public string ConstraintName { get; set; }

    /// <summary>
    /// Gets or sets the list of column names that make up the primary key.
    /// </summary>
    public List<string> ColumnNames { get; set; }

    /// <summary>
    /// Generates a SQL constraint for a primary key.
    /// </summary>
    /// <returns>A SQL string representing the primary key constraint.</returns>
    public override string ToString()
    {
      var sb = new StringBuilder();

      if (ConstraintName.IsFilled())
      {
        sb.Append($"CONSTRAINT {ConstraintName} ");
      }

      sb.Append($"Primary KEY ({string.Join(", ", ColumnNames)})");

      return sb.ToString();
    }
  }
}
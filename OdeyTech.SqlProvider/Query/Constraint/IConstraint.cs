// --------------------------------------------------------------------------
// <copyright file="IConstraint.cs" author="Andrii Odeychuk">
//
// Copyright (c) Andrii Odeychuk. ALL RIGHTS RESERVED
// The entire contents of this file is protected by International Copyright Laws.
// </copyright>
// --------------------------------------------------------------------------

using OdeyTech.SqlProvider.Enum;

namespace OdeyTech.SqlProvider.Query.Constraint
{
  /// <summary>
  /// Represents a database constraint.
  /// </summary>
  public interface IConstraint
  {
    /// <summary>
    /// Gets the type of the SQL constraint.
    /// </summary>
    SqlConstraintType Type { get; }

    /// <summary>
    /// Gets or sets the name of the constraint.
    /// </summary>
    string ConstraintName { get; set; }

    /// <summary>
    /// Gets the SQL constraint.
    /// </summary>
    /// <returns>
    /// A SQL string representing the constraint.
    /// </returns>
    string GetConstraint();
  }
}
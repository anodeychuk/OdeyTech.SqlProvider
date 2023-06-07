// --------------------------------------------------------------------------
// <copyright file="SqlConstraintType.cs" author="Andrii Odeychuk">
//
// Copyright (c) Andrii Odeychuk. ALL RIGHTS RESERVED
// The entire contents of this file is protected by International Copyright Laws.
// </copyright>
// --------------------------------------------------------------------------

namespace OdeyTech.SqlProvider.Entity.Table.Column.Constraint
{
  /// <summary>
  /// Enum representing the types of constraints in SQL.
  /// </summary>
  public enum SqlConstraintType
  {
    /// <summary>
    /// Represents a PRIMARY KEY constraint. 
    /// This constraint enforces uniqueness of the column or column combination and ensures that no column that is part of the primary key can contain a NULL value.
    /// </summary>
    Primary,

    /// <summary>
    /// Represents a FOREIGN KEY constraint.
    /// This constraint maintains referential integrity by enforcing a link between the data in two tables.
    /// </summary>
    Foreign,

    /// <summary>
    /// Represents a UNIQUE constraint.
    /// This constraint enforces uniqueness of the column or column combination.
    /// </summary>
    Unique
  }
}
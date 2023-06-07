// --------------------------------------------------------------------------
// <copyright file="SqlDataTypeCategory.cs" author="Andrii Odeychuk">
//
// Copyright (c) Andrii Odeychuk. ALL RIGHTS RESERVED
// The entire contents of this file is protected by International Copyright Laws.
// </copyright>
// --------------------------------------------------------------------------

namespace OdeyTech.SqlProvider.Entity.Table.Column.DataType
{
  /// <summary>
  /// Data types of SQL values.
  /// </summary>
  public enum DbDataTypeCategory
  {
    Int,
    Double,
    String,
    Date,
    DateTime,
    Boolean,
    Binary,
    Other
  }
}
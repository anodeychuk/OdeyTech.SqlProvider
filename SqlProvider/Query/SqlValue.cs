// --------------------------------------------------------------------------
// <copyright file="SqlValue.cs" author="Andrii Odeychuk">
//
// Copyright (c) Andrii Odeychuk. ALL RIGHTS RESERVED
// The entire contents of this file is protected by International Copyright Laws.
// </copyright>
// --------------------------------------------------------------------------

namespace SqlProvider.Query
{
  /// <summary>
  /// Value of a SQL column.
  /// </summary>
  public class SqlValue
  {
    /// <summary>
    /// Value of the SQL column.
    /// </summary>
    public object Value { get; set; }

    /// <summary>
    /// Data type of the SQL column.
    /// </summary>
    public SqlDataType DataType { get; set; }
  }
}
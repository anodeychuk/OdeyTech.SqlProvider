// --------------------------------------------------------------------------
// <copyright file="IDbDataType.cs" author="Andrii Odeychuk">
//
// Copyright (c) Andrii Odeychuk. ALL RIGHTS RESERVED
// The entire contents of this file is protected by International Copyright Laws.
// </copyright>
// --------------------------------------------------------------------------

namespace OdeyTech.SqlProvider.DataType
{
  /// <summary>
  /// Represents a database data type.
  /// </summary>
  public interface IDbDataType
  {
    /// <summary>
    /// Gets the name of the data type.
    /// </summary>
    string TypeName { get; }

    /// <summary>
    /// Gets or sets the size of the SQL column data type.
    /// </summary>
    string Size { get; set; }

    /// <summary>
    /// Gets the category of the data type.
    /// </summary>
    DbDataTypeCategory Category { get; }
  }
}
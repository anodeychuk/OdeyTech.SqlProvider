// --------------------------------------------------------------------------
// <copyright file="SqlDataType.cs" author="Andrii Odeychuk">
//
// Copyright (c) Andrii Odeychuk. ALL RIGHTS RESERVED
// The entire contents of this file is protected by International Copyright Laws.
// </copyright>
// --------------------------------------------------------------------------

using OdeyTech.SqlProvider.DataType;

namespace OdeyTech.SqlProvider.Query
{
  /// <summary>
  /// Represents a SQL data type.
  /// </summary>
  public class SqlDataType
  {
    public SqlDataType(DbDataTypeCategory category, string name, string size) : this(category, name)
    {
      Size = size;
    }

    public SqlDataType(DbDataTypeCategory category, string name)
    {
      Category = category;
      Name = name;
    }

    /// <summary>
    /// Gets or sets the category of the SQL data type.
    /// </summary>
    public DbDataTypeCategory Category { get; set; }

    /// <summary>
    /// Gets or sets the data type name of the SQL column.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the size of the SQL column data type.
    /// </summary>
    public string Size { get; set; }
  }
}
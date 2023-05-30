// --------------------------------------------------------------------------
// <copyright file="SqliteDataType.cs" author="Andrii Odeychuk">
//
// Copyright (c) Andrii Odeychuk. ALL RIGHTS RESERVED
// The entire contents of this file is protected by International Copyright Laws.
// </copyright>
// --------------------------------------------------------------------------

namespace OdeyTech.SqlProvider.DataType
{
  /// <summary>
  /// Represents an SQLite data type.
  /// </summary>
  public class SqliteDataType : IDbDataType
  {
    /// <summary>
    /// Enum representing the various data types in SQLite.
    /// </summary>
    public enum DataType
    {
      Integer,
      Real,
      Text,
      Blob,
      Null
    }

    /// <summary>
    /// Initializes a new instance of the SqliteDataType class with the specified type and size.
    /// </summary>
    /// <param name="type">The SQLite data type.</param>
    /// <param name="size">The size of the SQLite data type.</param>
    public SqliteDataType(SqliteDataType.DataType type, string size) : this(type)
    {
      Size = size;
    }

    /// <summary>
    /// Initializes a new instance of the SqliteDataType class with the specified type.
    /// </summary>
    /// <param name="type">The SQLite data type.</param>
    public SqliteDataType(SqliteDataType.DataType type)
    {
      TypeName = type.ToString();
      Category = GetTypeCategory(type);
    }

    /// <inheritdoc/>
    public string TypeName { get; }

    /// <inheritdoc/>
    public DbDataTypeCategory Category { get; }

    /// <inheritdoc/>
    public string Size { get; set; }

    /// <summary>
    /// Determines the category of an SQLite data type.
    /// </summary>
    /// <param name="type">The SQLite data type.</param>
    /// <returns>The category of the data type.</returns>
    private DbDataTypeCategory GetTypeCategory(SqliteDataType.DataType type)
    {
      switch (type)
      {
        case SqliteDataType.DataType.Integer:
          return DbDataTypeCategory.Int;

        case SqliteDataType.DataType.Real:
          return DbDataTypeCategory.Double;

        case SqliteDataType.DataType.Text:
          return DbDataTypeCategory.String;

        case SqliteDataType.DataType.Blob:
          return DbDataTypeCategory.Binary;

        default:
          return DbDataTypeCategory.Other;
      }
    }
  }
}
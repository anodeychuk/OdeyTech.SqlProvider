// --------------------------------------------------------------------------
// <copyright file="SqliteDataType.cs" author="Andrii Odeychuk">
//
// Copyright (c) Andrii Odeychuk. ALL RIGHTS RESERVED
// The entire contents of this file is protected by International Copyright Laws.
// </copyright>
// --------------------------------------------------------------------------

namespace OdeyTech.SqlProvider.Entity.Table.Column.DataType
{
  /// <summary>
  /// Represents an SQLite data type.
  /// </summary>
  public class SqliteDataType : DbDataType
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
      Date,
      DateTime,
      Null
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="SqliteDataType"/> class with the specified type and size.
    /// </summary>
    /// <param name="type">The SQLite data type.</param>
    /// <param name="size">The size of the SQLite data type.</param>
    public SqliteDataType(SqliteDataType.DataType type, string size) : this(type)
    {
      Size = size;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="SqliteDataType"/> class with the specified type.
    /// </summary>
    /// <param name="type">The SQLite data type.</param>
    public SqliteDataType(SqliteDataType.DataType type)
    {
      TypeName = GetTypeName(type);
      Category = GetTypeCategory(type);
    }

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

        case SqliteDataType.DataType.Date:
          return DbDataTypeCategory.Date;

        case SqliteDataType.DataType.DateTime:
          return DbDataTypeCategory.DateTime;

        default:
          return DbDataTypeCategory.Other;
      }
    }

    private string GetTypeName(DataType type)
    {
      switch (type)
      {
        case DataType.Date:
        case DataType.DateTime:
          return DataType.Integer.ToString();
      }

      return type.ToString();
    }
  }
}
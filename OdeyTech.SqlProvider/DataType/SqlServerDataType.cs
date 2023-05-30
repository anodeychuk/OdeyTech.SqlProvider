// --------------------------------------------------------------------------
// <copyright file="SqlServerDataType.cs" author="Andrii Odeychuk">
//
// Copyright (c) Andrii Odeychuk. ALL RIGHTS RESERVED
// The entire contents of this file is protected by International Copyright Laws.
// </copyright>
// --------------------------------------------------------------------------

using System.Drawing;

namespace OdeyTech.SqlProvider.DataType
{
  /// <summary>
  /// Represents a SQL Server data type.
  /// </summary>
  public class SqlServerDataType : IDbDataType
  {
    /// <summary>
    /// Enum representing the various data types in SQL Server.
    /// </summary>
    public enum DataType
    {
      BigInt,
      Binary,
      Bit,
      Char,
      Date,
      DateTime,
      DateTime2,
      DateTimeOffset,
      Decimal,
      Float,
      Geography,
      Geometry,
      HierarchyId,
      Image,
      Int,
      Money,
      NChar,
      NText,
      Numeric,
      NVarChar,
      Real,
      SmallDateTime,
      SmallInt,
      SmallMoney,
      SqlVariant,
      SysName,
      Text,
      Time,
      Timestamp,
      TinyInt,
      UniqueIdentifier,
      VarBinary,
      VarChar
    }

    /// <summary>
    /// Initializes a new instance of the SqlServerDataType class with the specified type and size.
    /// </summary>
    /// <param name="type">The SQL Server data type.</param>
    /// <param name="size">The size of the SQL Server data type.</param>
    public SqlServerDataType(SqlServerDataType.DataType type, string size) : this(type)
    {
      Size = size;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="SqlServerDataType"/> class with the specified type.
    /// </summary>
    /// <param name="type">The SQL Server data type.</param>
    public SqlServerDataType(SqlServerDataType.DataType type)
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
    /// Determines the category of a SQL Server data type.
    /// </summary>
    /// <param name="type">The SQL Server data type.</param>
    /// <returns>The category of the data type.</returns>
    private DbDataTypeCategory GetTypeCategory(SqlServerDataType.DataType type)
    {
      switch (type)
      {
        case SqlServerDataType.DataType.Int:
        case SqlServerDataType.DataType.BigInt:
        case SqlServerDataType.DataType.SmallInt:
        case SqlServerDataType.DataType.TinyInt:
        case SqlServerDataType.DataType.Bit:
        case SqlServerDataType.DataType.UniqueIdentifier:
          return DbDataTypeCategory.Int;

        case SqlServerDataType.DataType.Decimal:
        case SqlServerDataType.DataType.Float:
        case SqlServerDataType.DataType.Money:
        case SqlServerDataType.DataType.Numeric:
        case SqlServerDataType.DataType.Real:
        case SqlServerDataType.DataType.SmallMoney:
          return DbDataTypeCategory.Double;

        case SqlServerDataType.DataType.Date:
          return DbDataTypeCategory.Date;

        case SqlServerDataType.DataType.Time:
        case SqlServerDataType.DataType.SmallDateTime:
        case SqlServerDataType.DataType.DateTime:
        case SqlServerDataType.DataType.DateTime2:
        case SqlServerDataType.DataType.DateTimeOffset:
        case SqlServerDataType.DataType.Timestamp:
          return DbDataTypeCategory.DateTime;

        case SqlServerDataType.DataType.Char:
        case SqlServerDataType.DataType.NChar:
        case SqlServerDataType.DataType.NText:
        case SqlServerDataType.DataType.NVarChar:
        case SqlServerDataType.DataType.Text:
        case SqlServerDataType.DataType.VarChar:
          return DbDataTypeCategory.String;

        case SqlServerDataType.DataType.Binary:
        case SqlServerDataType.DataType.Image:
        case SqlServerDataType.DataType.VarBinary:
          return DbDataTypeCategory.Binary;

        default:
          return DbDataTypeCategory.Other;
      }
    }
  }
}
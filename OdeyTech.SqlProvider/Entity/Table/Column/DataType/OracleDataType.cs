// --------------------------------------------------------------------------
// <copyright file="OracleDataType.cs" author="Andrii Odeychuk">
//
// Copyright (c) Andrii Odeychuk. ALL RIGHTS RESERVED
// The entire contents of this file is protected by International Copyright Laws.
// </copyright>
// --------------------------------------------------------------------------

namespace OdeyTech.SqlProvider.Entity.Table.Column.DataType
{
  /// <summary>
  /// Represents an Oracle data type.
  /// </summary>
  public class OracleDataType : DbDataType
  {
    /// <summary>
    /// Enum representing the various data types in Oracle.
    /// </summary>
    public enum DataType
    {
      Char,
      NChar,
      Varchar2,
      NVarchar2,
      Clob,
      NClob,
      Blob,
      BFile,
      Number,
      BinaryFloat,
      BinaryDouble,
      Date,
      Timestamp,
      TimestampWithTimeZone,
      TimestampWithLocalTimeZone,
      IntervalYearToMonth,
      IntervalDayToSecond,
      Long,
      Raw,
      LongRaw,
      RowId,
      URowId
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="OracleDataType"/> class with the specified type and size.
    /// </summary>
    /// <param name="type">The Oracle data type.</param>
    /// <param name="size">The size of the Oracle data type.</param>
    public OracleDataType(OracleDataType.DataType type, string size) : this(type)
    {
      Size = size;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="OracleDataType"/> class with the specified type.
    /// </summary>
    /// <param name="type">The Oracle data type.</param>
    public OracleDataType(OracleDataType.DataType type)
    {
      TypeName = type.ToString();
      Category = GetTypeCategory(type);
    }

    /// <summary>
    /// Determines the category of an Oracle data type.
    /// </summary>
    /// <param name="type">The Oracle data type.</param>
    /// <returns>The category of the data type.</returns>
    private DbDataTypeCategory GetTypeCategory(OracleDataType.DataType type)
    {
      switch (type)
      {
        case OracleDataType.DataType.Char:
        case OracleDataType.DataType.NChar:
        case OracleDataType.DataType.Varchar2:
        case OracleDataType.DataType.NVarchar2:
        case OracleDataType.DataType.Clob:
        case OracleDataType.DataType.NClob:
        case OracleDataType.DataType.Long:
          return DbDataTypeCategory.String;

        case OracleDataType.DataType.Number:
        case OracleDataType.DataType.BinaryFloat:
        case OracleDataType.DataType.BinaryDouble:
          return DbDataTypeCategory.Double;

        case OracleDataType.DataType.Date:
        case OracleDataType.DataType.Timestamp:
        case OracleDataType.DataType.TimestampWithTimeZone:
        case OracleDataType.DataType.TimestampWithLocalTimeZone:
          return DbDataTypeCategory.DateTime;

        case OracleDataType.DataType.Blob:
        case OracleDataType.DataType.BFile:
        case OracleDataType.DataType.Raw:
        case OracleDataType.DataType.LongRaw:
          return DbDataTypeCategory.Binary;

        default:
          return DbDataTypeCategory.Other;
      }
    }
  }
}
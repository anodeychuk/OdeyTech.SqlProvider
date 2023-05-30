// --------------------------------------------------------------------------
// <copyright file="SqlColumnParameters.cs" author="Andrii Odeychuk">
//
// Copyright (c) Andrii Odeychuk. ALL RIGHTS RESERVED
// The entire contents of this file is protected by International Copyright Laws.
// </copyright>
// --------------------------------------------------------------------------

using OdeyTech.SqlProvider.DataType;

namespace OdeyTech.SqlProvider.Query
{
  /// <summary>
  /// Parameters of a SQL column.
  /// </summary>
  public class SqlColumnParameters
  {
    public SqlColumnParameters(IDbDataType dataType, object value) : this(dataType)
    {
      Value = value;
    }

    public SqlColumnParameters(IDbDataType dataType)
    {
      DataType = dataType;
    }

    /// <summary>
    /// Data type of the SQL column.
    /// </summary>
    public IDbDataType DataType { get; set; }

    /// <summary>
    /// Value of the SQL column.
    /// </summary>
    public object Value { get; set; }
  }
}
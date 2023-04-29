// --------------------------------------------------------------------------
// <copyright file="SqlDataType.cs" author="Andrii Odeychuk">
//
// Copyright (c) Andrii Odeychuk. ALL RIGHTS RESERVED
// The entire contents of this file is protected by International Copyright Laws.
// </copyright>
// --------------------------------------------------------------------------

namespace SqlProvider.Query
{
  /// <summary>
  /// Data types of SQL values.
  /// </summary>
  public enum SqlDataType
  {
    INT,
    DOUBLE,
    VARCHAR,
    DATE,
    DATETIME,
    BOOL
  }
}
// --------------------------------------------------------------------------
// <copyright file="SqliteValueConverter.cs" author="Andrii Odeychuk">
//
// Copyright (c) Andrii Odeychuk. ALL RIGHTS RESERVED
// The entire contents of this file is protected by International Copyright Laws.
// </copyright>
// --------------------------------------------------------------------------

using System;
using OdeyTech.SqlProvider.Entity.Table.Column.DataType;

namespace OdeyTech.SqlProvider.Entity.Table.Column.ValueConverter
{
  /// <summary>
  /// Provides a SQLite-specific implementation of the <see cref="IDbValueConverter"/> interface.
  /// </summary>
  public class SqliteValueConverter : IDbValueConverter
  {
    /// <inheritdoc/>
    public string ConvertToDbValue(object value, DbDataTypeCategory category)
      => value == null
        ? "NULL"
        : category switch
        {
          DbDataTypeCategory.String => $"'{value}'",
          DbDataTypeCategory.DateTime => $"unixepoch('{(DateTime)value:yyyy-MM-dd HH:mm:ss}')",
          DbDataTypeCategory.Date => $"unixepoch('{(DateTime)value:yyyy-MM-dd}')",
          DbDataTypeCategory.Boolean => Convert.ToBoolean(value) ? "1" : "0",
          _ => value.ToString(),
        };
  }
}
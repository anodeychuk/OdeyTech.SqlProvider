// --------------------------------------------------------------------------
// <copyright file="SQLiteValueConverter.cs" author="Andrii Odeychuk">
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
    public class SQLiteValueConverter : IDbValueConverter
    {
        /// <inheritdoc/>
        /// <exception cref="ArgumentException">Thrown when the value is not of the expected type for the specified category.</exception>
        public string ConvertToDbValue(object value, DbDataTypeCategory category)
            => value == null
                ? "NULL"
                : category switch
                {
                    DbDataTypeCategory.String => $"'{value}'",
                    DbDataTypeCategory.DateTime => value is DateTime date ? $"unixepoch('{date:yyyy-MM-dd HH:mm:ss}')" : throw new ArgumentException("The value must be of type DateTime"),
                    DbDataTypeCategory.Date => value is DateTime date ? $"unixepoch('{date:yyyy-MM-dd}')" : throw new ArgumentException("The value must be of type DateTime"),
                    DbDataTypeCategory.Boolean => Convert.ToBoolean(value) ? "1" : "0",
                    _ => value.ToString(),
                };
    }
}

// --------------------------------------------------------------------------
// <copyright file="IDbValueConverter.cs" author="Andrii Odeychuk">
//
// Copyright (c) Andrii Odeychuk. ALL RIGHTS RESERVED
// The entire contents of this file is protected by International Copyright Laws.
// </copyright>
// --------------------------------------------------------------------------

using OdeyTech.SqlProvider.Entity.Table.Column.DataType;

namespace OdeyTech.SqlProvider.Entity.Table.Column.ValueConverter
{
  /// <summary>
  /// Represents a converter for converting values to their corresponding database representation.
  /// </summary>
  public interface IDbValueConverter
  {
    /// <summary>
    /// Converts a value to its corresponding database representation.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <param name="category">The category of the database data type.</param>
    /// <returns>The converted value as a string.</returns>
    string ConvertToDbValue(object value, DbDataTypeCategory category);
  }
}
// --------------------------------------------------------------------------
// <copyright file="ColumnValue.cs" author="Andrii Odeychuk">
//
// Copyright (c) Andrii Odeychuk. ALL RIGHTS RESERVED
// The entire contents of this file is protected by International Copyright Laws.
// </copyright>
// --------------------------------------------------------------------------

using System;
using OdeyTech.SqlProvider.Entity.Table.Column.DataType;
using OdeyTech.SqlProvider.Entity.Table.Column.ValueConverter;

namespace OdeyTech.SqlProvider.Entity.Table.Column
{
    /// <summary>
    /// Represents the value of a column in a SQL query.
    /// </summary>
    public class ColumnValue
    {
        private object value;
        private IDbValueConverter converter;

        /// <summary>
        /// Gets the value of the column as a string.
        /// </summary>
        /// <returns>The string representation of the column value.</returns>
        /// <exception cref="InvalidOperationException">Thrown when DataType is null.</exception>
        public string GetValue()
        {
            CheckDataType();
            return ValueConverter.ConvertToDbValue(this.value, DataType.Category);
        }

        /// <summary>
        /// Sets the value of the column.
        /// </summary>
        /// <param name="value">The value to set for the column.</param>
        /// <exception cref="FormatException">Thrown when value does not match the sql data type.</exception>
        /// <exception cref="InvalidOperationException">Thrown when DataType is null.</exception>
        public void SetValue(object value)
        {
            CheckDataType();
            this.value = SanitizeSqlValue(value, DataType.Category);
        }

        /// <summary>
        /// Gets or sets the data type of the column.
        /// </summary>
        public IDbDataType DataType { get; set; }

        /// <summary>
        /// Gets or sets the value converter for the column.
        /// </summary>
        public IDbValueConverter ValueConverter
        {
            get => this.converter ??= new BasicDbValueConverter();
            set => this.converter = value;
        }

        private void CheckDataType()
        {
            if (DataType is null)
            {
                throw new InvalidOperationException("DataType cannot be null.");
            }
        }

        private object SanitizeSqlValue(object value, DbDataTypeCategory dataTypeCategory)
        {
            if (value is null)
            {
                return value;
            }

            switch (dataTypeCategory)
            {
                case DbDataTypeCategory.Int:
                    if (value is byte or sbyte or short or ushort or int or uint or long or ulong)
                    {
                        return value;
                    }

                    if (long.TryParse(value.ToString(), out var intValue))
                    {
                        return intValue;
                    }

                    break;

                case DbDataTypeCategory.Double:
                    if (value is float or double or decimal)
                    {
                        return value;
                    }

                    // Convert to a valid numeric format based on your database system's requirements
                    var filterValue = value.ToString().Replace(",", ".");
                    if (decimal.TryParse(filterValue, out var resultValue))
                    {
                        return resultValue;
                    }

                    break;

                case DbDataTypeCategory.Date:
                case DbDataTypeCategory.DateTime:
                    if (value is DateTime or DateTimeOffset)
                    {
                        return value;
                    }

                    break;

                case DbDataTypeCategory.Boolean:
                    if (value is bool)
                    {
                        return value;
                    }

                    if (bool.TryParse(value.ToString(), out var boolValue))
                    {
                        return boolValue;
                    }

                    break;

                case DbDataTypeCategory.String:
                case DbDataTypeCategory.Other:
                    // Escape single quotes by doubling them
                    var strValue = value.ToString().Replace("'", "''");
                    // Escape backslashes by doubling them
                    strValue = strValue.Replace("\\", "\\\\");
                    return strValue;
            }

            throw new FormatException($"Value does not match the sql data type: {value}.");
        }
    }
}

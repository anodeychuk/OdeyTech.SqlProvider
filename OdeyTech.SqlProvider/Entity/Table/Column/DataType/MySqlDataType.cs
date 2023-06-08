// --------------------------------------------------------------------------
// <copyright file="MySqlDataType.cs" author="Andrii Odeychuk">
//
// Copyright (c) Andrii Odeychuk. ALL RIGHTS RESERVED
// The entire contents of this file is protected by International Copyright Laws.
// </copyright>
// --------------------------------------------------------------------------

namespace OdeyTech.SqlProvider.Entity.Table.Column.DataType
{
    /// <summary>
    /// Represents a MySQL data type.
    /// </summary>
    public class MySqlDataType : DbDataType
    {
        /// <summary>
        /// Enum representing the various data types in MySQL.
        /// </summary>
        public enum DataType
        {
            BigInt,
            Binary,
            Bit,
            Char,
            Date,
            DateTime,
            Decimal,
            Double,
            Enum,
            Float,
            Geometry,
            Int,
            MediumInt,
            MediumText,
            LongText,
            TinyInt,
            Numeric,
            Real,
            Set,
            SmallInt,
            Text,
            Time,
            Timestamp,
            TinyText,
            VarChar,
            VarBinary,
            Year
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MySqlDataType"/> class with the specified type and size.
        /// </summary>
        /// <param name="type">The MySQL data type.</param>
        /// <param name="size">The size of the MySQL data type.</param>
        public MySqlDataType(MySqlDataType.DataType type, string size) : this(type)
        {
            Size = size;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MySqlDataType"/> class with the specified type.
        /// </summary>
        /// <param name="type">The MySQL data type.</param>
        public MySqlDataType(MySqlDataType.DataType type)
        {
            TypeName = type.ToString();
            Category = GetTypeCategory(type);
        }

        /// <summary>
        /// Determines the category of a MySQL data type.
        /// </summary>
        /// <param name="type">The MySQL data type.</param>
        /// <returns>The category of the data type.</returns>
        private DbDataTypeCategory GetTypeCategory(MySqlDataType.DataType type)
        {
            switch (type)
            {
                case MySqlDataType.DataType.Int:
                case MySqlDataType.DataType.BigInt:
                case MySqlDataType.DataType.SmallInt:
                case MySqlDataType.DataType.TinyInt:
                case MySqlDataType.DataType.MediumInt:
                    return DbDataTypeCategory.Int;

                case MySqlDataType.DataType.Decimal:
                case MySqlDataType.DataType.Double:
                case MySqlDataType.DataType.Float:
                case MySqlDataType.DataType.Real:
                case MySqlDataType.DataType.Numeric:
                    return DbDataTypeCategory.Double;

                case MySqlDataType.DataType.Date:
                    return DbDataTypeCategory.Date;

                case MySqlDataType.DataType.Time:
                case MySqlDataType.DataType.DateTime:
                case MySqlDataType.DataType.Timestamp:
                    return DbDataTypeCategory.DateTime;

                case MySqlDataType.DataType.Char:
                case MySqlDataType.DataType.Text:
                case MySqlDataType.DataType.VarChar:
                case MySqlDataType.DataType.TinyText:
                case MySqlDataType.DataType.MediumText:
                case MySqlDataType.DataType.LongText:
                    return DbDataTypeCategory.String;

                case MySqlDataType.DataType.Binary:
                case MySqlDataType.DataType.VarBinary:
                    return DbDataTypeCategory.Binary;

                default:
                    return DbDataTypeCategory.Other;
            }
        }
    }
}

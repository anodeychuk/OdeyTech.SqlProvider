// --------------------------------------------------------------------------
// <copyright file="PostgreSqlDataType.cs" author="Andrii Odeychuk">
//
// Copyright (c) Andrii Odeychuk. ALL RIGHTS RESERVED
// The entire contents of this file is protected by International Copyright Laws.
// </copyright>
// --------------------------------------------------------------------------

namespace OdeyTech.SqlProvider.Entity.Table.Column.DataType
{
    /// <summary>
    /// Represents a PostgreSQL data type.
    /// </summary>
    public class PostgreSqlDataType : DbDataType
    {
        /// <summary>
        /// Enum representing the various data types in PostgreSQL.
        /// </summary>
        public enum DataType
        {
            Int2,
            Int4,
            Int8,
            Numeric,
            Float4,
            Float8,
            Money,
            Char,
            Varchar,
            Text,
            Bytea,
            Timestamp,
            Date,
            Time,
            Interval,
            Boolean,
            Point,
            Line,
            Lseg,
            Box,
            Path,
            Polygon,
            Circle,
            Cidr,
            Inet,
            MacAddr,
            Bit,
            VarBit,
            Tsvector,
            Tsquery,
            Uuid,
            Xml,
            Json,
            Array,
            Composite,
            Range,
            PgLSN,
            PgTid,
            Unknown
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PostgreSqlDataType"/> class with the specified type and size.
        /// </summary>
        /// <param name="type">The PostgreSQL data type.</param>
        /// <param name="size">The size of the PostgreSQL data type.</param>
        public PostgreSqlDataType(PostgreSqlDataType.DataType type, string size = null)
        {
            Size = size;
            TypeName = type.ToString();
            Category = GetTypeCategory(type);
        }

        /// <summary>
        /// Determines the category of a PostgreSQL data type.
        /// </summary>
        /// <param name="type">The PostgreSQL data type.</param>
        /// <returns>The category of the data type.</returns>
        private DbDataTypeCategory GetTypeCategory(PostgreSqlDataType.DataType type)
        {
            switch (type)
            {
                case PostgreSqlDataType.DataType.Int2:
                case PostgreSqlDataType.DataType.Int4:
                case PostgreSqlDataType.DataType.Int8:
                    return DbDataTypeCategory.Int;

                case PostgreSqlDataType.DataType.Numeric:
                case PostgreSqlDataType.DataType.Float4:
                case PostgreSqlDataType.DataType.Float8:
                case PostgreSqlDataType.DataType.Money:
                    return DbDataTypeCategory.Double;

                case PostgreSqlDataType.DataType.Date:
                    return DbDataTypeCategory.Date;

                case PostgreSqlDataType.DataType.Time:
                case PostgreSqlDataType.DataType.Timestamp:
                    return DbDataTypeCategory.DateTime;

                case PostgreSqlDataType.DataType.Char:
                case PostgreSqlDataType.DataType.Varchar:
                case PostgreSqlDataType.DataType.Text:
                    return DbDataTypeCategory.String;

                case PostgreSqlDataType.DataType.Bytea:
                    return DbDataTypeCategory.Binary;

                case PostgreSqlDataType.DataType.Boolean:
                    return DbDataTypeCategory.Boolean;

                default:
                    return DbDataTypeCategory.Other;
            }
        }
    }
}

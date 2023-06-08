// --------------------------------------------------------------------------
// <copyright file="SqlColumn.cs" author="Andrii Odeychuk">
//
// Copyright (c) Andrii Odeychuk. ALL RIGHTS RESERVED
// The entire contents of this file is protected by International Copyright Laws.
// </copyright>
// --------------------------------------------------------------------------

using OdeyTech.SqlProvider.Entity.Table.Column.DataType;
using OdeyTech.SqlProvider.Entity.Table.Column.NameConverter;
using OdeyTech.SqlProvider.Entity.Table.Column.ValueConverter;
using OdeyTech.SqlProvider.Enum;

namespace OdeyTech.SqlProvider.Entity.Table.Column
{
    /// <summary>
    /// Represents a column in a SQL query.
    /// </summary>
    public class SqlColumn
    {
        private readonly ColumnName name;
        private readonly ColumnValue value;

        /// <summary>
        /// Initializes a new instance of the SqlColumn class with the specified name, data type, alias, value converter, and name converter.
        /// </summary>
        /// <param name="name">The name of the column.</param>
        /// <param name="dataType">The data type of the column.</param>
        /// <param name="alias">The alias for the column. (Optional)</param>
        /// <param name="valueConverter">The value converter for the column. (Optional)</param>
        /// <param name="nameConverter">The name converter for the column. (Optional)</param>
        public SqlColumn(string name, IDbDataType dataType, string alias = null, IDbValueConverter valueConverter = null, INameConverter nameConverter = null)
        {
            this.name = new ColumnName(name, alias, nameConverter);
            this.value = new ColumnValue()
            {
                DataType = dataType,
                ValueConverter = valueConverter
            };
        }

        /// <summary>
        /// Gets the data type of the SQL column.
        /// </summary>
        public IDbDataType DataType => this.value.DataType;

        /// <summary>
        /// Gets or sets a value indicating whether the column is excluded.
        /// </summary>
        public bool IsExcluded { get; set; }

        /// <summary>
        /// Gets the name of the column based on the specified SQL query type.
        /// </summary>
        /// <param name="sqlQueryType">The SQL query type.</param>
        /// <returns>The name of the column.</returns>
        public string GetName(SqlQueryType sqlQueryType = SqlQueryType.Create) => this.name.GetName(sqlQueryType);

        /// <summary>
        /// Gets the value of the column.
        /// </summary>
        /// <returns>The value of the column.</returns>
        public string GetValue() => this.value.GetValue();

        /// <summary>
        /// Sets the value of the column.
        /// </summary>
        /// <param name="value">The value to set for the column.</param>
        public void SetValue(object value) => this.value.SetValue(value);
    }
}

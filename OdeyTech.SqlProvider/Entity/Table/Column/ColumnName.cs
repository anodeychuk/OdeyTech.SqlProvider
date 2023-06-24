// --------------------------------------------------------------------------
// <copyright file="ColumnName.cs" author="Andrii Odeychuk">
//
// Copyright (c) Andrii Odeychuk. ALL RIGHTS RESERVED
// The entire contents of this file is protected by International Copyright Laws.
// </copyright>
// --------------------------------------------------------------------------

using System;
using OdeyTech.SqlProvider.Entity.Table.Column.NameConverter;
using OdeyTech.SqlProvider.Enum;

namespace OdeyTech.SqlProvider.Entity.Table.Column
{
    /// <summary>
    /// Represents the name of a column in a SQL query.
    /// </summary>
    public class ColumnName : ICloneable
    {
        private readonly string name;
        private readonly string alias;
        private readonly INameConverter converter;

        /// <summary>
        /// Initializes a new instance of the <see cref="ColumnName"/> class with the specified name, alias, and name converter.
        /// </summary>
        /// <param name="name">The name of the column. This parameter cannot be null.</param>
        /// <param name="alias">The alias for the column. This parameter can be null, in which case the column will not be aliased.</param>
        /// <param name="converter">The name converter for the column. This parameter can be null, in which case a <see cref="BasicNameConverter"/> will be used.</param>
        /// <exception cref="ArgumentException">Thrown when the name parameter is null.</exception>
        public ColumnName(string name, string alias, INameConverter converter)
        {
            this.name = name ?? throw new ArgumentException(nameof(name));
            this.alias = alias;
            this.converter = converter ?? new BasicNameConverter();
        }

        /// <summary>
        /// Gets the name of the column, with optional aliasing, based on the SQL query type.
        /// </summary>
        /// <param name="sqlQueryType">The SQL query type. If this is <see cref="SqlQueryType.Select"/>, the column name will be aliased if an alias was provided; otherwise, the original column name will be returned.</param>
        /// <returns>The name of the column, with optional aliasing.</returns>
        public string GetName(SqlQueryType sqlQueryType)
            => sqlQueryType == SqlQueryType.Select
                ? this.converter.ConvertName(this.name, this.alias)
                : this.name;

        /// <inheritdoc/>
        public object Clone() => new ColumnName(this.name, this.alias, this.converter);
    }
}

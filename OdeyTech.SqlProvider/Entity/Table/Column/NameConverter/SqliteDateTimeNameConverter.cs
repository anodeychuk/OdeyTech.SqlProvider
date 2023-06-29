// --------------------------------------------------------------------------
// <copyright file="SQLiteDateTimeNameConverter.cs" author="Andrii Odeychuk">
//
// Copyright (c) Andrii Odeychuk. ALL RIGHTS RESERVED
// The entire contents of this file is protected by International Copyright Laws.
// </copyright>
// --------------------------------------------------------------------------

using System;
using OdeyTech.ProductivityKit;
using OdeyTech.ProductivityKit.Extension;

namespace OdeyTech.SqlProvider.Entity.Table.Column.NameConverter
{
    /// <summary>
    /// Represents a name converter for SQLite that converts column names to the datetime function with 'unixepoch' format and includes an optional alias.
    /// </summary>
    /// <exception cref="ArgumentNullException">Thrown when the name is null or empty.</exception>
    public class SQLiteDateTimeNameConverter : INameConverter
    {
        /// <inheritdoc/>
        public string ConvertName(string name, string alias)
        {
            ThrowHelper.ThrowIfNullOrEmpty(name, nameof(name));
            return $"datetime({name}, 'unixepoch') AS {(alias.IsNullOrEmpty() ? name : alias)}";
        }
    }
}

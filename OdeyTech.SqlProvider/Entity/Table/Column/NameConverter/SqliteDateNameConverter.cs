// --------------------------------------------------------------------------
// <copyright file="SQLiteDateNameConverter.cs" author="Andrii Odeychuk">
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
    /// Represents a name converter for SQLite that converts column names to the date function with 'unixepoch' format and includes an optional alias.
    /// </summary>
    public class SQLiteDateNameConverter : INameConverter
    {
        /// <inheritdoc/>
        /// <exception cref="ArgumentNullException">Thrown when the name is null or empty.</exception>
        public string ConvertName(string name, string alias)
        {
            ThrowHelper.ThrowIfNullOrEmpty(name, nameof(name));
            return $"date({name}, 'unixepoch') AS {(alias.IsNullOrEmpty() ? name : alias)}";
        }
    }
}

// --------------------------------------------------------------------------
// <copyright file="BasicNameConverter.cs" author="Andrii Odeychuk">
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
    /// Provides a basic implementation of the <see cref="INameConverter"/> interface.
    /// </summary>
    public class BasicNameConverter : INameConverter
    {
        /// <summary>
        /// Converts a name to a SQL alias, if an alias is provided.
        /// </summary>
        /// <param name="name">The original name to convert.</param>
        /// <param name="alias">The alias to use. If this parameter is null or empty, the original name is returned.</param>
        /// <returns>The original name, or the original name followed by "AS" and the alias, if an alias is provided.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the name parameter is null or empty.</exception>
        public string ConvertName(string name, string alias)
        {
            ThrowHelper.ThrowIfNullOrEmpty(name, nameof(name));
            return alias.IsNullOrEmpty()
                ? name
                : $"{name} AS {alias}";
        }
    }
}

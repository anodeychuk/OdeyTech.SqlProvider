﻿// --------------------------------------------------------------------------
// <copyright file="SqliteDateTimeNameConverter.cs" author="Andrii Odeychuk">
//
// Copyright (c) Andrii Odeychuk. ALL RIGHTS RESERVED
// The entire contents of this file is protected by International Copyright Laws.
// </copyright>
// --------------------------------------------------------------------------

using OdeyTech.ProductivityKit.Extension;

namespace OdeyTech.SqlProvider.Entity.Table.Column.NameConverter
{
  /// <summary>
  /// Represents a name converter for SQLite that converts column names to the datetime function with 'unixepoch' format and includes an optional alias.
  /// </summary>
  public class SqliteDateTimeNameConverter : INameConverter
  {
    /// <inheritdoc/>
    public string ConvertName(string name, string alias)
        => $"datetime({name}, 'unixepoch') AS {(alias.IsNullOrEmpty() ? name : alias)}";
  }
}
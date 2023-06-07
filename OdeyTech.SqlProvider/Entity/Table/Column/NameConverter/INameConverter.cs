// --------------------------------------------------------------------------
// <copyright file="INameConverter.cs" author="Andrii Odeychuk">
//
// Copyright (c) Andrii Odeychuk. ALL RIGHTS RESERVED
// The entire contents of this file is protected by International Copyright Laws.
// </copyright>
// --------------------------------------------------------------------------

namespace OdeyTech.SqlProvider.Entity.Table.Column.NameConverter
{
  /// <summary>
  /// Represents a name converter that converts column names and aliases.
  /// </summary>
  public interface INameConverter
  {
    /// <summary>
    /// Converts a column name and its alias according to the implementation logic.
    /// </summary>
    /// <param name="name">The original column name.</param>
    /// <param name="alias">The column alias.</param>
    /// <returns>The converted column name.</returns>
    string ConvertName(string name, string alias);
  }
}
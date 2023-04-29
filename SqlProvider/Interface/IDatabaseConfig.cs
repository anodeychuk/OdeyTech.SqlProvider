// --------------------------------------------------------------------------
// <copyright file="IDatabaseConfig.cs" author="Andrii Odeychuk">
//
// Copyright (c) Andrii Odeychuk. ALL RIGHTS RESERVED
// The entire contents of this file is protected by International Copyright Laws.
// </copyright>
// --------------------------------------------------------------------------

namespace SqlProvider.Interface
{
  /// <summary>
  /// Defines the configuration for a database.
  /// </summary>
  public interface IDatabaseConfig
  {
    /// <summary>
    /// Gets the connection string for the database.
    /// </summary>
    /// <returns>The connection string.</returns>
    string GetConnectionString();
  }
}
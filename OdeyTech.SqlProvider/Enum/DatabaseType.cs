// --------------------------------------------------------------------------
// <copyright file="DatabaseType.cs" author="Andrii Odeychuk">
//
// Copyright (c) Andrii Odeychuk. ALL RIGHTS RESERVED
// The entire contents of this file is protected by International Copyright Laws.
// </copyright>
// --------------------------------------------------------------------------

namespace OdeyTech.Data.Repository.Checker
{
  /// <summary>
  /// Enum representing different types of databases.
  /// </summary>
  public enum DatabaseType
  {
    /// <summary>
    /// Represents a MySQL database.
    /// </summary>
    MySql,

    /// <summary>
    /// Represents a Microsoft SQL Server database.
    /// </summary>
    SqlServer,

    /// <summary>
    /// Represents an Oracle database.
    /// </summary>
    Oracle,

    /// <summary>
    /// Represents a PostgreSQL database.
    /// </summary>
    PostgreSql,

    /// <summary>
    /// Represents a SQLite database.
    /// </summary>
    SQLite
  }
}
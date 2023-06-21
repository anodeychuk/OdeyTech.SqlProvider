// --------------------------------------------------------------------------
// <copyright file="DatabaseType.cs" author="Andrii Odeychuk">
//
// Copyright (c) Andrii Odeychuk. ALL RIGHTS RESERVED
// The entire contents of this file is protected by International Copyright Laws.
// </copyright>
// --------------------------------------------------------------------------

namespace OdeyTech.SqlProvider.Entity.Database
{
    /// <summary>
    /// Specifies the type of the database.
    /// </summary>
    public enum DatabaseType
    {
        MySql,
        Oracle,
        PostgreSql,
        SQLite,
        SqlServer
    }
}

﻿// --------------------------------------------------------------------------
// <copyright file="IDbChecker.cs" author="Andrii Odeychuk">
//
// Copyright (c) Andrii Odeychuk. ALL RIGHTS RESERVED
// The entire contents of this file is protected by International Copyright Laws.
// </copyright>
// --------------------------------------------------------------------------

using OdeyTech.SqlProvider.Entity.Table;

namespace OdeyTech.SqlProvider.Entity.Database.Checker
{
    /// <summary>
    /// Represents an interface for checking the existence of a database and its items.
    /// </summary>
    public interface IDbChecker
    {
        /// <summary>
        /// Gets the SQL query source for the database item.
        /// </summary>
        SqlTable DatabaseItemSource { get; set; }

        /// <summary>
        /// Checks the database and creates the database item if it does not exist.
        /// </summary>
        void CheckDatabase();
    }
}

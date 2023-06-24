// --------------------------------------------------------------------------
// <copyright file="DbDataType.cs" author="Andrii Odeychuk">
//
// Copyright (c) Andrii Odeychuk. ALL RIGHTS RESERVED
// The entire contents of this file is protected by International Copyright Laws.
// </copyright>
// --------------------------------------------------------------------------

using OdeyTech.ProductivityKit.Extension;

namespace OdeyTech.SqlProvider.Entity.Table.Column.DataType
{
    /// <summary>
    /// Represents a database data type.
    /// </summary>
    public abstract class DbDataType : IDbDataType
    {
        /// <inheritdoc/>
        public string TypeName { get; protected set; }

        /// <inheritdoc/>
        public DbDataTypeCategory Category { get; protected set; }

        /// <inheritdoc/>
        public string Size { get; protected set; }

        /// <inheritdoc/>
        public override string ToString() => Size.IsNullOrEmpty() ? TypeName : $"{TypeName}({Size})";
    }
}

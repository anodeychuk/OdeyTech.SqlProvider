// --------------------------------------------------------------------------
// <copyright file="ForeignKeyConstraint.cs" author="Andrii Odeychuk">
//
// Copyright (c) Andrii Odeychuk. ALL RIGHTS RESERVED
// The entire contents of this file is protected by International Copyright Laws.
// </copyright>
// --------------------------------------------------------------------------

using System.Text;
using OdeyTech.ProductivityKit.Extension;

namespace OdeyTech.SqlProvider.Entity.Table.Column.Constraint
{
    /// <summary>
    /// Foreign key constraint.
    /// </summary>
    public class ForeignKeyConstraint : IConstraint
    {
        /// <inheritdoc/>
        public SqlConstraintType Type => SqlConstraintType.Foreign;

        /// <summary>
        /// Gets or sets the name of the foreign key constraint.
        /// </summary>
        public string ConstraintName { get; set; }

        /// <summary>
        /// Gets or sets the name of the column associated with the foreign key constraint.
        /// </summary>
        public string ColumnName { get; set; }

        /// <summary>
        /// Gets or sets the name of the referenced table in the foreign key constraint.
        /// </summary>
        public string ReferenceTable { get; set; }

        /// <summary>
        /// Gets or sets the name of the referenced column in the foreign key constraint.
        /// </summary>
        public string ReferenceColumn { get; set; }

        /// <summary>
        /// Generates a SQL constraint for a foreign key.
        /// </summary>
        /// <returns>A SQL string representing the foreign key constraint.</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            if (ConstraintName.IsFilled())
            {
                sb.Append($"CONSTRAINT {ConstraintName} ");
            }

            sb.Append($"Foreign KEY ({ColumnName}) REFERENCES {ReferenceTable}({ReferenceColumn})");

            return sb.ToString();
        }
    }
}

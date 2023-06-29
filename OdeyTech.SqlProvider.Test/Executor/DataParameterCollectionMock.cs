// --------------------------------------------------------------------------
// <copyright file="DataParameterCollectionMock.cs" author="Andrii Odeychuk">
//
// Copyright (c) Andrii Odeychuk. ALL RIGHTS RESERVED
// The entire contents of this file is protected by International Copyright Laws.
// </copyright>
// --------------------------------------------------------------------------

using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using OdeyTech.ProductivityKit;

namespace OdeyTech.SqlProvider.Test.Executor
{
    internal class DataParameterCollectionMock : List<DbParameter>, IDataParameterCollection
    {
        public object this[string parameterName]
        {
            get => this.FirstOrDefault(param => param.ParameterName == parameterName)?.Value;
            set
            {
                DbParameter param = this.FirstOrDefault(p => p.ParameterName == parameterName);
                ThrowHelper.ThrowIfNull(param, nameof(param), $"Parameter '{parameterName}' not found.");
                param.Value = value;
            }
        }

        public bool Contains(string parameterName) => this.Any(param => param.ParameterName == parameterName);

        public int IndexOf(string parameterName) => this.FindIndex(param => param.ParameterName == parameterName);

        public void RemoveAt(string parameterName)
        {
            DbParameter param = this.FirstOrDefault(p => p.ParameterName == parameterName);
            ThrowHelper.ThrowIfNull(param, nameof(param), $"Parameter '{parameterName}' not found.");
            this.Remove(param);
        }
    }
}

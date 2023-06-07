// --------------------------------------------------------------------------
// <copyright file="SqlExecutorException.cs" author="Andrii Odeychuk">
//
// Copyright (c) Andrii Odeychuk. ALL RIGHTS RESERVED
// The entire contents of this file is protected by International Copyright Laws.
// </copyright>
// --------------------------------------------------------------------------

using System;
using System.Data.SqlClient;
using System.Text;

namespace OdeyTech.SqlProvider.Executor
{
  /// <summary>
  /// Exception that is thrown when a SQL query execution fails.
  /// </summary>
  public class SqlExecutorException : Exception
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="SqlExecutorException"/> class.
    /// </summary>
    public SqlExecutorException() : base()
    { }

    /// <summary>
    /// Initializes a new instance of the <see cref="SqlExecutorException"/> class with the specified error message.
    /// </summary>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    public SqlExecutorException(string message) : base(message)
    { }

    /// <summary>
    /// Initializes a new instance of the <see cref="SqlExecutorException"/> class with the specified error message and inner exception.
    /// </summary>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    /// <param name="innerException">The inner exception that caused the current exception.</param>
    public SqlExecutorException(string message, Exception innerException) : base(message, innerException)
    { }

    /// <summary>
    /// Initializes a new instance of the <see cref="SqlExecutorException"/> class with the specified SQL exception.
    /// </summary>
    /// <param name="exception">The SQL exception that caused the exception.</param>
    public SqlExecutorException(SqlException exception) : base(GetSqlExceptionMessage(exception))
    { }

    /// <summary>
    /// Gets the error message for a SQL exception.
    /// </summary>
    /// <param name="exception">The SQL exception.</param>
    /// <returns>The error message.</returns>
    private static string GetSqlExceptionMessage(SqlException exception)
    {
      var errorMessages = new StringBuilder();

      for (var i = 0; i < exception.Errors.Count; i++)
      {
        errorMessages.Append($"Sql-error #{i}\nMessage: {exception.Errors[i].Message}\nLineNumber: {exception.Errors[i].LineNumber}\nSource: {exception.Errors[i].Source}\nProcedure: {exception.Errors[i].Procedure}\n");
      }

      return errorMessages.ToString();
    }
  }
}
# OdeyTech.SqlProvider

**OdeyTech.SqlProvider** is a robust, user-friendly, and efficient C# library designed to simplify and streamline SQL query generation and execution for .NET applications.

## Features

- Strongly-typed column and table definitions for increased maintainability and reduced errors.
- Flexible and composable query generation, allowing you to build complex queries with ease:
    - **`SqlQueryGenerator`**: A powerful utility that allows you to easily generate various SQL queries, such as `SELECT`, `INSERT`, `UPDATE`, and `DELETE`, using the SqlQuerySource object.
    - **`SqlQuerySource`**: A flexible and extensible class to define the structure of your SQL queries, including table names, columns, join statements, conditions, and more.
    - **`ColumnValues`**: Represents a collection of column values to be used in SQL queries, allowing you to add, update, or exclude columns and their values.
    - **`SqlType`**: Defines the different types of SQL queries supported by the library: Select, Insert, Update, and Delete.
- Intuitive and easy-to-understand API, designed with both experienced and novice developers in mind.
- Comprehensive query execution features, including support for stored procedures and functions.
- Lightweight and efficient codebase, making it suitable for use in projects of any size.
- Seamless integration with other projects through NuGet package distribution.

## Sample Usage
Using `OdeyTech.SqlProvider`, you can easily create and manage complex SQL queries. Here are examples of how you can use this library for `SELECT`, `INSERT`, `UPDATE`, and `DELETE` operations:

~~~csharp
// Define a database configuration
IDatabaseConfig dbConfig = new YourDatabaseConfig();

// Create an instance of SqlExecutor
ISqlExecutor sqlExecutor = new SqlExecutor();
sqlExecutor.SetDbConnection(new SqlConnection(dbConfig.GetConnectionString()));

// Define a SqlQuerySource
SqlQuerySource querySource = new SqlQuerySource();
querySource.SetTable("Users");
querySource.AddColumns("Id", "FirstName", "LastName", "Email");

// SELECT Example
string selectQuery = SqlQueryGenerator.Select(querySource);
DataTable result = sqlExecutor.Select(selectQuery);

// INSERT Example
querySource.AddColumnWithValue("FirstName", new SqlValue("John"));
querySource.AddColumnWithValue("LastName", new SqlValue("Doe"));
querySource.AddColumnWithValue("Email", new SqlValue("john.doe@example.com"));
string insertQuery = SqlQueryGenerator.Insert(querySource);
int insertResult = sqlExecutor.ExecuteNonQuery(insertQuery);

// UPDATE Example
querySource.AddConditions("Id = 1");
querySource.SetValue("FirstName", new SqlValue("Jane"));
querySource.SetValue("LastName", new SqlValue("Doe"));
querySource.SetValue("Email", new SqlValue("jane.doe@example.com"));
string updateQuery = SqlQueryGenerator.Update(querySource);
int updateResult = sqlExecutor.ExecuteNonQuery(updateQuery);

// DELETE Example
querySource.ClearConditions();
querySource.AddConditions("Id = 2");
string deleteQuery = SqlQueryGenerator.Delete(querySource);
int deleteResult = sqlExecutor.ExecuteNonQuery(deleteQuery);
~~~
`OdeyTech.SqlProvider` provides a clean, efficient, and professional solution for working with SQL databases. The comprehensive nature of the library ensures it is well-suited for various projects.

## Getting Started
To start using `OdeyTech.SqlProvider`, install it as a NuGet package in your C# project. Follow our step-by-step guide and sample code snippets to leverage the power of OdeyTech.SqlProvider in your applications.

## Contributing
We welcome contributions to `OdeyTech.SqlProvider`! Feel free to submit pull requests or raise issues to help us improve the library.

## License
`OdeyTech.SqlProvider` is released under the [MIT License][LICENSE]. See the LICENSE file for more information.

## Stay in Touch
For more information, updates, and future releases, follow me on [LinkedIn][LIn] I'd be happy to connect and discuss any questions or ideas you might have.

[//]: #
   [LIn]: <https://www.linkedin.com/in/anodeychuk/>
   [LICENSE]: <https://github.com/anodeychuk/OdeyTech.ProductivityKit/blob/main/LICENSE>

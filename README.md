# OdeyTech.SqlProvider

**OdeyTech.SqlProvider** is a robust, user-friendly, and efficient C# library designed to simplify and streamline SQL query generation and execution for .NET applications.

## Features

- **Strongly-typed Column and Table Definitions**: The library provides strongly-typed column and table definitions, which enhance maintainability and reduce errors in your code.
- **Flexible Query Generation**: With `OdeyTech.SqlProvider`, you can easily build complex SQL queries using the `SqlQueryGenerator` utility. It supports various query types such as `CREATE TABLE`, `SELECT`, `INSERT`, `UPDATE`, and `DELETE`, utilizing the `SqlQuerySource` object.
    - **`SqlQueryGenerator`**: This powerful utility enables you to generate different types of SQL queries, leveraging the capabilities of the `SqlQuerySource` object.
    - **`SqlQuerySource`**: This flexible and extensible class allows you to define the structure of your SQL queries, including table names, columns, join statements, conditions, and more.
    - **`SqlColumns`**: Represents a collection of column values for use in SQL queries. It provides methods to add, update, or exclude columns and their values easily.
- **Intuitive API**: The library offers an intuitive and easy-to-understand API, catering to both experienced and novice developers.
- **Comprehensive Query Execution**: `OdeyTech.SqlProvider` provides comprehensive query execution features, including support for stored procedures and functions.
- **Lightweight and Efficient**: The library is designed to be lightweight and efficient, ensuring optimal performance in projects of any size.
- **Seamless Integration**: `OdeyTech.SqlProvider` seamlessly integrates with other projects through the NuGet package distribution.

## Sample Usage
Using `OdeyTech.SqlProvider`, you can easily create and manage complex SQL queries. Here are examples of how you can use this library for `CREATE TABLE`, `SELECT`, `INSERT`, `UPDATE`, and `DELETE` operations:

~~~csharp
// Define a database configuration
IDatabaseConfig dbConfig = new YourDatabaseConfig();

// Create an instance of SqlExecutor
ISqlExecutor sqlExecutor = new SqlExecutor();
sqlExecutor.SetDbConnection(new SqlConnection(dbConfig.GetConnectionString()));

// Define a SqlQuerySource
SqlQuerySource querySource = new SqlQuerySource();
querySource.SetTable("Users");

// Define columns for the SQL query source
SqlColumns columns = new SqlColumns();
columns.AddColumn("Id", new SqlColumnParameters(new MySqlDataType(MySqlDataType.DataType.Int)));
columns.AddColumn("FirstName", new SqlColumnParameters(new MySqlDataType(MySqlDataType.DataType.VarChar)));
columns.AddColumn("LastName", new SqlColumnParameters(new MySqlDataType(MySqlDataType.DataType.VarChar)));
columns.AddColumn("Email", new SqlColumnParameters(new MySqlDataType(MySqlDataType.DataType.VarChar)));

// Add primary key constraint for the "Id" column
PrimaryKey primaryKey = new PrimaryKey();
primaryKey.ColumnNames = new List<string> { "Id" };
querySource.AddConstraints(primaryKey);

querySource.Columns = columns;

// CREATE TABLE Example
string createTableQuery = SqlQueryGenerator.Create(querySource);
sqlExecutor.ExecuteNonQuery(createTableQuery);

// SELECT Example
string selectQuery = SqlQueryGenerator.Select(querySource);
DataTable result = sqlExecutor.Select(selectQuery);

// INSERT Example
SqlColumns insertColumns = new SqlColumns();
insertColumns.AddColumn("FirstName", new SqlColumnParameters(new MySqlDataType(MySqlDataType.DataType.VarChar), "John"));
insertColumns.AddColumn("LastName", new SqlColumnParameters(new MySqlDataType(MySqlDataType.DataType.VarChar), "Doe"));
insertColumns.AddColumn("Email", new SqlColumnParameters(new MySqlDataType(MySqlDataType.DataType.VarChar), "john.doe@example.com"));

querySource.Columns = insertColumns;

string insertQuery = SqlQueryGenerator.Insert(querySource);
int insertResult = sqlExecutor.ExecuteNonQuery(insertQuery);

// UPDATE Example
SqlColumns updateColumns = new SqlColumns();
updateColumns.AddColumn("FirstName", new SqlColumnParameters(new MySqlDataType(MySqlDataType.DataType.VarChar), "Jane"));
updateColumns.AddColumn("LastName", new SqlColumnParameters(new MySqlDataType(MySqlDataType.DataType.VarChar), "Doe"));
updateColumns.AddColumn("Email", new SqlColumnParameters(new MySqlDataType(MySqlDataType.DataType.VarChar), "jane.doe@example.com"));

querySource.Columns = updateColumns;
querySource.ClearConditions();
querySource.AddConditions("Id = 1");

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
To start using `OdeyTech.SqlProvider`, install it as a NuGet package in your C# project. Follow our step-by-step guide and sample code snippets to leverage the power of `OdeyTech.SqlProvider` in your applications.

## Contributing
We welcome contributions to `OdeyTech.SqlProvider`! Feel free to submit pull requests or raise issues to help us improve the library.

## License
`OdeyTech.SqlProvider` is released under the [MIT License][LICENSE]. See the LICENSE file for more information.

## Stay in Touch
For more information, updates, and future releases, follow me on [LinkedIn][LIn] I'd be happy to connect and discuss any questions or ideas you might have.

[//]: #
   [LIn]: <https://www.linkedin.com/in/anodeychuk/>
   [LICENSE]: <https://github.com/anodeychuk/OdeyTech.ProductivityKit/blob/main/LICENSE>

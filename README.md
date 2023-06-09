# OdeyTech.SqlProvider

**OdeyTech.SqlProvider** is a robust, user-friendly, and efficient C# library designed to simplify and optimize the formation and execution of SQL queries for most popular RDBMS (MySQL, SQL Server, PostgreSQL, Oracle, SQLite) in .NET applications.

## Features

- **Strongly-typed Column and Table Definitions**: The library provides strongly-typed column and table definitions, which enhance maintainability and reduce errors in your code.
- **Flexible Query Generation**: With **OdeyTech.SqlProvider**, you can easily build complex SQL queries using the `SqlQueryGenerator` utility. It supports various query types such as `CREATE TABLE`, `SELECT`, `INSERT`, `UPDATE`, and `DELETE`, using the object `SqlTable`.
    - **`SqlQueryGenerator`**: This powerful utility enables you to generate different types of SQL queries, leveraging the capabilities of the `SqlTable` object.
    - **`SqlTable`**: This class represents a SQL table in a database, providing a structured way to interact with and manipulate the table. It allows for the setting and retrieval of table names, prefix, and columns, as well as the adding and clearing of join, condition, and order by statements.
    - **`SqlColumns`**: This class represents the columns of a SQL table, providing a structured way to interact with and manipulate the columns.
    - **`SqlColumn`**: This class represents a column in a SQL query. It provides a structured way to define and interact with a column, including its name, data type, alias, and converters for value and name.
- **Automated Database Verification and Creation**: The library includes built-in verification to check for the existence of the database and its necessary structures, such as tables and their relationships. If they are absent, it also has the capability to create them.	
- **Intuitive API**: The library offers an intuitive and easy-to-understand API, catering to both experienced and novice developers.
- **Comprehensive Query Execution**: **OdeyTech.SqlProvider** provides comprehensive query execution features, including support for stored procedures and functions.
- **Lightweight and Efficient**: The library is designed to be lightweight and efficient, ensuring optimal performance in projects of any size.
- **Seamless Integration**: **OdeyTech.SqlProvider** seamlessly integrates with other projects through the NuGet package distribution.

## Sample Usage
Using **OdeyTech.SqlProvider**, you can easily create and manage complex SQL queries. Here are examples of how you can use this library for `CREATE TABLE`, `SELECT`, `INSERT`, `UPDATE`, and `DELETE` operations:

~~~csharp
// Define a database configuration
IDatabaseConfig dbConfig = new YourDatabaseConfig();

// Create an instance of SqlExecutor
ISqlExecutor sqlExecutor = new SqlExecutor();
sqlExecutor.SetDbConnection(new SqlConnection(dbConfig.GetConnectionString()));

// Define a SqlTable
SqlTable tableSource = new SqlTable();
tableSource.SetName("Users");

// Define columns for the SQL table
tableSource.Columns.AddColumn("Id", new MySqlDataType(MySqlDataType.DataType.Int));
tableSource.Columns.AddColumn("FirstName", new MySqlDataType(MySqlDataType.DataType.VarChar));
tableSource.Columns.AddColumn("LastName", new MySqlDataType(MySqlDataType.DataType.VarChar));
tableSource.Columns.AddColumn("Email", new MySqlDataType(MySqlDataType.DataType.VarChar));

// Add primary key constraint for the "Id" column
var primaryKey = new PrimaryKeyConstraint() { ColumnNames = new List<string> { "Id" } };
tableSource.Columns.AddConstraints(primaryKey);

// CREATE TABLE Example
string createTableQuery = SqlQueryGenerator.Create(tableSource);
sqlExecutor.ExecuteNonQuery(createTableQuery);

// SELECT Example
string selectQuery = SqlQueryGenerator.Select(tableSource);
DataTable result = sqlExecutor.Select(selectQuery);

// INSERT Example
tableSource.Columns.SetValue("Id", 1));
tableSource.Columns.SetValue("FirstName", "John"));
tableSource.Columns.SetValue("LastName", "Doe"));
tableSource.Columns.SetValue("Email", "john.doe@example.com"));

string insertQuery = SqlQueryGenerator.Insert(tableSource);
int insertResult = sqlExecutor.ExecuteNonQuery(insertQuery);

// UPDATE Example
tableSource.Columns.GetColumn("Id").IsExcluded = true;
tableSource.Columns.SetValue("FirstName", "John"));
tableSource.Columns.SetValue("LastName", "Doe"));
tableSource.Columns.SetValue("Email", "john.doe@example.com"));
tableSource.AddConditions("Id = 1");

string updateQuery = SqlQueryGenerator.Update(tableSource);
int updateResult = sqlExecutor.ExecuteNonQuery(updateQuery);

// DELETE Example
tableSource.ClearConditions();
tableSource.AddConditions("Id = 2");

string deleteQuery = SqlQueryGenerator.Delete(tableSource);
int deleteResult = sqlExecutor.ExecuteNonQuery(deleteQuery);
~~~
**OdeyTech.SqlProvider** provides a clean, efficient, and professional solution for working with SQL databases. The comprehensive nature of the library ensures it is well-suited for various projects.

**Note:** An example of how to use **OdeyTech.SqlProvider** can be found in the [OdeyTech.WPF.Example.Hospital repository][Example].

## Getting Started
To start using **OdeyTech.SqlProvider**, install it as a NuGet package in your C# project. Follow our step-by-step guide and sample code snippets to leverage the power of **OdeyTech.SqlProvider** in your applications.

## Contributing
We welcome contributions to **OdeyTech.SqlProvider**! Feel free to submit pull requests or raise issues to help us improve the library.

## License
**OdeyTech.SqlProvider** is released under the [Non-Commercial License][LICENSE]. See the LICENSE file for more information.

## Stay in Touch
For more information, updates, and future releases, follow me on [LinkedIn][LIn] I'd be happy to connect and discuss any questions or ideas you might have.

[//]: #
   [LIn]: <https://www.linkedin.com/in/anodeychuk/>
   [LICENSE]: <https://github.com/anodeychuk/OdeyTech.SqlProvider/blob/main/LICENSE>
   [Example]: <https://github.com/anodeychuk/OdeyTech.WPF.Example.Hospital>
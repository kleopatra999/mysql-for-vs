﻿// Copyright © 2016, Oracle and/or its affiliates. All rights reserved.
//
// MySQL for Visual Studio is licensed under the terms of the GPLv2
// <http://www.gnu.org/licenses/old-licenses/gpl-2.0.html>, like most
// MySQL Connectors. There are special exceptions to the terms and
// conditions of the GPLv2 as it is applied to this software, see the
// FLOSS License Exception
// <http://www.mysql.com/about/legal/licensing/foss-exception.html>.
//
// This program is free software; you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published
// by the Free Software Foundation; version 2 of the License.
//
// This program is distributed in the hope that it will be useful, but
// WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY
// or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License
// for more details.
//
// You should have received a copy of the GNU General Public License along
// with this program; if not, write to the Free Software Foundation, Inc.,
// 51 Franklin St, Fifth Floor, Boston, MA 02110-1301  USA

using System;
using System.Data;
using MySql.Data.MySqlClient;

namespace MySql.VisualStudio.Tests.MySqlX.Base
{
  public abstract class BaseTests : IDisposable
  {
    #region ConstantNames

    /// <summary>
    /// SakilaX schema name.
    /// </summary>
    public const string SAKILA_X_SCHEMA_NAME = "sakila_x";

    /// <summary>
    /// SakilaX movies collection name.
    /// </summary>
    public const string SAKILA_X_MOVIES_COLLECTION = "movies";

    /// <summary>
    /// SakilaX users collection name.
    /// </summary>
    public const string SAKILA_X_USERS_COLLECTION = "users";

    /// <summary>
    /// Table test name
    /// </summary>
    public const string TEST_COLLECTION_NAME = "collection_test";

    /// <summary>
    /// Database test name
    /// </summary>
    public const string TEST_DATABASE_NAME = "database_test";

    /// <summary>
    /// Test schema name
    /// </summary>
    public const string TEST_SCHEMA_NAME = "schema_test";

    /// <summary>
    /// Table test name
    /// </summary>
    public const string TEST_TABLE_NAME = "table_test";

    #endregion ConstantNames

    #region CommonSqlQueries

    /// <summary>
    /// Sql statement to drop the test database
    /// </summary>
    protected const string DROP_TEST_DB_SQL_SYNTAX = "drop schema if exists " + TEST_SCHEMA_NAME + ";";

    /// <summary>
    /// Search for a table in the schema.tables information. Use: string.format(SEARCH_TABLE_SQL_SYNTAX, "myTable", "myDatabase")
    /// </summary>
    protected const string SEARCH_TABLE_SQL_SYNTAX = "select count(*) from information_schema.TABLES where table_name='{0}' and table_schema='{1}';";

    /// <summary>
    /// Sql statement to select the current databases in the server
    /// </summary>
    protected const string SHOW_DBS_SQL_SYNTAX = "show databases;";

    #endregion CommonSqlQueries

    #region AssertFailMessages

    /// <summary>
    /// Message to display when a database is not found. Usage: string.format(_dbNotFound, "myDatabase").
    /// </summary>
    protected const string DB_NOT_FOUND = "DB {0} not found";

    /// <summary>
    /// Message to display when a collection is not deleted. Usage: string.format(_tableNotDeleted, "myTable")
    /// </summary>
    protected const string COLLECTION_NOT_DELETED = "Collection {0} was not deleted";

    /// <summary>
    /// Message to display when a collection is not found. Usage: string.format(_tableNotFound, "myTable")
    /// </summary>
    protected const string COLLECTION_NOT_FOUND = "Collection {0} not found";

    /// <summary>
    /// Message to display when the data received doesn't match the data expected
    /// </summary>
    protected const string DATA_NOT_MATCH = "Data doesn't match";

    /// <summary>
    /// Message to display when an object is null. Usage: string.format(_nullObject, "myObject")
    /// </summary>
    protected const string NULL_OBJECT = "The object {0} is null";

    /// <summary>
    /// Message to display when a schema is not found. Usage: string.format(_dbNotFound, "myDatabase")
    /// </summary>
    protected const string SCHEMA_NOT_FOUND = "Schema {0} not found";

    /// <summary>
    /// Message to display when a table is not deleted. Usage: string.format(_tableNotDeleted, "myTable").
    /// </summary>
    protected const string TABLE_NOT_DELETED = "Table {0} was not deleted";

    /// <summary>
    /// Message to display when a table is not found. Usage: string.format(_tableNotFound, "myTable").
    /// </summary>
    protected const string TABLE_NOT_FOUND = "Table {0} not found";

    #endregion AssertFailMessages

    #region JavaScript specific

    /// <summary>
    /// Single line comment in hash format for a first line.
    /// </summary>
    protected const string JAVASCRIPT_COMMENT_SINGLE_LINE_1 = "// This is a test JavaScript comment in double-slash format...";

    /// <summary>
    /// Single line comment in hash format for a second line.
    /// </summary>
    protected const string JAVASCRIPT_COMMENT_SINGLE_LINE_2 = "//  which is continued in a second comment line.";

    /// <summary>
    /// Multi-line comment in triple double quote format for a first line.
    /// </summary>
    protected const string JAVASCRIPT_COMMENT_MULTI_LINE_1 = "/* This is a test JavaScript comment using the slash-star format;";

    /// <summary>
    /// Multi-line comment in triple double quote format for a second line.
    /// </summary>
    protected const string JAVASCRIPT_COMMENT_MULTI_LINE_2 = "  the intention is that this spans multiple lines...";

    /// <summary>
    /// Multi-line comment in triple double quote format for a third line.
    /// </summary>
    protected const string JAVASCRIPT_COMMENT_MULTI_LINE_3 = "  and we end it here at the third line. */";

    #endregion JavaScript specific

    #region Python specific

    /// <summary>
    /// Single line comment in hash format for a first line.
    /// </summary>
    protected const string PYTHON_COMMENT_SINGLE_LINE_1 = "# This is a test Python comment in hash format...";

    /// <summary>
    /// Single line comment in hash format for a second line.
    /// </summary>
    protected const string PYTHON_COMMENT_SINGLE_LINE_2 = "#  which is continued in a second comment line.";

    /// <summary>
    /// Multi-line comment in triple double quote format for a first line.
    /// </summary>
    protected const string PYTHON_COMMENT_MULTI_LINE_1 = "\"\"\" This is a test Python comment in triple double quote format;";

    /// <summary>
    /// Multi-line comment in triple double quote format for a second line.
    /// </summary>
    protected const string PYTHON_COMMENT_MULTI_LINE_2 = "  the intention is that this spans multiple lines...";

    /// <summary>
    /// Multi-line comment in triple double quote format for a third line.
    /// </summary>
    protected const string PYTHON_COMMENT_MULTI_LINE_3 = "  and we end it here at the third line. \"\"\"";

    #endregion Python specific

    #region Fields

    /// <summary>
    /// The command to set the value of the session variable to assign the node session of the X Protocol.
    /// </summary>
    protected string setSessionVar;

    #endregion Fields

    #region Properties

    /// <summary>
    /// Gets the <see cref="MySqlCommand"/> to execute statements.
    /// </summary>
    public MySqlCommand Command { get; protected set; }

    /// <summary>
    /// Gets the <see cref="MySqlConnection"/> to connect to a server.
    /// </summary>
    public MySqlConnection Connection { get; protected set; }

    /// <summary>
    /// Gets the object that stores and accesses the current configuration for the test database.
    /// </summary>
    public SetUpXShell SetUp { get; protected set; }

    /// <summary>
    /// Gets thee connection string format used by the X Protocol;
    /// </summary>
    public string XConnString { get; protected set; }

    #endregion Properties

    /// <summary>
    /// Dispose implementation of the current class
    /// </summary>
    public void Dispose()
    {
      SetUp.Dispose();
    }

    /// <summary>
    /// Setup information about the current schema test.
    /// </summary>
    /// <param name="data">Current test instance configuration.</param>
    public virtual void SetFixture(SetUpXShell data)
    {
      SetUp = data;
      Connection = new MySqlConnection(SetUp.GetConnectionString(SetUp.User, SetUp.Password, false, false));
      XConnString = string.Format("{0}:{1}@{2}:{3}", SetUp.User, SetUp.Password, SetUp.Host, SetUp.XPort);
    }

    /// <summary>
    /// Close a MySqlConnection when it is not closed.
    /// </summary>
    protected virtual void CloseConnection()
    {
      if (Connection.State != ConnectionState.Closed)
      {
        Connection.Close();
      }
    }

    /// <summary>
    /// Open a MySqlConnection when it is not opened.
    /// </summary>
    protected virtual void OpenConnection()
    {
      if (Connection.State != ConnectionState.Open)
      {
        Connection.Open();
      }
    }
  }
}
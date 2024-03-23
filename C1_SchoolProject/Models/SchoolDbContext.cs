using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MySql.Data.MySqlClient;

namespace C1_SchoolProject.Models
{
    //<summary>
    // Represents the database context for the school project.
    //</summary>
    public class SchoolDbContext
    {
        //<summary>
        // Gets the username for connecting to the database.
        //</summary>
        private static string User { get { return "root"; } }

        //<summary>
        // Gets the password for connecting to the database.
        //</summary>
        private static string Password { get { return "root"; } }

        //<summary>
        // Gets the database name.
        //</summary>
        private static string Database { get { return "school"; } }

        //<summary>
        // Gets the server address for connecting to the database.
        //</summary>
        private static string Server { get { return "localhost"; } }

        //<summary>
        // Gets the port number for connecting to the database.
        //</summary>
        private static string Port { get { return "3306"; } }

        //<summary>
        // Gets the connection string for accessing the database.
        //</summary>
        protected static string ConnectionString
        {
            get
            {
                return "server = " + Server
                    + "; user = " + User
                    + "; database = " + Database
                    + "; port = " + Port
                    + "; password = " + Password
                    + "; convert zero datetime = True";
            }
        }

        //<summary>
        // Creates and returns a new MySqlConnection object using the connection string.
        //</summary>
        //<returns>A MySqlConnection object.</returns>
        public MySqlConnection AccessDatabase()
        {
            return new MySqlConnection(ConnectionString);
        }
    }
}

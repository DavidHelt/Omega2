using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Omega;

namespace Omega
{
    
    public class Database
    {
        /// <summary>
        /// Holds the singleton instance of SqlConnection.
        /// </summary>
        private static SqlConnection conn = null;

        /// <summary>
        /// Private constructor to prevent instantiation.
        /// </summary>
        private Database()
        {

        }

        /// <summary>
        /// Gets the singleton instance of the SqlConnection. If the connection is not already established,
        /// it creates a new connection using the settings from the application's configuration file.
        /// </summary>
        /// <returns>A singleton instance of SqlConnection.</returns>
        public static SqlConnection GetInstance()
        {
            if (conn == null)
            {
                // Create a new SqlConnectionStringBuilder to build the connection string dynamically.
                SqlConnectionStringBuilder consStringBuilder = new SqlConnectionStringBuilder();
                consStringBuilder.DataSource = ReadSetting("DataSource");
                consStringBuilder.InitialCatalog = ReadSetting("InitialCatalog");
                consStringBuilder.IntegratedSecurity = bool.Parse(ReadSetting("IntergratedSecurity"));

                /* Add username and password if required   //UNCOMMENT ONLY IF YOU WANT TO USE USERNAME AND PASSWORD IN SQL AUTHENTICATION
                if (!consStringBuilder.IntegratedSecurity)
                {
                    consStringBuilder.UserID = ReadSetting("Username"); // Add username
                    consStringBuilder.Password = ReadSetting("Password"); // Add password
                }
                */
                conn = new SqlConnection(consStringBuilder.ConnectionString);
                conn.Open();
            }
            return conn;
        }

        /// <summary>
        /// Closes and disposes the current database connection if it exists.
        /// </summary>
        public static void CloseConnection()
        {
            try
            {
                if (conn != null)
                {
                    conn.Close();
                    conn.Dispose();
                }
            }
            catch { }
            finally
            {
                conn = null;
            }
        }

        /// <summary>
        /// Reads a setting value from the application's configuration file.
        /// </summary>
        /// <param name="key">The key of the setting to read.</param>
        /// <returns>The value of the specified setting, or "Not Found" if the setting does not exist.</returns>
        private static string ReadSetting(string key)
        {
            var appSettings = ConfigurationManager.AppSettings;
            string result = appSettings[key] ?? "Not Found";
            return result;
        }
    }
}
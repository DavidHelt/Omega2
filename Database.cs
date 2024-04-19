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
        private static SqlConnection conn = null;
        private Database()
        {

        }

        /// <summary>
        /// This method creates sql connection to database
        /// </summary>
        /// <returns></returns>
        public static SqlConnection GetInstance()
        {
            if (conn == null)
            {
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
        /// this method closes connection
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
        /// this method is just reading config file
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private static string ReadSetting(string key)
        {
            var appSettings = ConfigurationManager.AppSettings;
            string result = appSettings[key] ?? "Not Found";
            return result;
        }
    }
}
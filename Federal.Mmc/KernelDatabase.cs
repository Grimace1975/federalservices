using System.Data.SqlClient;
using System.Text;
using System.Data;
using System;
namespace Federal
{
    /// <summary>
    /// Database class
    /// </summary>
    public class KernelDatabase
    {
        private static string s_connectionText;

        /// <summary>
        /// Initializes the <see cref="Database"/> class.
        /// </summary>
        static KernelDatabase()
        {
			//string serverId = "tcp:degssql01.deg.local\\SQL2005";
			string serverId = "localhost\\sqlexpress";
			//string dbaseId = "NEUROX_sandbox";
            string userId = "";
            string password = "";
            int timeout = 30;
            var b = new StringBuilder();
            SqlConnectionStringBuilder.AppendKeyValuePair(b, "data source", serverId);
			//SqlConnectionStringBuilder.AppendKeyValuePair(b, "initial catalog", dbaseId);
            SqlConnectionStringBuilder.AppendKeyValuePair(b, "persist security info", "false");
            SqlConnectionStringBuilder.AppendKeyValuePair(b, "timeout", timeout.ToString());
            if ((string.IsNullOrEmpty(userId)) && ((string.IsNullOrEmpty(password))))
            {
                SqlConnectionStringBuilder.AppendKeyValuePair(b, "integrated security", "true");
            }
            else
            {
                SqlConnectionStringBuilder.AppendKeyValuePair(b, "user id", userId);
                SqlConnectionStringBuilder.AppendKeyValuePair(b, "password", password);
                SqlConnectionStringBuilder.AppendKeyValuePair(b, "integrated security", "false");
            }
            s_connectionText = b.ToString();
        }

        /// <summary>
        /// Creates the SQL connection.
        /// </summary>
        /// <returns></returns>
        public static SqlConnection CreateSqlConnection()
        {
            return new SqlConnection(s_connectionText);
        }

        /// <summary>
        /// Creates the data set.
        /// </summary>
        /// <param name="dataReader">The data reader.</param>
        /// <returns></returns>
        public static DataSet CreateDataSet(SqlDataReader dataReader)
        {
            var dataSet = new DataSet();
            var dataTableCollection = dataSet.Tables;
            while (!dataReader.IsClosed)
            {
				var table = new DataTable() { TableName = "Table" + dataTableCollection.Count };
                table.Load(dataReader);
                dataTableCollection.Add(table);
            }
            return dataSet;
        }

        /// <summary>
        /// Replaces the SQL value.
        /// </summary>
        /// <param name="sql">The SQL.</param>
        /// <param name="keyArray">The key array.</param>
        /// <param name="valueParameterArray">The value parameter array.</param>
        /// <returns></returns>
        public static string ReplaceSqlValue(string sql, string[] names, params object[] values)
        {
            for (int nameIndex = 0; nameIndex < names.Length; nameIndex++)
            {
                object value = values[nameIndex];
                string name = names[nameIndex];
                string id = "[:" + name.Substring(1) + ":]";
                //+
                switch (name[0])
                {
                    case 'i':
                        sql = sql.Replace(id, "[" + (string)value + "]");
                        break;
                    case 'c':
                        sql = sql.Replace(id, "N'" + ((string)value).Replace("'", "''") + "'");
                        break;
                    case 'n':
                        sql = sql.Replace(id, value.ToString());
                        break;
                    default:
                        throw new InvalidOperationException();
                }
            }
            return sql;
        }
    }
}
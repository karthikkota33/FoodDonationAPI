using System.Data;
using System.Data.SqlClient;

namespace FoodDonationAPI.Helpers
{
    public static class SQLHelpers
    {
        static IConfigurationRoot _configurationRoot;

        public static void Initialize(IConfigurationRoot configurationRoot)
        {
            _configurationRoot = configurationRoot;
        }

        /// <summary>
        /// To execute any DML statement using a command supplied as parameter.
        /// </summary>
        /// <param name="sqlCmd">SQLCommand with commandstring and paramters set up as needed </param>        
        /// <returns>Returns integer of number rows affected</returns>
        /// <remarks></remarks>
        public static int ExecuteInsertCommandAndReturnIdentity(string targetTableName, Dictionary<string, object> insertValues)
        {
            try
            {
                SqlCommand sqlCmd = SQLHelpers.GetInsertCommand(targetTableName, insertValues);
                return SQLHelpers.ExecuteDMLCommandAndReturnIdentity(sqlCmd);
            }
            catch (Exception ex)
            {
                throw new Exception("Error in ExecuteInsertCommandAndReturnIdentity. Details: " + ex.Message);
            }
        }

        public static SqlCommand GetInsertCommand(string tableName, Dictionary<string, object> columns)
        {
            try
            {
                var sqlCmd = new SqlCommand();

                string Q = "INSERT INTO " + tableName + " (";
                int n = columns.Count;
                for (int iCol = 0; iCol < n; iCol++)
                {
                    string columnName = columns.Keys.ElementAt<string>(iCol);
                    object value = columns[columnName];
                    if (value != null)
                    {
                        string paramName = "@" + columnName;
                        Q = Q + columnName + ", ";
                    }
                }

                //Remove last ', '
                Q = Q.Remove(Q.Length - 2, 2);

                Q = Q + ") VALUES (";

                for (int iCol = 0; iCol < n; iCol++)
                {
                    string columnName = columns.Keys.ElementAt<string>(iCol);
                    object value = columns[columnName];
                    if (value != null)
                    {
                        string paramName = "@" + columnName;
                        Q = Q + paramName + ", ";
                        sqlCmd.Parameters.Add(new SqlParameter("@" + columnName, value));
                    }
                }

                //Remove last ', '
                Q = Q.Remove(Q.Length - 2, 2);

                Q = Q + ")";

                sqlCmd.CommandText = Q;
                return sqlCmd;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in GetInsertCommand. Details: " + ex.Message);
            }
        }

        /// <summary>
        /// To execute any DML statement using a command supplied as parameter.
        /// </summary>
        /// <param name="sqlCmd">SQLCommand with commandstring and paramters set up as needed </param>        
        /// <returns>Returns identity (auto-number) value of newly inserted row</returns>
        /// <remarks></remarks>
        private static int ExecuteDMLCommandAndReturnIdentity(SqlCommand sqlCmd)
        {
            string strConn = _configurationRoot["ConnectionStrings:MainDB"];
            SqlConnection sqlCon = new SqlConnection(strConn);
            try
            {
                sqlCon.Open();
                if (sqlCmd.CommandText.ToUpper().Substring(0, 6) == "INSERT")
                {
                    sqlCmd.CommandText = sqlCmd.CommandText + " SELECT SCOPE_IDENTITY()";
                }
                sqlCmd.Connection = sqlCon;
                int id = Convert.ToInt32(sqlCmd.ExecuteScalar().ToString());
                return id;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in ExecuteDMLCommandAndReturnIdentit. Details: " + ex.Message);
            }
            finally
            {
                sqlCon.Close();
            }
        }

        public static DataTable ExecuteDataTable(string sql, SqlParameter[] p)
        {
            // The DataTable to be returned 
            //DataTable table;
            DataTable table = new DataTable();
            //SqlConnection cnn = new SqlConnection(strConn);
            //cnn.Open();
            //SqlConnection cnn = ConnOpen(strConn);
            string strConn = _configurationRoot["ConnectionStrings:MainDB"];
            using (SqlConnection cnn = ConnOpen(strConn))
            {
                //using (SqlCommand cmd = new SqlCommand(sql, cnn))
                //{
                SqlCommand cmd = new SqlCommand(sql, cnn);
                cmd.CommandTimeout = 0;
                cmd.CommandType = CommandType.StoredProcedure;
                if (p != null)
                {
                    for (int i = 0; i < p.Length; i++)
                    {
                        cmd.Parameters.Add(p[i]);
                    }
                }
                //SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                //table = new DataTable();
                //table.Load(reader);
                //reader.Close();
                //cnn.Close();
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(table);

                //}
            }
            return table;
        }

        public static DataSet ExecuteDataSet(string sql)
        {
            return ExecuteDataSet(sql, null);
        }

        public static DataSet ExecuteDataSet(string sql, SqlParameter[] p)
        {
            DataSet ds = new DataSet();
            //SqlConnection cnn = new SqlConnection(strConn);
            string strConn = _configurationRoot["ConnectionStrings:MainDB"];
            using (SqlConnection cnn = new SqlConnection(strConn))
            {
                //cnn.Open();
                //using (SqlCommand cmd = new SqlCommand(sql, cnn))
                //{
                SqlCommand cmd = new SqlCommand(sql, cnn);
                cmd.CommandTimeout = 0;
                cmd.CommandType = CommandType.StoredProcedure;
                if (p != null)
                {
                    for (int i = 0; i < p.Length; i++)
                    {
                        cmd.Parameters.Add(p[i]);
                    }
                }
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;

                da.Fill(ds);
                //cnn.Close();
                //cnn.Dispose();
            }
            //}
            return ds;
        }

        public static DataSet ExecuteDataSet(string sql, SqlParameter[] p, string sqlConn)
        {
            DataSet ds = new DataSet();
            //SqlConnection cnn = new SqlConnection(sqlConn);
            using (SqlConnection cnn = new SqlConnection(sqlConn))
            {
                //cnn.Open();
                //using (SqlCommand cmd = new SqlCommand(sql, cnn))
                //{
                SqlCommand cmd = new SqlCommand(sql, cnn);
                cmd.CommandTimeout = 0;
                cmd.CommandType = CommandType.StoredProcedure;
                if (p != null)
                {
                    for (int i = 0; i < p.Length; i++)
                    {
                        cmd.Parameters.Add(p[i]);
                    }
                }
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;

                da.Fill(ds);
                //cnn.Close();
                //cnn.Dispose();

                //}
            }
            return ds;
        }

        /// <summary>
        /// To execute a SQL Update Command
        /// </summary>
        ///<param name="targetTableName">Name of the database table in which to update values</param>  
        ///<param name="insertValues">Column names (as keys) and update values (as values) for query</param>
        ///<param name="whereConditions">Column names (as keys) and query values (as values) for where query. Pass null to omit and WHERE conditions</param>
        /// <returns>Returns integer of number rows affected</returns>
        /// <remarks></remarks>
        public static int ExecuteUpdateCommand(string targetTableName, Dictionary<string, object> updateValues, Dictionary<string, object> whereConditions)
        {
            try
            {
                SqlCommand sqlCmd = GetUpdateCommand(targetTableName, updateValues, whereConditions);
                return ExecuteDMLCommand(sqlCmd);
            }
            catch (Exception ex)
            {
                throw new Exception("Error in ExecuteUpdateCommand. Details: " + ex.Message);
            }
        }

        /// <summary>
        /// Builds SQL Command, without connection, for flat table UPDATE query with / without where condition and no JOINS
        /// </summary>
        /// <param name="tableName">Table name from which data to be returned</param>
        /// <param name="columns ">Dictionary containing Column names (as keys) and values (as values) to use for update</param>        
        /// <param name="whereColumns">Dictionary containing Column names (as keys) and values (as values) to use for parameterized WHERE part of the query</param>
        /// <returns>Query command without connection</returns>
        /// <remarks></remarks>
        public static SqlCommand GetUpdateCommand(string tableName, Dictionary<string, object> columns, Dictionary<string, object> whereColumns = null)
        {
            try
            {
                var sqlCmd = new SqlCommand();
                string Q = "UPDATE " + tableName + " SET ";

                if (columns != null && columns.Count > 0)
                {
                    for (int iCol = 0; iCol < columns.Count; iCol++)
                    {
                        string columnName = columns.Keys.ElementAt<string>(iCol);
                        object value = columns[columnName];
                        if (value != null)
                        {
                            string paramName = "@" + columnName;
                            Q = Q + columnName + " = " + paramName + ", ";
                            sqlCmd.Parameters.Add(new SqlParameter("@" + columnName, value));
                        }
                    }
                    Q = Q.Substring(0, Q.Length - 2);

                    if (whereColumns != null && whereColumns.Count > 0)
                    {

                        int count = 0;

                        Q = Q + " WHERE ";
                        for (int i = 0; i < whereColumns.Count; i++)
                        {
                            string columnName = whereColumns.Keys.ElementAt<string>(i);
                            object columnValue = whereColumns[columnName];
                            if (columnName.GetType().Name != "System.DateTime")
                            {
                                Q = Q + columnName + " = @" + columnName + " AND ";
                            }
                            else
                            {
                                Q = Q + "Convert(varchar(22)," + columnName + ",103) = @" + columnName + " AND ";
                            }

                            sqlCmd.Parameters.Add(new SqlParameter("@" + columnName, columnValue));

                            count = count + 1;
                        }


                        Q = Q.Substring(0, Q.Length - 4);

                    }

                }

                sqlCmd.CommandText = Q;
                return sqlCmd;

            }
            catch (Exception ex)
            {
                throw new Exception("Error in GetUpdateCommand. Details: " + ex.Message);
            }
        }

        /// <summary>
        /// To execute any DML statement using a command supplied as parameter.
        /// </summary>
        /// <param name="sqlCmd">SQLCommand with commandstring and paramters set up as needed </param>        
        /// <returns>Returns integer of number rows affected</returns>
        /// <remarks></remarks>
        private static int ExecuteDMLCommand(SqlCommand sqlCmd)
        {
            string strConn = _configurationRoot["ConnectionStrings:MainDB"];
            SqlConnection sqlCon = new SqlConnection(strConn);
            int intRowsEffected = 0;
            try
            {
                sqlCon.Open();
                sqlCmd.Connection = sqlCon;
                sqlCmd.CommandTimeout = 20000;
                intRowsEffected = sqlCmd.ExecuteNonQuery();
                sqlCmd.Dispose();
                return intRowsEffected;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in ExecuteDMLCommand. Details: " + ex.Message);
            }
            finally
            {
                sqlCon.Close();
            }
        }

        public static SqlConnection ConnOpen(string connString)
        {
            SqlConnection conn = new SqlConnection(connString);
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            return conn;
        }
    }
}

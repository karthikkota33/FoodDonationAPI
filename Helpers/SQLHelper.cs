using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Transactions;

namespace FoodDonationAPI.Helpers
{
    public class SQLHelper
    {
        private readonly IConfigurationRoot _configurationRoot;
        //Delegate to declare accepted databinding callback method
        public delegate object BorrowReader(IDataReader reader);


        public SQLHelper(IConfiguration configuration)
        {
            _configurationRoot = (IConfigurationRoot) configuration;
        }

        public  int ExecuteNonQuery(string sql)
        {
            return ExecuteNonQuery(sql, null);
        }

        public int ExecuteNonQuery(string sql, SqlParameter[] p)
        {
            //SqlTransaction sTran = null; SqlConnection cnn = new SqlConnection(strConn);
            int retval = 0;
            try
            {
                string strConn = _configurationRoot["ConnectionStrings:MainDB"];
                using (TransactionScope scope = new TransactionScope())
                {
                    using (SqlConnection cnn = new SqlConnection(strConn))
                    {
                        cnn.Open();

                        SqlCommand cmd = new SqlCommand(sql, cnn);

                        //sTran = cnn.BeginTransaction("trans");
                        //SqlCommand cmd = new SqlCommand(sql, cnn);

                        //cmd.Transaction = sTran;
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        if (p != null)
                        {

                            for (int i = 0; i < p.Length; i++)
                            {
                                cmd.Parameters.Add(p[i]);
                            }
                        }
                        retval = cmd.ExecuteNonQuery();
                        //sTran.Commit();
                        //cnn.Close();
                        //cnn.Dispose();
                    }
                    //}
                    scope.Complete();
                }
            }
            catch (Exception ex)
            {
                //if (sTran != null)
                //{
                //    sTran.Rollback();
                //}

                //logger.Error(ex);

            }

            //finally
            //{


            //    cnn.Close();
            //    cnn.Dispose();
            //}

            return retval;
        }


        public  int ExecuteNonQuery(string sql, SqlParameter[] p, bool CloseConnection)
        {
            SqlTransaction sTran = null;
            int retval = 0;
            try
            {
                string strConn = _configurationRoot["ConnectionStrings:MainDB"];
                using (TransactionScope scope = new TransactionScope())
                {

                    SqlConnection cnn = new SqlConnection(strConn);
                    cnn.Open();

                    using (SqlCommand cmd = new SqlCommand(sql, cnn))
                    {
                        sTran = cnn.BeginTransaction("trans");
                        cmd.Transaction = sTran;
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        if (p != null)
                        {
                            for (int i = 0; i < p.Length; i++)
                            {
                                cmd.Parameters.Add(p[i]);
                            }
                        }
                        retval = cmd.ExecuteNonQuery();
                        sTran.Commit();
                        cnn.Close(); cnn.Dispose(); scope.Complete();
                    }
                }
            }
            catch
            {
                if (sTran != null)
                {
                    sTran.Rollback();
                }

            }
            return retval;
        }



        public  int ExecuteNonQuery(string sql, SqlParameter[] p, string sqlConn)
        {
            //SqlTransaction sTran = null;
            int retval = 0;
            try
            {
                string? strConn = _configurationRoot["ConnectionStrings:MainDB"];
                using (TransactionScope scope = new TransactionScope())
                {

                    //SqlConnection cnn = new SqlConnection(sqlConn);
                    using (SqlConnection cnn = new SqlConnection(sqlConn))
                    {
                        cnn.Open();

                        SqlCommand cmd = new SqlCommand(sql, cnn);
                        //using (SqlCommand cmd = new SqlCommand(sql, cnn))
                        //{
                        //    sTran = cnn.BeginTransaction("trans");

                        //     SqlCommand cmd = new SqlCommand(sql, cnn);

                        //cmd.Transaction = sTran;
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        if (p != null)
                        {
                            for (int i = 0; i < p.Length; i++)
                            {
                                cmd.Parameters.Add(p[i]);
                            }
                        }
                        retval = cmd.ExecuteNonQuery();
                        //sTran.Commit();
                        //cnn.Close();
                        //cnn.Dispose();
                    }
                    scope.Complete();
                }
            }

            catch (Exception ex)
            {
                //if (sTran != null)
                //{
                //    sTran.Rollback();
                //}
                //logger.Error(ex);
            }


            return retval;
        }


        public  object ExecuteScalar(string sql)
        {
            return ExecuteScalar(sql, null);
        }

        public  object ExecuteScalar(string sql, string sqlConn, int temp)
        {
            object? retval = null;
            using (SqlConnection cnn = new SqlConnection(sqlConn))
            {
                cnn.Open();
                //using (SqlCommand cmd = new SqlCommand(sql, cnn))
                //{
                SqlCommand cmd = new SqlCommand(sql, cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;

                retval = cmd.ExecuteScalar();
                //}
                //cnn.Close();
            }

            return retval;
        }

        public  object ExecuteScalar(string sql, SqlParameter[]? p)
        {
            object? retval = null;
            string? strConn = _configurationRoot["ConnectionStrings:MainDB"];
            //using (TransactionScope scope = new TransactionScope())
            //{
            using (SqlConnection cnn = new SqlConnection(strConn))
            {
                cnn.Open();
                //using (SqlCommand cmd = new SqlCommand(sql, cnn))
                //{
                SqlCommand cmd = new SqlCommand(sql, cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                if (p != null)
                {
                    for (int i = 0; i < p.Length; i++)
                    {
                        cmd.Parameters.Add(p[i]);
                    }
                }
                retval = cmd.ExecuteScalar();
            }
            //cnn.Close(); scope.Complete();
            //}
            //}

            return retval;
        }

        public  object ExecuteScalar(string sql, SqlParameter[] p, string sqlConn)
        {
            object? retval = null;
            using (TransactionScope scope = new TransactionScope())
            {
                //SqlConnection cnn = new SqlConnection(sqlConn);
                using (SqlConnection cnn = new SqlConnection(sqlConn))
                {
                    cnn.Open();
                    //using (SqlCommand cmd = new SqlCommand(sql, cnn))
                    //{
                    SqlCommand cmd = new SqlCommand(sql, cnn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 0;
                    if (p != null)
                    {
                        for (int i = 0; i < p.Length; i++)
                        {
                            cmd.Parameters.Add(p[i]);
                        }
                    }
                    retval = cmd.ExecuteScalar();
                }
                //cnn.Close();
                //cnn.Dispose();
                scope.Complete();
            }

            return retval;

        }

        public  SqlDataReader ExecuteReader(string sql)
        {
            string? strConn = _configurationRoot["ConnectionStrings:MainDB"];
            SqlConnection cnn = new SqlConnection(strConn);

            cnn.Open();


            SqlCommand cmd = new SqlCommand(sql, cnn);


            cmd.CommandTimeout = 0;
            cmd.CommandType = CommandType.StoredProcedure;

            SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            return reader;
        }
        public  SqlDataReader ExecuteReader(string sql, string Conn)
        {

            SqlConnection cnn = new SqlConnection(Conn);


            cnn.Open();
            SqlCommand cmd = new SqlCommand(sql, cnn);
            cmd.CommandTimeout = 0;
            cmd.CommandType = CommandType.StoredProcedure;

            SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            return reader;
        }

        public  void ExecuteReader(string sql, string Conn, BorrowReader borrower)
        {

            SqlConnection cnn = new SqlConnection(Conn);


            cnn.Open();
            SqlCommand cmd = new SqlCommand(sql, cnn);
            cmd.CommandTimeout = 0;
            cmd.CommandType = CommandType.StoredProcedure;


            using (SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
            {
                borrower(rdr);

            }

        }
        //reader with parameters
        public  SqlDataReader ExecuteReader(string sql, SqlParameter[] p)
        {
            SqlConnection? cnn = null; SqlCommand? cmd = null;
            try
            {
                string? strConn = _configurationRoot["ConnectionStrings:MainDB"];
                //cnn = new SqlConnection(strConn);
                //cnn.Open();
                cnn = ConnOpen(strConn);
                cmd = new SqlCommand(sql, cnn);
                cmd.CommandTimeout = 0;
                cmd.CommandType = CommandType.StoredProcedure;
                if (p != null)
                {
                    for (int i = 0; i < p.Length; i++)
                    {
                        cmd.Parameters.Add(p[i]);
                    }
                }
                return cmd.ExecuteReader(CommandBehavior.CloseConnection);
            }
            finally
            {
                //cnn.Close();
               // cmd.Dispose();
            }

        }
        //reader with Delegate
        public  void ExecuteReader(string sql, SqlParameter[] p, BorrowReader borrower)
        {
            SqlConnection cnn = null; SqlCommand? cmd = null;
            try
            {
                string strConn = _configurationRoot["ConnectionStrings:MainDB"];
                cnn = new SqlConnection(strConn);
                cnn.Open();
                cmd = new SqlCommand(sql, cnn);
                cmd.CommandTimeout = 0;
                cmd.CommandType = CommandType.StoredProcedure;
                if (p != null)
                {
                    for (int i = 0; i < p.Length; i++)
                    {
                        cmd.Parameters.Add(p[i]);
                    }
                }
                using (SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    borrower(rdr);
                }


            }
            finally
            {
                // cnn.Close();
                //  cmd.Dispose();
            }

        }

        //delegate reader without params

        public  void ExecuteReader(string sql, BorrowReader borrower)
        {
            SqlConnection? cnn = null; SqlCommand? cmd = null;
            string? strConn = _configurationRoot["ConnectionStrings:MainDB"];
            cnn = new SqlConnection(strConn);
            cnn.Open();
            cmd = new SqlCommand(sql, cnn);
            cmd.CommandTimeout = 0;
            cmd.CommandType = CommandType.StoredProcedure;

            using (SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
            {
                borrower(rdr);

            }


        }

        public  void ExecuteReader(string sql, SqlParameter[] p, string sqlConn, BorrowReader borrower)
        {
            SqlConnection cnn = new SqlConnection(sqlConn);
            cnn.Open();
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
            using (SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
            {
                borrower(reader);
            }

        }

        public  SqlDataReader ExecuteReader(string sql, SqlParameter[] p, string sqlConn)
        {
            SqlConnection cnn = new SqlConnection(sqlConn);
            cnn.Open();
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
            SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            return reader;
        }

        public  DataSet ExecuteDataSet(string sql)
        {
            return ExecuteDataSet(sql, null);
        }

        public  DataSet ExecuteDataSet(string sql, SqlParameter[]? p)
        {
            DataSet ds = new DataSet();
            //SqlConnection cnn = new SqlConnection(strConn);
            string? strConn = _configurationRoot["ConnectionStrings:MainDB"];
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

        public  DataSet ExecuteDataSet(string sql, SqlParameter[] p, string sqlConn)
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

        public  SqlConnection ConnOpen(string? connString)
        {
            SqlConnection conn = new SqlConnection(connString);
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            return conn;
        }
        public  DataTable ExecuteDataTable(string sql, SqlParameter[] p)
        {
            // The DataTable to be returned 
            //DataTable table;
            DataTable table = new DataTable();
            //SqlConnection cnn = new SqlConnection(strConn);
            //cnn.Open();
            //SqlConnection cnn = ConnOpen(strConn);
            string? strConn = _configurationRoot["ConnectionStrings:MainDB"];
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

        public  DataTable ExecuteDataTable(string sql, SqlParameter[] p, string sqlConn)
        {
            // The DataTable to be returned 
            //DataTable table;
            //SqlConnection cnn = new SqlConnection(sqlConn);
            DataTable table = new DataTable();
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

        public  DataTable ExecuteDataTable(string sql)
        {
            // The DataTable to be returned 
            //DataTable table;
            DataTable table = new DataTable();
            string? strConn = _configurationRoot["ConnectionStrings:MainDB"];
            using (SqlConnection cnn = new SqlConnection(strConn))
            {
                //cnn.Open();
                SqlCommand cmd = new SqlCommand(sql, cnn);
                cmd.CommandTimeout = 0;
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(table);

                //SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                //table = new DataTable();
                //table.Load(reader);
                //reader.Close();
                //cnn.Close();
                //cmd.Connection.Close();
            }
            return table;
        }
        public  DataTable ExecuteDataTable(string sql, object sqlConn)
        {
            // The DataTable to be returned 
            //DataTable table;
            DataTable table = new DataTable();

            using (SqlConnection cnn = new SqlConnection(sqlConn.ToString()))
            {
                cnn.Open();
                SqlCommand cmd = new SqlCommand(sql, cnn);
                cmd.CommandTimeout = 0;
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(table);
                //SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                //table = new DataTable();
                //table.Load(reader);
                //reader.Close();
                //cnn.Close();
                //cmd.Connection.Close();
            }
            return table;
        }


        public  bool DBConnectionStatus(string conn)
        {
            SqlConnection? sqlConn = null;
            try
            {
                using (sqlConn = new SqlConnection(conn))
                {
                    sqlConn.Open();
                    return (sqlConn.State == ConnectionState.Open);


                }
            }
            catch (SqlException)
            {
                return false;
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                //sqlConn.Close();
                //sqlConn.Dispose();
            }
        }

        /// <summary>
        /// To execute any DML statement using a command supplied as parameter.
        /// </summary>
        /// <param name="sqlCmd">SQLCommand with commandstring and paramters set up as needed </param>        
        /// <returns>Returns integer of number rows affected</returns>
        /// <remarks></remarks>
        public int ExecuteInsertCommandAndReturnIdentity(string targetTableName, Dictionary<string, object> insertValues)
        {
            try
            {
                SqlCommand sqlCmd = GetInsertCommand(targetTableName, insertValues);
                return this.ExecuteDMLCommandAndReturnIdentity(sqlCmd);
            }
            catch (Exception ex)
            {
                throw new Exception("Error in ExecuteInsertCommandAndReturnIdentity. Details: " + ex.Message);
            }
        }

        public SqlCommand GetInsertCommand(string tableName, Dictionary<string, object> columns)
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
        private int ExecuteDMLCommandAndReturnIdentity(SqlCommand sqlCmd)
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
    }
}

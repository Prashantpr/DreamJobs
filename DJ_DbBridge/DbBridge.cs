using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.DbBridge
{
   public class SqlDbBridge
    {
       
        private SqlDataAdapter myAdapter;
        private SqlConnection conn;
        private SqlConnection Selectconn;
        private SqlConnection hrmsconn;
        /// <constructor>
        /// Initialise Connection
        /// </constructor>
        public SqlDbBridge()
        {
            myAdapter = new SqlDataAdapter();
            conn = new SqlConnection(ConfigurationManager.ConnectionStrings
                    ["MainConn"].ConnectionString);
            //Selectconn = new SqlConnection(ConfigurationManager.ConnectionStrings
            //     ["dbSelectConnectionString"].ConnectionString);
            //hrmsconn = new SqlConnection(ConfigurationManager.ConnectionStrings
            //    ["dbHRMSConnectionString"].ConnectionString);
        }

        /// <method>
        /// Open Database Connection if Closed or Broken
        /// </method>
        private void closeConnection()
        {
            if (conn.State == ConnectionState.Open)
                conn.Close();
            ////if (Selectconn.State == ConnectionState.Open)
            ////    Selectconn.Close();
            ////if (hrmsconn.State == ConnectionState.Open)
            ////    hrmsconn.Close();
        }
        private SqlConnection openConnection()
        {
            return openConnection(conn);
        }
        private SqlConnection openSelectConnection()
        {
            return openConnection(Selectconn);
        }

        private SqlConnection openHrmsConnection()
        {
            return openConnection(hrmsconn);
        }

        private SqlConnection openConnection(SqlConnection conn)
        {
            if (conn.State == ConnectionState.Closed || conn.State ==
                           ConnectionState.Broken)
            {
                lock (this)
                {
                    conn.Open();
                }
            }
            return conn;
        }


        /// <method>
        /// Select In Datatable
        /// </method>
        public DataTable ExecuteDataTable(String storedProcedureName, SqlParameter[] sqlParameter, string dbName = "")
        {
            SqlCommand myCommand = new SqlCommand();
            DataTable dataTable = new DataTable();
            dataTable = null;
            DataSet ds = new DataSet();
            try
            {
                myCommand.Connection = string.IsNullOrEmpty(dbName) ? openSelectConnection() : openHrmsConnection();
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.CommandText = storedProcedureName;
                myCommand.Parameters.AddRange(sqlParameter);
                //myCommand.ExecuteNonQuery();                
                myAdapter.SelectCommand = myCommand;
                myAdapter.Fill(ds);
                dataTable = ds.Tables[0];
                conn.Close();
            }
            catch (SqlException e)
            {
                Console.Write("Error - Connection.executeSelectQuery - Query: " + storedProcedureName + " \nException: " + e.StackTrace.ToString());
                return null;
            }
            finally
            {

            }
            return dataTable;
        }

        public DataTable InsertExecuteDataTable(String storedProcedureName, SqlParameter[] sqlParameter)
        {
            SqlCommand myCommand = new SqlCommand();
            DataTable dataTable = new DataTable();
            dataTable = null;
            DataSet ds = new DataSet();
            try
            {
                myCommand.Connection = openConnection();
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.CommandText = storedProcedureName;
                myCommand.Parameters.AddRange(sqlParameter);
                //myCommand.ExecuteNonQuery();                
                myAdapter.SelectCommand = myCommand;
                myAdapter.Fill(ds);
                dataTable = ds.Tables[0];
                conn.Close();
            }
            catch (SqlException e)
            {
                Console.Write("Error - Connection.executeSelectQuery - Query: " + storedProcedureName + " \nException: " + e.StackTrace.ToString());
                return null;
            }
            finally
            {

            }
            return dataTable;
        }

        public DataTable ExecuteDataTable(String storedProcedureName, SqlParameter[] sqlParameter,out int TotalRecord)
        {
            SqlCommand myCommand = new SqlCommand();
            DataTable dataTable = new DataTable();
            dataTable = null;
            DataSet ds = new DataSet();
            try
            {
                myCommand.Connection = openConnection();
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.CommandText = storedProcedureName;
                myCommand.Parameters.AddRange(sqlParameter);
                myCommand.Parameters["@RecordCount"].Direction = ParameterDirection.Output;
            //    myCommand.ExecuteNonQuery();
                myAdapter.SelectCommand = myCommand;
                myAdapter.Fill(ds);
                dataTable = ds.Tables[0];
                TotalRecord = Convert.ToInt32(myCommand.Parameters["@RecordCount"].Value.ToString());
                conn.Close();
            }
            catch (SqlException e)
            {
                TotalRecord = 0;
                Console.Write("Error - Connection.executeSelectQuery - Query: " + storedProcedureName + " \nException: " + e.StackTrace.ToString());
                return null;
            }
            finally
            {

            }
            return dataTable;
        }

        /// <method>
        /// Insert using ExecuteNonQuery
        /// </method>
        public int ExecuteNonQuery(String storedProcedureName, SqlParameter[] sqlParameter)
        {
            SqlCommand myCommand = new SqlCommand();
            try
            {
                myCommand.Connection = openConnection();
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.CommandText = storedProcedureName;
                myCommand.Parameters.AddRange(sqlParameter);
                return myCommand.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                Console.Write("Error - Connection.executeInsertQuery - Query: " + storedProcedureName + " \nException: \n" + e.StackTrace.ToString());
                return 0;
            }
            finally
            {
            }
            return 0;
        }

        /// <method>
        /// Insert using ExecuteScalar
        /// </method>
        public object ExecuteScalar(String storedProcedureName, SqlParameter[] sqlParameter)
        {
            SqlCommand myCommand = new SqlCommand();
            try
            {
                myCommand.Connection = openConnection();
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.CommandText = storedProcedureName;
                myCommand.Parameters.AddRange(sqlParameter);
                return myCommand.ExecuteScalar();
            }
            catch (SqlException e)
            {
                Console.Write("Error - Connection.executeInsertQuery - Query: " + storedProcedureName + " \nException: \n" + e.StackTrace.ToString());
                return 0;
            }
            finally
            {
            }
            return 0;
        }

        /// <method>
        /// Select in Dataset
        /// </method>
        public DataSet ExecuteDataSet(String storedProcedureName, SqlParameter[] sqlParameter)
        {
            SqlCommand myCommand = new SqlCommand();
           
            DataSet ds = new DataSet();
            try
            {
                myCommand.Connection = openConnection();
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.CommandText = storedProcedureName;
                myCommand.Parameters.AddRange(sqlParameter);
                myCommand.ExecuteNonQuery();
                myAdapter.SelectCommand = myCommand;
                myAdapter.Fill(ds);
                conn.Close();
                return ds;
            }
            catch (SqlException e)
            {
                Console.Write("Error - Connection.executeSelectQuery - Query: " + storedProcedureName + " \nException: " + e.StackTrace.ToString());
                return null;
            }
            finally
            {
             
            }
            return new DataSet();
        }

        public DataSet ExecuteDataSet(String storedProcedureName, List<SqlParameter> sqlParameter, string dbName = "")//Prashant
        {
            DataSet ds = new DataSet();
            try
            {
                using (SqlCommand myCommand = new SqlCommand())
                {
                    using (SqlDataAdapter myAdapter = new SqlDataAdapter())
                    {
                        myCommand.Connection = string.IsNullOrEmpty(dbName) ? conn : hrmsconn;
                        myCommand.CommandType = CommandType.StoredProcedure;
                        myCommand.CommandText = storedProcedureName;
                        foreach (SqlParameter param in sqlParameter)
                        {
                            myCommand.Parameters.Add(param);
                        }
                        myAdapter.SelectCommand = myCommand;
                        myAdapter.Fill(ds);
                    }
                }
            }
            catch (SqlException e)
            {
                Console.Write("Error - Connection.executeSelectQuery - Query: " + storedProcedureName + " \nException: " + e.StackTrace.ToString());
            }
            finally
            {
                closeConnection();
            }

            return ds;
        }

        public void BulkUpload(string SourceFileName, DataTable dt, string UploadedBy)//Prashant
        {
            try
            {
                using (conn = new SqlConnection(ConfigurationManager.ConnectionStrings["AtrifinCRMConn"].ConnectionString))
                {
                    conn.Open();
                    //Do a bulk copy for gaining better performance
                    using (SqlBulkCopy bulkCopy = new SqlBulkCopy(conn))
                    {
                        bulkCopy.ColumnMappings.Add("MobileNumber", "Phone_Number");
                        bulkCopy.ColumnMappings.Add("CustomerName", "CustomerName");
                        bulkCopy.ColumnMappings.Add("EmailID", "EmailID");

                        bulkCopy.BatchSize = 10000;
                        bulkCopy.BulkCopyTimeout = 1200;
                        bulkCopy.DestinationTableName = "ProspectCustomer";
                        bulkCopy.WriteToServer(dt.CreateDataReader());
                    }
                }
            }
            catch (SqlException e)
            {
                Console.Write("Error - Connection.executeSelectQuery - Query: Bulk Upload \nException: " + e.StackTrace.ToString());
            }
            finally
            {
                closeConnection();
            }

        }
    }
}

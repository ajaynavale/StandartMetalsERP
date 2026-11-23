using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace StandardMetals.DAL
{
    public class ConncetionClass
    {
        internal static string conn;
        internal SqlConnection cnUniversal;
        internal SqlCommand cmdUniversal;
        internal SqlDataAdapter daUniversal;

        public string ConnectionString
        {
            set { conn = value; }
        }

        public ConncetionClass()
        {
            // windows auth
            // conn = @"Data Source=DESKTOP-3BL9M8E;Initial Catalog=MVCAKash_Test;Integrated Security=True";
            //MVCAKash_Test_Test
            //conn = @"Data Source=DESKTOP-BQK6HVP\SQLEXPRESS ; Initial catalog=aaliml4n_MVC_ERP ; Integrated Security=true";
            conn = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;

            // Sql Server auth = third party server
            //conn = @"Data Source=208.91.198.174;Initial Catalog=aaliml4n_MVC_ERP;Integrated Security=False;uid=erpmvcuser;password=Mvcerp@123";
            // conn = @"Data Source=DESKTOP-3BL9M8E;Initial Catalog=SS!_MVC;Integrated Security=False;uid=Akash;password=Akash@8787";


        }

        public virtual void Initialize()
        {
            try
            {
                cnUniversal = new SqlConnection(conn);
                daUniversal = new SqlDataAdapter();
                cmdUniversal = new SqlCommand();
                cmdUniversal.CommandType = CommandType.StoredProcedure;
                cmdUniversal.Connection = cnUniversal;
            }

            catch (Exception e)
            {
                ErrorLog("dbBasis initialization", e);
            }
        }


        protected DataTable GetData(string strProcName)
        {
            this.Initialize();
            int nRecAffected = 0;
            DataTable dt = new DataTable();
            using (cnUniversal)
            {
                cmdUniversal.CommandText = strProcName;

                this.daUniversal.SelectCommand = cmdUniversal;
                this.cnUniversal.Open();
                nRecAffected = this.daUniversal.Fill(dt);
                this.Close();
                return dt;
            }
        }


        public DataTable GetData(string procedureName, SqlParameter[] sqlParam)
        {

            this.Initialize();
            int nRecAffected = 0;
            DataTable dt = new DataTable();
            using (cnUniversal)
            {
                cmdUniversal.CommandText = procedureName;

                this.daUniversal.SelectCommand = cmdUniversal;

                this.cmdUniversal.Parameters.AddRange(sqlParam);

                this.cnUniversal.Open();

                nRecAffected = this.daUniversal.Fill(dt);
                this.Close();
            }

            return dt;
        }

        public virtual void Close()
        {
            if (cnUniversal.State != System.Data.ConnectionState.Closed)
                cnUniversal.Close();
        }

        protected virtual void ErrorLog(string strMessage, Exception e)
        {
            Exception oException = new Exception(e.InnerException.Message + " \n\n Details:" + strMessage);
            throw oException;
        }

        //// Method to execute a stored procedure
        public DataSet ExecuteStoredProcedure(string storedProcedureName)
        {
            Initialize();
            DataSet ds = new DataSet();
            try
            {
                cmdUniversal.CommandText = storedProcedureName;
                daUniversal.SelectCommand = cmdUniversal;
                daUniversal.Fill(ds);
            }
            catch (Exception e)
            {
                ErrorLog("Executing Stored Procedure", e);
            }
            finally
            {
                Close();
            }
            return ds;
        }

        protected DataTable GetDataTable(string procName, params SqlParameter[] parameters)
        {
            using (SqlConnection con = new SqlConnection(conn))
            using (SqlCommand cmd = new SqlCommand(procName, con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                if (parameters != null) cmd.Parameters.AddRange(parameters);

                DataTable dt = new DataTable();
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    da.Fill(dt);
                }
                return dt;
            }
        }

        protected object ExecuteScalar(string procName, params SqlParameter[] parameters)
        {
            using (SqlConnection con = new SqlConnection(conn))
            using (SqlCommand cmd = new SqlCommand(procName, con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                if (parameters != null) cmd.Parameters.AddRange(parameters);

                con.Open();
                return cmd.ExecuteScalar();
            }
        }

        protected void BulkInsert(string tableName, DataTable dt, params string[][] mappings)
        {
            using (SqlConnection con = new SqlConnection(conn))
            using (SqlBulkCopy bulk = new SqlBulkCopy(con))
            {
                bulk.DestinationTableName = tableName;
                bulk.BulkCopyTimeout = 300;

                foreach (string[] m in mappings)
                    bulk.ColumnMappings.Add(m[0], m[1]);

                con.Open();
                bulk.WriteToServer(dt);
            }
        }

    }
}

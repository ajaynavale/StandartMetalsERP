using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StandardMetals.MODEL;
using System.Data;
using System.Configuration;
using StandardMetals.CommonLibrary;

namespace StandardMetals.DAL
{
    public class DefaultDashBoard_DAL
    {
        string connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;

        public DefaultModal GetSupplierWiseCount()
        {
            LogInfo.LogDetailsToFile("DefaultDashBoard_DAL- GetSupplierWiseCount Action- Start");
            DefaultModal model = new DefaultModal();
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("SPGetSuppbyItemId", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataTable dt = new DataTable();
            dt = ds.Tables[0];
            foreach (DataRow row in dt.Rows)
            {
                model.SupplierList.Add(new DefaultModal
                {
                    SupplierName = row["SuppName"] == DBNull.Value ? "" : row["SuppName"].ToString(),
                    ItemCount = row["ItemCount"] == DBNull.Value ? 0 : Convert.ToInt32(row["ItemCount"])
                });
                model.TotalSuppCount = row["SuppCount"] == DBNull.Value ? 0 : Convert.ToInt32(row["SuppCount"]);
                model.TotalItemCount = row["TotalItemCount"] == DBNull.Value ? 0 : Convert.ToInt32(row["TotalItemCount"]);
            }
            LogInfo.LogDetailsToFile("DefaultDashBoard_DAL- GetSupplierWiseCount Action- Start-End");
            return model;
        }

        public DefaultModal GetCustomerWiseCount()
        {
            LogInfo.LogDetailsToFile("DefaultDashBoard_DAL- GetCustomerWiseCount Action- Start");
            DefaultModal model = new DefaultModal();
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("SPGetCustbyItemId", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataTable dt = new DataTable();
            dt = ds.Tables[0];
            foreach (DataRow row in dt.Rows)
            {
                model.CustomerList.Add(new DefaultModal
                {
                    CustName = row["CustName"] == DBNull.Value ? "" : row["CustName"].ToString(),
                    ItemCount = row["ItemCount"] == DBNull.Value ? 0 : Convert.ToInt32(row["ItemCount"])
                });
                model.TotalCustCount = row["CustCount"] == DBNull.Value ? 0 : Convert.ToInt32(row["CustCount"]);
                model.TotalItemCount = row["TotalItemCount"] == DBNull.Value ? 0 : Convert.ToInt32(row["TotalItemCount"]);
            }
            LogInfo.LogDetailsToFile("DefaultDashBoard_DAL- GetCustomerWiseCount Action- Start-End");
            return model;
        }
    }
}
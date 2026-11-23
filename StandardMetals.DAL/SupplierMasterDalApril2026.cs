using StandardMetals.CommonLibrary;
using StandardMetals.MODEL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StandardMetals.DAL
{
    public class SupplierMasterDalApril2026
    {
        string str = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;

        public List<SupplierMasterModelApril2026> GetAllSupplier()
        {
            LogInfo.LogDetailsToFile("SupplierMasterDAL- GetAllSupplier Action- Start");

            SqlConnection con = new SqlConnection(str);
            SqlCommand cmd = new SqlCommand("GetAllS", con);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataTable dt = new DataTable();
            dt = ds.Tables[0];

            List<SupplierMasterModelApril2026> lst = new List<SupplierMasterModelApril2026>();

            foreach (DataRow item in dt.Rows)
            {
                lst.Add(new SupplierMasterModelApril2026()
                {
                    SupplierID = item["Id"] == DBNull.Value ? 0 : Convert.ToInt32(item["Id"]),
                    SupplierCode = item["SuppCode"] == DBNull.Value ? "" : item["SuppCode"].ToString(),
                    SupplierName = item["SuppName"] == DBNull.Value ? "" : item["SuppName"].ToString(),
                    ContactPerson = item["ContactPerson"] == DBNull.Value ? "" : item["ContactPerson"].ToString(),
                    Branch = item["BranchName"] == DBNull.Value ? "" : item["BranchName"].ToString(),
                    Address1 = item["Address1"] == DBNull.Value ? "" : item["Address1"].ToString(),
                    Address2 = item["Address2"] == DBNull.Value ? "" : item["Address2"].ToString(),
                    City = item["CityName"] == DBNull.Value ? "" : item["CityName"].ToString(),
                    State = item["StateName"] == DBNull.Value ? "" : item["StateName"].ToString(),
                    PinCode = item["PinCode"] == DBNull.Value ? 0 : Convert.ToDecimal(item["PinCode"]),
                    Country = item["CountryName"] == DBNull.Value ? "" : item["CountryName"].ToString(),
                    EmailID = item["EmailID"] == DBNull.Value ? "" : item["EmailID"].ToString(),
                    Telephone = item["Telephone"] == DBNull.Value ? 0 : Convert.ToDecimal(item["Telephone"]),
                    Mobile = item["Mobile"] == DBNull.Value ? 0 : Convert.ToDecimal(item["Mobile"]),
                    Fax = item["Fax"] == DBNull.Value ? "" : item["Fax"].ToString(),
                    Website = item["WebSite"] == DBNull.Value ? "" : item["WebSite"].ToString(),
                    GSTIN = item["GSTIN"] == DBNull.Value ? "" : item["GSTIN"].ToString(),
                    Remarks = item["Remarks"] == DBNull.Value ? "" : item["Remarks"].ToString(),
                    RawMaterial = item["Material"] == DBNull.Value ? false : Convert.ToBoolean(item["Material"])
                });
            }
            LogInfo.LogDetailsToFile("SupplierMasterDAL- GetAllSupplier Action- Start-End");

            return lst;
        }

        public SupplierMasterModelApril2026 GetByIdSupplier(int id)
        {
            LogInfo.LogDetailsToFile("SupplierMasterDAL- GetById Action- Start");

            SqlConnection con = new SqlConnection(str);
            SqlCommand cmd = new SqlCommand("GetSupplierDetailsByIdApr", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Id", id);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataTable dt = new DataTable();
            dt = ds.Tables[0];

            SupplierMasterModelApril2026 model = new SupplierMasterModelApril2026();

            DataRow item = dt.Rows[0];

            model.SupplierCode = item["SuppCode"] == DBNull.Value ? "" : item["SuppCode"].ToString();
            model.SupplierName = item["SuppName"] == DBNull.Value ? "" : item["SuppName"].ToString();
            model.ContactPerson = item["ContactPerson"] == DBNull.Value ? "" : item["ContactPerson"].ToString();
            model.BranchId = item["BranchId"] == DBNull.Value ? 0 : Convert.ToInt32(item["BranchId"]);
            model.Address1 = item["Address1"] == DBNull.Value ? "" : item["Address1"].ToString();
            model.Address2 = item["Address2"] == DBNull.Value ? "" : item["Address2"].ToString();
            model.CityId = item["CityId"] == DBNull.Value ? 0 : Convert.ToInt32(item["CityId"]);
            model.StateId = item["StateId"] == DBNull.Value ? 0 : Convert.ToInt32(item["StateId"]);
            model.PinCode = item["PinCode"] == DBNull.Value ? 0 : Convert.ToDecimal(item["PinCode"]);
            model.CountryId = item["CountryId"] == DBNull.Value ? 0 : Convert.ToInt32(item["CountryId"]);
            model.EmailID = item["EmailID"] == DBNull.Value ? "" : item["EmailID"].ToString();
            model.Telephone = item["Telephone"] == DBNull.Value ? 0 : Convert.ToDecimal(item["Telephone"]);
            model.Mobile = item["Mobile"] == DBNull.Value ? 0 : Convert.ToDecimal(item["Mobile"]);
            model.Fax = item["Fax"] == DBNull.Value ? "" : item["Fax"].ToString();
            model.Website = item["WebSite"] == DBNull.Value ? "" : item["WebSite"].ToString();
            model.GSTIN = item["GSTIN"] == DBNull.Value ? "" : item["GSTIN"].ToString();
            model.Remarks = item["Remarks"] == DBNull.Value ? "" : item["Remarks"].ToString();
            model.RawMaterial = item["Material"] == DBNull.Value ? false : Convert.ToBoolean(item["Material"]);

            LogInfo.LogDetailsToFile("SupplierMasterDAL- GetById Action- Start-End");

            return model;
        }

        public string InsertSupplier(SupplierMasterModelApril2026 model)
        {
            SqlConnection con = new SqlConnection(str);
            SqlCommand cmd = new SqlCommand("InsertIntoSupplierMaster", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@SuppName", model.SupplierName);
            cmd.Parameters.AddWithValue("@ContactPerson", model.ContactPerson);
            cmd.Parameters.AddWithValue("@BranchId", model.BranchId);
            cmd.Parameters.AddWithValue("@Address1", model.Address1);
            cmd.Parameters.AddWithValue("@Address2", model.Address2);
            cmd.Parameters.AddWithValue("@CityId", model.CityId);
            cmd.Parameters.AddWithValue("@StateId", model.StateId);
            cmd.Parameters.AddWithValue("@PinCode", model.PinCode);
            cmd.Parameters.AddWithValue("@CountryId", model.CountryId);
            cmd.Parameters.AddWithValue("@EmailID", model.EmailID);
            cmd.Parameters.AddWithValue("@Telephone", model.Telephone);
            cmd.Parameters.AddWithValue("@Mobile", model.Mobile);
            cmd.Parameters.AddWithValue("@Fax", model.Fax);
            cmd.Parameters.AddWithValue("@WebSite", model.Website);
            cmd.Parameters.AddWithValue("@GSTIN", model.GSTIN);
            cmd.Parameters.AddWithValue("@Remarks", model.Remarks);
            cmd.Parameters.AddWithValue("@Material", model.RawMaterial);

            con.Open();
            string result = cmd.ExecuteScalar().ToString();
            con.Close();

            return result;
        }

        public int UpdateSupplier(int id, SupplierMasterModelApril2026 model)
        {
            SqlConnection con = new SqlConnection(str);
            SqlCommand cmd = new SqlCommand("UpdateSupplierMasterApr", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Id", id);
            cmd.Parameters.AddWithValue("@SuppName", model.SupplierName);
            cmd.Parameters.AddWithValue("@ContactPerson", model.ContactPerson);
            cmd.Parameters.AddWithValue("@BranchId", model.BranchId);
            cmd.Parameters.AddWithValue("@Address1", model.Address1);
            cmd.Parameters.AddWithValue("@Address2", model.Address2);
            cmd.Parameters.AddWithValue("@CityId", model.CityId);
            cmd.Parameters.AddWithValue("@StateId", model.StateId);
            cmd.Parameters.AddWithValue("@PinCode", model.PinCode);
            cmd.Parameters.AddWithValue("@CountryId", model.CountryId);
            cmd.Parameters.AddWithValue("@EmailID", model.EmailID);
            cmd.Parameters.AddWithValue("@Telephone", model.Telephone);
            cmd.Parameters.AddWithValue("@Mobile", model.Mobile);
            cmd.Parameters.AddWithValue("@Fax", model.Fax);
            cmd.Parameters.AddWithValue("@WebSite", model.Website);
            cmd.Parameters.AddWithValue("@GSTIN", model.GSTIN);
            cmd.Parameters.AddWithValue("@Remarks", model.Remarks);
            cmd.Parameters.AddWithValue("@Material", model.RawMaterial);

            con.Open();
            int result = Convert.ToInt32(cmd.ExecuteScalar());
            con.Close();

            return result;
        }
    }
}
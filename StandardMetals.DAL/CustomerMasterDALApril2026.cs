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
    //public sealed class CustomerMaster
    //{

    //    private static ItemMaster obj;

    //    private ItemMaster()
    //    {
    //        LogInfo.LogDetailsToFile("Enter into singlton constructor");
    //    }

    //    public static ItemMaster GetInstance()
    //    {
    //        if (obj == null)
    //        {
    //            obj = new ItemMaster();
    //        }
    //        return obj;
    //    }
    //}

    public class CustomerMasterDALApril2026:ConncetionClass
    {
        string str = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;

        // Customer DropDown

        public List<CustomerMasterModelApril2026> GetAllCustomer()
        {
            LogInfo.LogDetailsToFile("CustomerMasterDAL- GetAllCustomer Action- Start");

            SqlConnection con = new SqlConnection(str);

            SqlCommand cmd = new SqlCommand("BindCustomer", con);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataSet ds = new DataSet();
            da.Fill(ds);

            DataTable dt = new DataTable();
            dt = ds.Tables[0];

            List<CustomerMasterModelApril2026> list = new List<CustomerMasterModelApril2026>();

            foreach (DataRow item in dt.Rows)
            {
                list.Add(new CustomerMasterModelApril2026()
                {
                    Id = item["Id"] == DBNull.Value ? 0 : Convert.ToInt32(item["Id"]),
                    CustName = item["CustName"] == DBNull.Value ? "" : item["CustName"].ToString(),
                    CustCode = item["CustCode"] == DBNull.Value ? "" : item["CustCode"].ToString(),
                    ContactPerson = item["ContactPerson"] == DBNull.Value ? "" : item["ContactPerson"].ToString(),
                    Address1 = item["Address1"] == DBNull.Value ? "" : item["Address1"].ToString(),
                    Address2 = item["Address2"] == DBNull.Value ? "" : item["Address2"].ToString(),
                    PinCode = item["PinCode"] == DBNull.Value ? "" : item["PinCode"].ToString(),
                    EmailId = item["EmailId"] == DBNull.Value ? "" : item["EmailId"].ToString(),
                    Telephone = item["Telephone"] == DBNull.Value ? "" : item["Telephone"].ToString(),
                    Mobile = item["Mobile"] == DBNull.Value ? "" : item["Mobile"].ToString(),
                    Fax = item["Fax"] == DBNull.Value ? "" : item["Fax"].ToString(),
                    WebSite = item["WebSite"] == DBNull.Value ? "" : item["WebSite"].ToString(),
                    GSTIN = item["GSTIN"] == DBNull.Value ? "" : item["GSTIN"].ToString(),
                    Remarks = item["Remarks"] == DBNull.Value ? "" : item["Remarks"].ToString(),
                    City = item["CityName"] == DBNull.Value ? "" : item["CityName"].ToString(),
                    StateName = item["StateName"] == DBNull.Value ? "" : item["StateName"].ToString(),
                    Country = item["CountryName"] == DBNull.Value ? "" : item["CountryName"].ToString(),
                    BranchName = item["BranchName"] == DBNull.Value ? "" : item["BranchName"].ToString(),
                });
            }
            LogInfo.LogDetailsToFile("CustomerMasterDAL- GetAllCustomer Action- Start-Success");

            return list;
        }

        // Branch DropDown

        public List<CustomerMasterModelApril2026> GetBranch()
        {
            LogInfo.LogDetailsToFile("CustomerMasterDAL- GetBranch Action- Start");

            SqlConnection con = new SqlConnection(str);

            SqlCommand cmd = new SqlCommand("BindBranchMaster", con);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataSet ds = new DataSet();
            da.Fill(ds);

            DataTable dt = new DataTable();
            dt = ds.Tables[0];

            List<CustomerMasterModelApril2026> list = new List<CustomerMasterModelApril2026>();

            foreach (DataRow item in dt.Rows)
            {
                CustomerMasterModelApril2026 model = new CustomerMasterModelApril2026();

                model.BranchId = item["BranchId"] == DBNull.Value ? 0 : Convert.ToInt32(item["BranchId"]);
                model.BranchName = item["BranchName"] == DBNull.Value ? "" : item["BranchName"].ToString();

                list.Add(model);
            }
            LogInfo.LogDetailsToFile("CustomerMasterDAL- GetBrand Action- Start-End");

            return list;
        }

        // State DropDown

        public List<CustomerMasterModelApril2026> GetStates()
        {
            LogInfo.LogDetailsToFile("CustomerMasterDAL- GetSataes Action- Start");

            SqlConnection con = new SqlConnection(str);

            SqlCommand cmd = new SqlCommand("spstateall", con);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataSet ds = new DataSet();
            da.Fill(ds);

            DataTable dt = new DataTable();
            dt = ds.Tables[0];

            List<CustomerMasterModelApril2026> list = new List<CustomerMasterModelApril2026>();

            foreach (DataRow item in dt.Rows)
            {
                CustomerMasterModelApril2026 model = new CustomerMasterModelApril2026();

                model.StateId = item["StateId"] == DBNull.Value ? 0 : Convert.ToInt32(item["StateId"]);
                model.StateName = item["StateName"] == DBNull.Value ? "" : item["StateName"].ToString();

                list.Add(model);
            }
            LogInfo.LogDetailsToFile("CustomerMasterDAL- GetStates Action- Start-End");

            return list;
        }

        // Countries DropDown

        public List<CustomerMasterModelApril2026> GetCountries()
        {
            LogInfo.LogDetailsToFile("CustomerMasterDAL- GetCountries Action- Start");

            SqlConnection con = new SqlConnection(str);

            SqlCommand cmd = new SqlCommand("GetAllCountriesSM", con);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataSet ds = new DataSet();
            da.Fill(ds);

            DataTable dt = new DataTable();
            dt = ds.Tables[0];

            List<CustomerMasterModelApril2026> list = new List<CustomerMasterModelApril2026>();

            foreach (DataRow item in dt.Rows)
            {
                CustomerMasterModelApril2026 model = new CustomerMasterModelApril2026();

                model.CountryId = item["CountryId"] == DBNull.Value ? 0 : Convert.ToInt32(item["CountryId"]);
                model.Country = item["CountryName"] == DBNull.Value ? "" : item["CountryName"].ToString();

                list.Add(model);
            }
            LogInfo.LogDetailsToFile("CustomerMasterDAL- GetCountries Action- Start-End");

            return list;
        }

        // Cities DropDown

        public List<CustomerMasterModelApril2026> GetCities()
        {
            LogInfo.LogDetailsToFile("CustomerMasterDAL- GetCities Action- Start");

            SqlConnection con = new SqlConnection(str);

            SqlCommand cmd = new SqlCommand("GetAllCitySM", con);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataSet ds = new DataSet();
            da.Fill(ds);

            DataTable dt = new DataTable();
            dt = ds.Tables[0];

            List<CustomerMasterModelApril2026> list = new List<CustomerMasterModelApril2026>();

            foreach (DataRow item in dt.Rows)
            {
                CustomerMasterModelApril2026 model = new CustomerMasterModelApril2026();

                model.CityId = item["CityId"] == DBNull.Value ? 0 : Convert.ToInt32(item["CityId"]);
                model.City = item["CityName"] == DBNull.Value ? "" : item["CityName"].ToString();

                list.Add(model);
            }
            LogInfo.LogDetailsToFile("CustomerMasterDAL- GetCities Action- Start-End");

            return list;
        }

        // Inserting Records into DB

        public int SaveData(int id, CustomerMasterModelApril2026 model)
        {
            LogInfo.LogDetailsToFile("CustomerMasterDAL- Save Action- Start");

            SqlConnection con = new SqlConnection(str);

            SqlCommand cmd = new SqlCommand("SaveCustomerMaster", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@CustCode", model.CustCode);
            cmd.Parameters.AddWithValue("@CustName", model.CustName);
            cmd.Parameters.AddWithValue("@ContactPerson", model.ContactPerson);
            cmd.Parameters.AddWithValue("@BranchId", model.BranchId);
            cmd.Parameters.AddWithValue("@Address1", model.Address1);
            cmd.Parameters.AddWithValue("@Address2", model.Address2);
            cmd.Parameters.AddWithValue("@CityId", model.CityId);
            cmd.Parameters.AddWithValue("@StateId", model.StateId);
            cmd.Parameters.AddWithValue("@PinCode", model.PinCode);
            cmd.Parameters.AddWithValue("@CountryId", model.CountryId);
            cmd.Parameters.AddWithValue("@EmailId", model.EmailId);
            cmd.Parameters.AddWithValue("@Telephone", model.Telephone);
            cmd.Parameters.AddWithValue("@Mobile", model.Mobile);
            cmd.Parameters.AddWithValue("@Fax", model.Fax);
            cmd.Parameters.AddWithValue("@Website", model.WebSite);
            cmd.Parameters.AddWithValue("@GSTIN", model.GSTIN);
            cmd.Parameters.AddWithValue("@Remarks", model.Remarks);

            con.Open();

            int result = cmd.ExecuteNonQuery();

            con.Close();
            LogInfo.LogDetailsToFile("CustomerMasterDAL- Save Action- Start-End");

            return result;
        }

        // Updating Existing Record In DB

        public int Update(int id, CustomerMasterModelApril2026 model)
        {
            LogInfo.LogDetailsToFile("CustomerMasterDAL- Update Action- Start");

            SqlConnection con = new SqlConnection(str);

            SqlCommand cmd = new SqlCommand("UpdateCustomerMasterfeb", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@ID", id);
            cmd.Parameters.AddWithValue("@CustName", model.CustName);
            cmd.Parameters.AddWithValue("@ContactPerson", model.ContactPerson);
            cmd.Parameters.AddWithValue("@BranchId", model.BranchId);
            cmd.Parameters.AddWithValue("@Address1", model.Address1);
            cmd.Parameters.AddWithValue("@Address2", model.Address2);
            cmd.Parameters.AddWithValue("@CityId", model.CityId);
            cmd.Parameters.AddWithValue("@StateId", model.StateId);
            cmd.Parameters.AddWithValue("@PinCode", model.PinCode);
            cmd.Parameters.AddWithValue("@CountryId", model.CountryId);
            cmd.Parameters.AddWithValue("@EmailId", model.EmailId);
            cmd.Parameters.AddWithValue("@Telephone", model.Telephone);
            cmd.Parameters.AddWithValue("@Mobile", model.Mobile);
            cmd.Parameters.AddWithValue("@Fax", model.Fax);
            cmd.Parameters.AddWithValue("@Website", model.WebSite);
            cmd.Parameters.AddWithValue("@GSTIN", model.GSTIN);
            cmd.Parameters.AddWithValue("@Remarks", model.Remarks);

            con.Open();

            int result = cmd.ExecuteNonQuery();

            con.Close();
            LogInfo.LogDetailsToFile("CustomerMasterDAL- Update Action- Start-End");

            return result;
        }

        // Finding records Using Id (Primary key)

        public CustomerMasterModelApril2026 GetById(int id)
        {
            LogInfo.LogDetailsToFile("CustomerMasterDAL- GetById Action- Start");

            SqlConnection con = new SqlConnection(str);

            SqlCommand cmd = new SqlCommand("CustGetId", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Id", id);

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataSet ds = new DataSet();
            da.Fill(ds);

            DataTable dt = new DataTable();
            dt = ds.Tables[0];

            CustomerMasterModelApril2026 model = new CustomerMasterModelApril2026();

            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];

                model.Id = row["Id"] == DBNull.Value ? 0 : (int.TryParse(row["Id"].ToString(), out int Id) ? Id : 0);
                model.CustCode = row["CustCode"] == DBNull.Value ? "" : row["CustCode"].ToString();
                model.CustName = row["CustName"] == DBNull.Value ? "" : row["CustName"].ToString();
                model.ContactPerson = row["ContactPerson"] == DBNull.Value ? "" : row["ContactPerson"].ToString();

                model.Address1 = row["Address1"] == DBNull.Value ? "" : row["Address1"].ToString();
                model.Address2 = row["Address2"] == DBNull.Value ? "" : row["Address2"].ToString();

                model.City = row["City"] == DBNull.Value ? "" : row["City"].ToString();
                model.CityId = row["CityId"] == DBNull.Value ? 0 : (int.TryParse(row["CityId"].ToString(), out int cityId) ? cityId : 0);

                model.PinCode = row["PinCode"] == DBNull.Value ? "" : row["PinCode"].ToString();

                model.Country = row["Country"] == DBNull.Value ? "" : row["Country"].ToString();
                model.CountryId = row["CountryId"] == DBNull.Value ? 0 : (int.TryParse(row["CountryId"].ToString(), out int countryId) ? countryId : 0);

                model.BranchId = row["BranchId"] == DBNull.Value ? 0 : (int.TryParse(row["BranchId"].ToString(), out int branchId) ? branchId : 0);
                model.StateId = row["StateId"] == DBNull.Value ? 0 : (int.TryParse(row["StateId"].ToString(), out int stateId) ? stateId : 0);

                model.EmailId = row["EmailId"] == DBNull.Value ? "" : row["EmailId"].ToString();
                model.Telephone = row["Telephone"] == DBNull.Value ? "" : row["Telephone"].ToString();
                model.Mobile = row["Mobile"] == DBNull.Value ? "" : row["Mobile"].ToString();
                model.Fax = row["Fax"] == DBNull.Value ? "" : row["Fax"].ToString();
                model.WebSite = row["WebSite"] == DBNull.Value ? "" : row["WebSite"].ToString();
                model.GSTIN = row["GSTIN"] == DBNull.Value ? "" : row["GSTIN"].ToString();
                model.Remarks = row["Remarks"] == DBNull.Value ? "" : row["Remarks"].ToString();
            }

            LogInfo.LogDetailsToFile("CustomerMasterDAL- GetById Action- Start-End");

            return model;
        }

        // Inserting Records into DB (Friend's version - returns string via ExecuteScalar)

        public string InsertCustomerMaster(CustomerMasterModelApril2026 model)
        {
            LogInfo.LogDetailsToFile("CustomerMasterDAL- Save Action- Start");

            SqlConnection con = new SqlConnection(str);

            SqlCommand cmd = new SqlCommand("SaveCustomerMaster", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@CustName", model.CustName);
            cmd.Parameters.AddWithValue("@ContactPerson", model.ContactPerson);
            cmd.Parameters.AddWithValue("@BranchId", model.BranchId);
            cmd.Parameters.AddWithValue("@Address1", model.Address1);
            cmd.Parameters.AddWithValue("@Address2", model.Address2);
            cmd.Parameters.AddWithValue("@CityId", model.CityId);
            cmd.Parameters.AddWithValue("@StateId", model.StateId);
            cmd.Parameters.AddWithValue("@PinCode", model.PinCode);
            cmd.Parameters.AddWithValue("@CountryId", model.CountryId);
            cmd.Parameters.AddWithValue("@EmailId", model.EmailId);
            cmd.Parameters.AddWithValue("@Telephone", model.Telephone);
            cmd.Parameters.AddWithValue("@Mobile", model.Mobile);
            cmd.Parameters.AddWithValue("@Fax", model.Fax);
            cmd.Parameters.AddWithValue("@Website", model.WebSite);
            cmd.Parameters.AddWithValue("@GSTIN", model.GSTIN);
            cmd.Parameters.AddWithValue("@Remarks", model.Remarks);

            con.Open();

            string result = cmd.ExecuteScalar().ToString();

            con.Close();
            LogInfo.LogDetailsToFile("CustomerMasterDAL- Save Action- Start-End");

            return result;
        }

        // Updating Existing Record In DB (Friend's version)

        public int UpdateCustomerMaster(int id, CustomerMasterModelApril2026 model)
        {
            LogInfo.LogDetailsToFile("CustomerMasterDAL- Update Action- Start");

            SqlConnection con = new SqlConnection(str);

            SqlCommand cmd = new SqlCommand("UpdateCustomerMasterfeb", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@ID", id);
            cmd.Parameters.AddWithValue("@CustName", model.CustName);
            cmd.Parameters.AddWithValue("@ContactPerson", model.ContactPerson);
            cmd.Parameters.AddWithValue("@BranchId", model.BranchId);
            cmd.Parameters.AddWithValue("@Address1", model.Address1);
            cmd.Parameters.AddWithValue("@Address2", model.Address2);
            cmd.Parameters.AddWithValue("@CityId", model.CityId);
            cmd.Parameters.AddWithValue("@StateId", model.StateId);
            cmd.Parameters.AddWithValue("@PinCode", model.PinCode);
            cmd.Parameters.AddWithValue("@CountryId", model.CountryId);
            cmd.Parameters.AddWithValue("@EmailId", model.EmailId);
            cmd.Parameters.AddWithValue("@Telephone", model.Telephone);
            cmd.Parameters.AddWithValue("@Mobile", model.Mobile);
            cmd.Parameters.AddWithValue("@Fax", model.Fax);
            cmd.Parameters.AddWithValue("@Website", model.WebSite);
            cmd.Parameters.AddWithValue("@GSTIN", model.GSTIN);
            cmd.Parameters.AddWithValue("@Remarks", model.Remarks);

            con.Open();

            int result = cmd.ExecuteNonQuery();

            con.Close();
            LogInfo.LogDetailsToFile("CustomerMasterDAL- Update Action- Start-End");

            return result;
        }

        // Finding records Using Id (Primary key) (Friend's version)

        public CustomerMasterModelApril2026 CustomerDetailsGetById(int id)
        {
            LogInfo.LogDetailsToFile("CustomerMasterDAL- GetById Action- Start");

            SqlConnection con = new SqlConnection(str);

            SqlCommand cmd = new SqlCommand("CustGetId", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Id", id);

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataSet ds = new DataSet();
            da.Fill(ds);

            DataTable dt = new DataTable();
            dt = ds.Tables[0];

            CustomerMasterModelApril2026 model = new CustomerMasterModelApril2026();

            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];

                model.Id = row["Id"] == DBNull.Value ? 0 : Convert.ToInt32(row["Id"]);
                model.CustCode = row["CustCode"] == DBNull.Value ? "" : row["CustCode"].ToString();
                model.CustName = row["CustName"] == DBNull.Value ? "" : row["CustName"].ToString();
                model.ContactPerson = row["ContactPerson"] == DBNull.Value ? "" : row["ContactPerson"].ToString();

                model.BranchId = row["BranchId"] == DBNull.Value ? 0 : Convert.ToInt32(row["BranchId"]);
                model.Address1 = row["Address1"] == DBNull.Value ? "" : row["Address1"].ToString();
                model.Address2 = row["Address2"] == DBNull.Value ? "" : row["Address2"].ToString();

                model.CityId = row["CityId"] == DBNull.Value ? 0 : Convert.ToInt32(row["CityId"]);
                model.City = row["City"] == DBNull.Value ? "" : row["City"].ToString();

                model.StateId = row["StateId"] == DBNull.Value ? 0 : Convert.ToInt32(row["StateId"]);

                model.PinCode = row["PinCode"] == DBNull.Value ? "" : row["PinCode"].ToString();

                model.CountryId = row["CountryId"] == DBNull.Value ? 0 : Convert.ToInt32(row["CountryId"]);
                model.Country = row["Country"] == DBNull.Value ? "" : row["Country"].ToString();

                model.EmailId = row["EmailId"] == DBNull.Value ? "" : row["EmailId"].ToString();
                model.Telephone = row["Telephone"] == DBNull.Value ? "" : row["Telephone"].ToString();
                model.Mobile = row["Mobile"] == DBNull.Value ? "" : row["Mobile"].ToString();
                model.Fax = row["Fax"] == DBNull.Value ? "" : row["Fax"].ToString();
                model.WebSite = row["WebSite"] == DBNull.Value ? "" : row["WebSite"].ToString();
                model.GSTIN = row["GSTIN"] == DBNull.Value ? "" : row["GSTIN"].ToString();
                model.Remarks = row["Remarks"] == DBNull.Value ? "" : row["Remarks"].ToString();
            }

            LogInfo.LogDetailsToFile("CustomerMasterDAL- GetById Action- Start-End");

            return model;
        }

        public List<CustomerMasterModelApril2026> GetCitiesFromMaster()
        {
            SqlConnection con = new SqlConnection(str);
            SqlCommand cmd = new SqlCommand("SELECT CityId, CityName FROM CityMaster", con);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);

            List<CustomerMasterModelApril2026> list = new List<CustomerMasterModelApril2026>();
            foreach (DataRow item in ds.Tables[0].Rows)
            {
                CustomerMasterModelApril2026 model = new CustomerMasterModelApril2026();
                model.CityId = item["CityId"] == DBNull.Value ? 0 : Convert.ToInt32(item["CityId"]);
                model.City = item["CityName"] == DBNull.Value ? "" : item["CityName"].ToString();
                list.Add(model);
            }
            return list;
        }
    }
}
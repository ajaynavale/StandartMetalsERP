using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StandardMetals.CommonLibrary;
using StandardMetals.MODEL;


namespace StandardMetals.DAL
{
    public sealed class ItemMaster
    {

        private static ItemMaster obj;

        private ItemMaster()
        {
            LogInfo.LogDetailsToFile("Enter into singlton constructor");
        }

        public static ItemMaster GetInstance()
        {
            if(obj==null)
            {
                obj = new ItemMaster();
            }
            return obj;
        }
    }

    public class ItemMasterDALApril2026
    {
        string connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;

        // Get All Item Grid
        public List<ItemMasterModalApril2026> GetAllItemList()
        {
            List<ItemMasterModalApril2026> list = new List<ItemMasterModalApril2026>();

            LogInfo.LogDetailsToFile("ItemMasterDAL- GetAllItemMasterDal Action- Start");

            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand("GetAllItem", conn);
            command.CommandType = CommandType.StoredProcedure;

            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataSet set = new DataSet();
            adapter.Fill(set);

            DataTable table = new DataTable();
            table = set.Tables[0];



            foreach (DataRow row in table.Rows)
            {
                list.Add(new ItemMasterModalApril2026()
                {
                    Id = row["ID"] == DBNull.Value ? 0 : Convert.ToInt32(row["ID"]),
                    ItemCode = row["ItemCode"] == DBNull.Value ? "" : row["ItemCode"].ToString(),
                    ItemName = row["ItemName"] == DBNull.Value ? "" : row["ItemName"].ToString(),
                    Material = row["Material"] == DBNull.Value ? "" : row["Material"].ToString(),
                    HSNCODE = row["HSNCODE"] == DBNull.Value ? "" : row["HSNCODE"].ToString(),

                    GSTRate = row["GSTRate"] == DBNull.Value ? 0 : Convert.ToDouble(row["GSTRate"]),
                    PurchaseCost = row["PurchaseCost"] == DBNull.Value ? 0 : Convert.ToDouble(row["PurchaseCost"]),
                    SellingPrice = row["SellingPrice"] == DBNull.Value ? 0 : Convert.ToDouble(row["SellingPrice"]),

                    CategoryId = row["CategoryId"] == DBNull.Value ? 0 : Convert.ToInt32(row["CategoryId"]),
                    CategoryName = row["CategoryName"] == DBNull.Value ? "" : row["CategoryName"].ToString(),

                    SubCategoryId = row["SubCategoryId"] == DBNull.Value ? 0 : Convert.ToInt32(row["SubCategoryId"]),
                    SubCategoryName = row["SubCategoryName"] == DBNull.Value ? "" : row["SubCategoryName"].ToString(),

                    ManufactureId = row["ManufacturerId"] == DBNull.Value ? 0 : Convert.ToInt32(row["ManufacturerId"]),
                    ManufactureName = row["ManufacturerName"] == DBNull.Value ? "" : row["ManufacturerName"].ToString(),

                    ColourId = row["ColourId"] == DBNull.Value ? 0 : Convert.ToInt32(row["ColourId"]),
                    ColourName = row["ColourName"] == DBNull.Value ? "" : row["ColourName"].ToString(),

                    UnitId = row["UnitId"] == DBNull.Value ? 0 : Convert.ToInt32(row["UnitId"]),
                    UnitName = row["UnitName"] == DBNull.Value ? "" : row["UnitName"].ToString()
                });
            }

            LogInfo.LogDetailsToFile("ItemMasterDAL- GetAllItemMasterDal Action- Start-Success");

            return list;

        }

        // Save ItemMasterWithItemVsSupp Mapping Data
        public string InsertItemMaster(ItemMasterModalApril2026 model)
        {
            LogInfo.LogDetailsToFile("ItemMasterDAL- Save Action- Start");

            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand("Insert_ItemWithSuppiler", conn);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@ItemName", model.ItemName);
            command.Parameters.AddWithValue("@ManufacturerId", model.ManufactureId);
            command.Parameters.AddWithValue("@Material", model.Material);
            command.Parameters.AddWithValue("@CategoryId", model.CategoryId);
            command.Parameters.AddWithValue("@SubCategoryId", model.SubCategoryId);
            command.Parameters.AddWithValue("@ColourId", model.ColourId);
            command.Parameters.AddWithValue("@UnitId", model.UnitId);
            command.Parameters.AddWithValue("@HSNCode", model.HSNCODE);
            command.Parameters.AddWithValue("@GSTRate", model.GSTRate);
            command.Parameters.AddWithValue("@PurchaseCost", model.PurchaseCost);
            command.Parameters.AddWithValue("@SellingPrice", model.SellingPrice);

            DataTable SupplierTable = new DataTable();

            SupplierTable.Columns.Add("SupplierID", typeof(int));

            if (model.SupplierList.Count > 0)
            {
                foreach (var supplier in model.SupplierList)
                {
                    SupplierTable.Rows.Add(supplier.SupplierID);
                }
            }

            SqlParameter SuppParamerter = command.Parameters.AddWithValue("@SupplierList", SupplierTable);
            SuppParamerter.SqlDbType = SqlDbType.Structured;
            SuppParamerter.TypeName = "dbo.SupplierTable";

            DataTable CustomerTable = new DataTable();
            CustomerTable.Columns.Add("CustomerId", typeof(int));

            if (model.CustList.Count > 0)
            {
                foreach (var customer in model.CustList)
                {
                    CustomerTable.Rows.Add(customer.CustomerId);
                }

            }

            SqlParameter CustParameters = command.Parameters.AddWithValue("@CustomerList", CustomerTable);
            CustParameters.SqlDbType = SqlDbType.Structured;
            CustParameters.TypeName = "dbo.CustomerTable";

            conn.Open();

            string result = command.ExecuteScalar().ToString();

            conn.Close();
            LogInfo.LogDetailsToFile("ItemMasterDAL- Save Action- Start-Success");

            return result;
        }

        // Get ItemMaster And ItemVsSupplier Mapping By Id
        public ItemMasterModalApril2026 GetItemById(int itemId)
        {
            LogInfo.LogDetailsToFile("ItemMasterDAL- GetById Action- Start");

            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand("GetByItemIdDetails", conn);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@ItemId", itemId);

            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataSet ds = new DataSet();
            adapter.Fill(ds);

            DataTable dt = new DataTable();
            DataTable SupplierT = new DataTable();
            DataTable CustomerT = new DataTable();
            dt = ds.Tables[0];
            SupplierT = ds.Tables[1];
            CustomerT = ds.Tables[2];

            ItemMasterModalApril2026 model = new ItemMasterModalApril2026();

            model.Id = itemId;
            model.ItemCode = dt.Rows[0]["ItemCode"] == DBNull.Value ? "" : dt.Rows[0]["ItemCode"].ToString();
            model.ItemName = dt.Rows[0]["ItemName"] == DBNull.Value ? "" : dt.Rows[0]["ItemName"].ToString();
            model.Material = dt.Rows[0]["Material"] == DBNull.Value ? "" : dt.Rows[0]["Material"].ToString();
            model.GSTRate = dt.Rows[0]["GSTRate"] == DBNull.Value ? 0 : Convert.ToDouble(dt.Rows[0]["GSTRate"]);
            model.PurchaseCost = dt.Rows[0]["PurchaseCost"] == DBNull.Value ? 0 : Convert.ToDouble(dt.Rows[0]["PurchaseCost"]);
            model.SellingPrice = dt.Rows[0]["SellingPrice"] == DBNull.Value ? 0 : Convert.ToDouble(dt.Rows[0]["SellingPrice"]);
            model.CategoryId = dt.Rows[0]["CategoryId"] == DBNull.Value ? 0 : Convert.ToInt32(dt.Rows[0]["CategoryId"]);
            model.SubCategoryId = dt.Rows[0]["SubCategoryId"] == DBNull.Value ? 0 : Convert.ToInt32(dt.Rows[0]["SubCategoryId"]);
            model.ManufactureId = dt.Rows[0]["ManufacturerId"] == DBNull.Value ? 0 : Convert.ToInt32(dt.Rows[0]["ManufacturerId"]);
            model.ColourId = dt.Rows[0]["ColourId"] == DBNull.Value ? 0 : Convert.ToInt32(dt.Rows[0]["ColourId"]);
            model.UnitId = dt.Rows[0]["UnitId"] == DBNull.Value ? 0 : Convert.ToInt32(dt.Rows[0]["UnitId"]);
            model.HSNCODE = dt.Rows[0]["HSNCODE"] == DBNull.Value ? "" : dt.Rows[0]["HSNCODE"].ToString();

            // ItemMasterVsSupplierMaster Mapping

            foreach (DataRow row in SupplierT.Rows)
            {
                model.SupplierList.Add(new SupplierModelIM()
                {
                    SupplierID = row["SupplierId"] == DBNull.Value ? 0 : Convert.ToInt32(row["SupplierId"]),
                    SupplierCode = row["SuppCode"] == DBNull.Value ? "" : row["SuppCode"].ToString(),
                    SupplierName = row["SuppName"] == DBNull.Value ? "" : row["SuppName"].ToString()
                });
            }

            foreach (DataRow row in CustomerT.Rows)
            {
                model.CustList.Add(new CustomerModelIM()
                {
                    CustomerId = row["CustId"] == DBNull.Value ? 0 : Convert.ToInt32(row["CustId"]),
                    CustomerCode = row["CustCode"] == DBNull.Value ? "" : row["CustCode"].ToString(),
                    CustomerName = row["CustName"] == DBNull.Value ? "" : row["CustName"].ToString()
                });
            }

            LogInfo.LogDetailsToFile("ItemMasterDAL- GetById Action- Start-Success");

            return model;
        }

        // Update ItemMaster Details
        public int UpdateItemMaster(int itemId, ItemMasterModalApril2026 model)
        {
            LogInfo.LogDetailsToFile("ItemMasterDAL- Update Action- Start");

            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand("UpdateItemWithItemVsSuppM", conn);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@ID", itemId);
            command.Parameters.AddWithValue("@ItemCode", model.ItemCode);
            command.Parameters.AddWithValue("@ItemName", model.ItemName);
            command.Parameters.AddWithValue("@ManufacturerId", model.ManufactureId);
            command.Parameters.AddWithValue("@Material", model.Material);
            command.Parameters.AddWithValue("@CategoryId", model.CategoryId);
            command.Parameters.AddWithValue("@SubCategoryId", model.SubCategoryId);
            command.Parameters.AddWithValue("@ColourId", model.ColourId);
            command.Parameters.AddWithValue("@UnitId", model.UnitId);
            command.Parameters.AddWithValue("@HSNCODE", model.HSNCODE);
            command.Parameters.AddWithValue("@GSTRate", model.GSTRate);
            command.Parameters.AddWithValue("@PurchaseCost", model.PurchaseCost);
            command.Parameters.AddWithValue("@SellingPrice", model.SellingPrice);

            DataTable SupplierTable = new DataTable();

            SupplierTable.Columns.Add("SupplierID", typeof(int));

            if (model.SupplierList.Count > 0)
            {
                foreach (var supplier in model.SupplierList)
                {
                    SupplierTable.Rows.Add(supplier.SupplierID);
                }
            }

            SqlParameter SuppParamerter = command.Parameters.AddWithValue("@SupplierList", SupplierTable);
            SuppParamerter.SqlDbType = SqlDbType.Structured;
            SuppParamerter.TypeName = "dbo.SupplierTable";

            DataTable CustomerTable = new DataTable();
            CustomerTable.Columns.Add("CustomerId", typeof(int));

            if (model.CustList.Count > 0)
            {
                foreach (var customer in model.CustList)
                {
                    CustomerTable.Rows.Add(customer.CustomerId);
                }
            }

            SqlParameter CustParameter = command.Parameters.AddWithValue("@CustomerList", CustomerTable);
            CustParameter.SqlDbType = SqlDbType.Structured;
            CustParameter.TypeName = "dbo.CustomerTable";

            conn.Open();
            int result = Convert.ToInt32(command.ExecuteScalar());
            conn.Close();
            LogInfo.LogDetailsToFile("ItemMasterDAL- Update Action- Start-Success");

            return result;
        }

        // Getting Data For DropDownList

        public List<ItemMasterModalApril2026> GetColours()
        {
            List<ItemMasterModalApril2026> list = new List<ItemMasterModalApril2026>();

            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand("BindColorMaster", conn);
            command.CommandType = CommandType.StoredProcedure;

            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataSet set = new DataSet();
            adapter.Fill(set);

            DataTable table = new DataTable();
            table = set.Tables[0];

            foreach (DataRow row in table.Rows)
            {
                list.Add(new ItemMasterModalApril2026()
                {
                    ColourId = Convert.ToInt32(row["ColourId"]),
                    ColourName = row["ColourName"].ToString(),
                });
            }

            return list;
        }

        // GetManufacture Drp
        public List<ItemMasterModalApril2026> GetManufacturer()
        {
            List<ItemMasterModalApril2026> list = new List<ItemMasterModalApril2026>();

            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand("BindManufacturerMaster", conn);
            command.CommandType = CommandType.StoredProcedure;

            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataSet set = new DataSet();
            adapter.Fill(set);

            DataTable table = new DataTable();
            table = set.Tables[0];

            foreach (DataRow row in table.Rows)
            {
                list.Add(new ItemMasterModalApril2026()
                {
                    ManufactureId = Convert.ToInt32(row["ManufacturerId"]),
                    ManufactureName = row["ManufacturerName"].ToString(),
                });
            }

            return list;
        }

        // Categories Drp
        public List<ItemMasterModalApril2026> GetCategories()
        {
            List<ItemMasterModalApril2026> list = new List<ItemMasterModalApril2026>();

            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand("BindCategory", conn);
            command.CommandType = CommandType.StoredProcedure;

            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataSet set = new DataSet();
            adapter.Fill(set);

            DataTable table = new DataTable();
            table = set.Tables[0];

            foreach (DataRow row in table.Rows)
            {
                list.Add(new ItemMasterModalApril2026()
                {
                    // CategoryId = Convert.ToInt32(row["CategoryID"]),
                    CategoryId = Convert.ToInt32(row["CategoryId"]),
                    CategoryName = row["CategoryName"].ToString(),
                });
            }

            return list;
        }

        // Get SubCategory Drp
        public List<ItemMasterModalApril2026> GetSubCategories(int categoryId)
        {
            List<ItemMasterModalApril2026> list = new List<ItemMasterModalApril2026>();

            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand("BindSubCategory", conn);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@CategoryId", categoryId);

            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataSet set = new DataSet();
            adapter.Fill(set);

            DataTable table = new DataTable();
            table = set.Tables[0];

            foreach (DataRow row in table.Rows)
            {
                list.Add(new ItemMasterModalApril2026()
                {
                    SubCategoryId = Convert.ToInt32(row["SubCategoryId"]),
                    SubCategoryName = row["SubCategoryName"].ToString(),
                });
            }

            return list;
        }

        // Get Unit Drp
        public List<ItemMasterModalApril2026> GetUnit()
        {
            List<ItemMasterModalApril2026> list = new List<ItemMasterModalApril2026>();

            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand("BindUnit", conn);
            command.CommandType = CommandType.StoredProcedure;

            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataSet set = new DataSet();
            adapter.Fill(set);

            DataTable table = new DataTable();
            table = set.Tables[0];

            foreach (DataRow row in table.Rows)
            {
                list.Add(new ItemMasterModalApril2026()
                {
                    UnitId = Convert.ToInt32(row["UnitId"]),
                    UnitName = row["UnitName"].ToString(),
                });
            }

            return list;
        }

        ///////// For JQUERY Project

        // Get all the available ItemCodes from the database
        public List<ItemMasterModalApril2026> getItemCodes()
        {
            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand("GetItemCode_Oct25", conn);
            command.CommandType = CommandType.StoredProcedure;

            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataSet set = new DataSet();
            adapter.Fill(set);

            DataTable table = new DataTable();
            table = set.Tables[0];

            List<ItemMasterModalApril2026> list = new List<ItemMasterModalApril2026>();

            foreach (DataRow row in table.Rows)
            {
                list.Add(new ItemMasterModalApril2026()
                {
                    Id = Convert.ToInt32(row["ID"]),
                    ItemCode = row["ItemCode"].ToString(),
                });
            }
            return list;
        }

        // Get all the available ItemNames from the database
        public List<ItemMasterModalApril2026> getItemNames()
        {
            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand("GetItemName_Oct25", conn);
            command.CommandType = CommandType.StoredProcedure;

            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataSet set = new DataSet();
            adapter.Fill(set);

            DataTable table = new DataTable();
            table = set.Tables[0];

            List<ItemMasterModalApril2026> list = new List<ItemMasterModalApril2026>();

            foreach (DataRow row in table.Rows)
            {
                list.Add(new ItemMasterModalApril2026()
                {
                    Id = Convert.ToInt32(row["ID"]),
                    ItemName = row["ItemName"].ToString(),
                });
            }
            return list;
        }

        // Save New Item for JQuery Item Master form

        public string SaveItemJW(ItemMasterModalApril2026 model)
        {
                SqlConnection conn = new SqlConnection(connectionString);
                SqlCommand command = new SqlCommand("SPCreateItemJW", conn);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@ItemName", model.ItemName);
                command.Parameters.AddWithValue("@ManufacturerId", model.ManufactureId);
                command.Parameters.AddWithValue("@Material", model.Material);
                command.Parameters.AddWithValue("@CategoryId", model.CategoryId);
                command.Parameters.AddWithValue("@SubCategoryId", model.SubCategoryId);
                command.Parameters.AddWithValue("@ColourId", model.ColourId);
                command.Parameters.AddWithValue("@UnitId", model.UnitId);
                command.Parameters.AddWithValue("@HSNCode", model.HSNCODE);
                command.Parameters.AddWithValue("@GSTRate", model.GSTRate);
                command.Parameters.AddWithValue("@PurchaseCost", model.PurchaseCost);
                command.Parameters.AddWithValue("@SellingPrice", model.SellingPrice);

                conn.Open();

                object result = command.ExecuteScalar();

                if (result == null)
                {
                    return "";
                }

                return result.ToString();
            
        }

        // Update Item for JQuery Item Master form

        public string UpdateItemJW(int id, ItemMasterModalApril2026 model)
        {
                SqlConnection conn = new SqlConnection(connectionString);
                SqlCommand command = new SqlCommand("SPUpdateItemJW", conn);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@Id", id);
                command.Parameters.AddWithValue("@ItemName", model.ItemName);
                command.Parameters.AddWithValue("@ManufacturerId", model.ManufactureId);
                command.Parameters.AddWithValue("@Material", model.Material);
                command.Parameters.AddWithValue("@CategoryId", model.CategoryId);
                command.Parameters.AddWithValue("@SubCategoryId", model.SubCategoryId);
                command.Parameters.AddWithValue("@ColourId", model.ColourId);
                command.Parameters.AddWithValue("@UnitId", model.UnitId);
                command.Parameters.AddWithValue("@HSNCode", model.HSNCODE);
                command.Parameters.AddWithValue("@GSTRate", model.GSTRate);
                command.Parameters.AddWithValue("@PurchaseCost", model.PurchaseCost);
                command.Parameters.AddWithValue("@SellingPrice", model.SellingPrice);

                conn.Open();

                object result = command.ExecuteScalar();

                if (result == null)
                {
                    return "";
                }

                return result.ToString();

        }
    }
}

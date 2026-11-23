using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using StandardMetals.MODEL;
using StandardMetals.CommonLibrary;

namespace StandardMetals.DAL
{
    public class BulckUploadDALApril2026
    {
        // GET CONNECTION FROM WEB.CONFIG
        private SqlConnection GetConnection()
        {
            return new SqlConnection(ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString);
        }

        // =====================================================
        // COMMON METHODS
        // =====================================================

        public List<BulkUploadeModel> GetTableList()
        {
            LogInfo.LogDetailsToFile("ItemMasterDAL- GetDummyTableList Action- Start");

            using (SqlConnection con = GetConnection())
            using (SqlCommand cmd = new SqlCommand("GetDummyTableList", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    DataSet ds = new DataSet();
                    da.Fill(ds);

                    DataTable dt = new DataTable();
                    if (ds.Tables.Count > 0)
                    {
                        dt = ds.Tables[0];
                    }

                    List<BulkUploadeModel> list = new List<BulkUploadeModel>();

                    foreach (DataRow item in dt.Rows)
                    {
                        list.Add(new BulkUploadeModel()
                        {
                            tableName = item["ValueField"] == DBNull.Value ? "" : item["ValueField"].ToString(),
                            displayName = item["DisplayField"] == DBNull.Value ? "" : item["DisplayField"].ToString()
                        });
                    }

                    LogInfo.LogDetailsToFile("ItemMasterDAL- GetDummyTableList Action- End");
                    return list;
                }
            }
        }

        public int InsertUploadMaster(string fileName, string tableName)
        {
            using (SqlConnection con = GetConnection())
            using (SqlCommand cmd = new SqlCommand("UpdateMasterInsertIMAug25", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@FileNames", SqlDbType.VarChar, 200).Value = fileName;
                cmd.Parameters.Add("@UploadType", SqlDbType.VarChar, 50).Value = tableName;

                con.Open();
                object result = cmd.ExecuteScalar();

                if (result == null || result == DBNull.Value)
                    return 0;

                return Convert.ToInt32(result);
            }
        }

        // =====================================================
        // ITEM MASTER
        // =====================================================

        public void BulkInsertItemMaster(DataTable excelData, int uploadId)
        {
            if (excelData == null) throw new ArgumentNullException("excelData");

            DataTable stagingData = new DataTable();
            stagingData.Columns.Add("ItemCode", typeof(string));
            stagingData.Columns.Add("ItemName", typeof(string));
            stagingData.Columns.Add("Material", typeof(string));
            stagingData.Columns.Add("HSNCODE", typeof(string));
            stagingData.Columns.Add("GSTRate", typeof(string));
            stagingData.Columns.Add("PurchaseCost", typeof(string));
            stagingData.Columns.Add("SellingPrice", typeof(string));
            stagingData.Columns.Add("ManufacturerName", typeof(string));
            stagingData.Columns.Add("ColourName", typeof(string));
            stagingData.Columns.Add("UnitName", typeof(string));
            stagingData.Columns.Add("CategoryName", typeof(string));
            stagingData.Columns.Add("SubCategoryName", typeof(string));
            stagingData.Columns.Add("UploadId", typeof(int));

            foreach (DataRow excelRow in excelData.Rows)
            {
                DataRow newRow = stagingData.NewRow();
                newRow["ItemCode"] = GetExcelValue(excelRow, "Item Code", "ItemCode");
                newRow["ItemName"] = GetExcelValue(excelRow, "Item Name", "ItemName");
                newRow["Material"] = GetExcelValue(excelRow, "Material");
                newRow["HSNCODE"] = GetExcelValue(excelRow, "HSN-Code", "HSNCODE");
                newRow["GSTRate"] = GetExcelValue(excelRow, "GST Rate", "GSTRate");
                newRow["PurchaseCost"] = GetExcelValue(excelRow, "Purchase Cost", "PurchaseCost");
                newRow["SellingPrice"] = GetExcelValue(excelRow, "Selling Price", "SellingPrice");
                newRow["ManufacturerName"] = GetExcelValue(excelRow, "Manufacture", "ManufacturerName", "Manufacturer");
                newRow["ColourName"] = GetExcelValue(excelRow, "Colour", "ColourName");
                newRow["UnitName"] = GetExcelValue(excelRow, "Unit", "UnitName");
                newRow["CategoryName"] = GetExcelValue(excelRow, "Category", "CategoryName");
                newRow["SubCategoryName"] = GetExcelValue(excelRow, "Sub Category", "SubCategoryName", "SubCategory");
                newRow["UploadId"] = uploadId;
                stagingData.Rows.Add(newRow);
            }

            using (SqlConnection con = GetConnection())
            {
                con.Open();
                using (SqlBulkCopy bulkCopy = new SqlBulkCopy(con))
                {
                    bulkCopy.DestinationTableName = "dbo.ItemMaster_DummyExcel";
                    foreach (DataColumn col in stagingData.Columns)
                        bulkCopy.ColumnMappings.Add(col.ColumnName, col.ColumnName);
                    bulkCopy.WriteToServer(stagingData);
                }
            }
        }

        public DataTable ValidateItemMasterUpload(int uploadId)
        {
            using (SqlConnection con = GetConnection())
            using (SqlCommand cmd = new SqlCommand("ValidateItemMasterAug25", con))
            using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@UploadId", SqlDbType.Int).Value = uploadId;
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                return dt;
            }
        }

        public DataTable GetFailedItemMasterRecords(int uploadId)
        {
            using (SqlConnection con = GetConnection())
            using (SqlCommand cmd = new SqlCommand("GetByUploadIdFailItemIMAug25", con))
            using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@UploadId", SqlDbType.Int).Value = uploadId;
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                return dt;
            }
        }

        // =====================================================
        // CUSTOMER MASTER
        // =====================================================

        public void BulkInsertCustomerMaster(DataTable excelData, int uploadId)
        {
            if (excelData == null) throw new ArgumentNullException("excelData");

            DataTable stagingData = new DataTable();
            stagingData.Columns.Add("CustCode", typeof(string));
            stagingData.Columns.Add("CustName", typeof(string));
            stagingData.Columns.Add("ContactPerson", typeof(string));
            stagingData.Columns.Add("BranchName", typeof(string));
            stagingData.Columns.Add("Address1", typeof(string));
            stagingData.Columns.Add("Address2", typeof(string));
            stagingData.Columns.Add("City", typeof(string));
            stagingData.Columns.Add("StateName", typeof(string));
            stagingData.Columns.Add("PinCode", typeof(string));
            stagingData.Columns.Add("Country", typeof(string));
            stagingData.Columns.Add("EmailId", typeof(string));
            stagingData.Columns.Add("Telephone", typeof(string));
            stagingData.Columns.Add("Mobile", typeof(string));
            stagingData.Columns.Add("Fax", typeof(string));
            stagingData.Columns.Add("WebSite", typeof(string));
            stagingData.Columns.Add("GSTIN", typeof(string));
            stagingData.Columns.Add("Remarks", typeof(string));
            stagingData.Columns.Add("UploadId", typeof(int));

            foreach (DataRow excelRow in excelData.Rows)
            {
                DataRow newRow = stagingData.NewRow();
                newRow["CustCode"] = GetExcelValue(excelRow, "CustCode", "Customer Code");
                newRow["CustName"] = GetExcelValue(excelRow, "CustName", "Customer Name");
                newRow["ContactPerson"] = GetExcelValue(excelRow, "ContactPerson", "Contact Person");
                newRow["BranchName"] = GetExcelValue(excelRow, "BranchName", "Branch");
                newRow["Address1"] = GetExcelValue(excelRow, "Address1", "Permanent Address");
                newRow["Address2"] = GetExcelValue(excelRow, "Address2", "Residential Address");
                newRow["City"] = GetExcelValue(excelRow, "City");
                newRow["StateName"] = GetExcelValue(excelRow, "StateName", "State");
                newRow["PinCode"] = GetExcelValue(excelRow, "PinCode", "Pin Code");
                newRow["Country"] = GetExcelValue(excelRow, "Country");
                newRow["EmailId"] = GetExcelValue(excelRow, "EmailId", "Email");
                newRow["Telephone"] = GetExcelValue(excelRow, "Telephone", "Phone");
                newRow["Mobile"] = GetExcelValue(excelRow, "Mobile", "Mobile No");
                newRow["Fax"] = GetExcelValue(excelRow, "Fax");
                newRow["WebSite"] = GetExcelValue(excelRow, "WebSite", "Website");
                newRow["GSTIN"] = GetExcelValue(excelRow, "GSTIN");
                newRow["Remarks"] = GetExcelValue(excelRow, "Remarks", "Remark");
                newRow["UploadId"] = uploadId;
                stagingData.Rows.Add(newRow);
            }

            using (SqlConnection con = GetConnection())
            {
                con.Open();
                using (SqlBulkCopy bulkCopy = new SqlBulkCopy(con))
                {
                    bulkCopy.DestinationTableName = "dbo.CustomerMaster_DummyExcel";
                    foreach (DataColumn col in stagingData.Columns)
                        bulkCopy.ColumnMappings.Add(col.ColumnName, col.ColumnName);
                    bulkCopy.WriteToServer(stagingData);
                }
            }
        }

        public DataTable ValidateCustomerMasterUpload(int uploadId)
        {
            using (SqlConnection con = GetConnection())
            using (SqlCommand cmd = new SqlCommand("ValidateCustomerMaster", con))
            using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@UploadId", SqlDbType.Int).Value = uploadId;
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                return dt;
            }
        }

        public DataTable GetFailedCustomerMasterRecords(int uploadId)
        {
            using (SqlConnection con = GetConnection())
            using (SqlCommand cmd = new SqlCommand("GetByUploadIdFailCustomer", con))
            using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@UploadId", SqlDbType.Int).Value = uploadId;
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                return dt;
            }
        }

        // =====================================================
        // SUPPLIER MASTER
        // =====================================================

        public void BulkInsertSupplierMaster(DataTable excelData, int uploadId)
        {
            if (excelData == null) throw new ArgumentNullException("excelData");

            DataTable stagingData = new DataTable();
            stagingData.Columns.Add("RawMaterial", typeof(string));
            stagingData.Columns.Add("SupplierCode", typeof(string));
            stagingData.Columns.Add("SupplierName", typeof(string));
            stagingData.Columns.Add("ContactPerson", typeof(string));
            stagingData.Columns.Add("Branch", typeof(string));
            stagingData.Columns.Add("Address1", typeof(string));
            stagingData.Columns.Add("Address2", typeof(string));
            stagingData.Columns.Add("City", typeof(string));
            stagingData.Columns.Add("State", typeof(string));
            stagingData.Columns.Add("Country", typeof(string));
            stagingData.Columns.Add("PinCode", typeof(string));
            stagingData.Columns.Add("EmailID", typeof(string));
            stagingData.Columns.Add("Telephone", typeof(string));
            stagingData.Columns.Add("Mobile", typeof(string));
            stagingData.Columns.Add("Fax", typeof(string));
            stagingData.Columns.Add("Website", typeof(string));
            stagingData.Columns.Add("GSTIN", typeof(string));
            stagingData.Columns.Add("Remarks", typeof(string));
            stagingData.Columns.Add("UploadId", typeof(int));

            foreach (DataRow excelRow in excelData.Rows)
            {
                DataRow newRow = stagingData.NewRow();
                newRow["RawMaterial"] = GetExcelValue(excelRow, "RawMaterial", "Raw Material");
                newRow["SupplierCode"] = GetExcelValue(excelRow, "SupplierCode", "Supplier Code");
                newRow["SupplierName"] = GetExcelValue(excelRow, "SupplierName", "Supplier Name");
                newRow["ContactPerson"] = GetExcelValue(excelRow, "ContactPerson", "Contact Person");
                newRow["Branch"] = GetExcelValue(excelRow, "Branch");
                newRow["Address1"] = GetExcelValue(excelRow, "Address1", "Address 1");
                newRow["Address2"] = GetExcelValue(excelRow, "Address2", "Address 2");
                newRow["City"] = GetExcelValue(excelRow, "City");
                newRow["State"] = GetExcelValue(excelRow, "State");
                newRow["Country"] = GetExcelValue(excelRow, "Country");
                newRow["PinCode"] = GetExcelValue(excelRow, "PinCode", "Pin Code");
                newRow["EmailID"] = GetExcelValue(excelRow, "EmailID", "Email ID", "Email");
                newRow["Telephone"] = GetExcelValue(excelRow, "Telephone", "Phone");
                newRow["Mobile"] = GetExcelValue(excelRow, "Mobile", "Mobile No");
                newRow["Fax"] = GetExcelValue(excelRow, "Fax");
                newRow["Website"] = GetExcelValue(excelRow, "Website", "Web Site");
                newRow["GSTIN"] = GetExcelValue(excelRow, "GSTIN");
                newRow["Remarks"] = GetExcelValue(excelRow, "Remarks", "Remark");
                newRow["UploadId"] = uploadId;
                stagingData.Rows.Add(newRow);
            }

            using (SqlConnection con = GetConnection())
            {
                con.Open();
                using (SqlBulkCopy bulkCopy = new SqlBulkCopy(con))
                {
                    bulkCopy.DestinationTableName = "dbo.SupplierMaster_DummyExcel";
                    foreach (DataColumn col in stagingData.Columns)
                        bulkCopy.ColumnMappings.Add(col.ColumnName, col.ColumnName);
                    bulkCopy.WriteToServer(stagingData);
                }
            }
        }

        public DataTable ValidateSupplierMasterUpload(int uploadId)
        {
            using (SqlConnection con = GetConnection())
            using (SqlCommand cmd = new SqlCommand("ValidateSupplierMaster", con))
            using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@UploadId", SqlDbType.Int).Value = uploadId;
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                return dt;
            }
        }

        public DataTable GetFailedSupplierMasterRecords(int uploadId)
        {
            using (SqlConnection con = GetConnection())
            using (SqlCommand cmd = new SqlCommand("GetByUploadIdFailSupplier", con))
            using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@UploadId", SqlDbType.Int).Value = uploadId;
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                return dt;
            }
        }

        // =====================================================
        // HELPER METHOD
        // =====================================================

        private string GetExcelValue(DataRow row, params string[] names)
        {
            foreach (string name in names)
            {
                foreach (DataColumn column in row.Table.Columns)
                {
                    if (string.Equals(column.ColumnName.Trim(), name, StringComparison.OrdinalIgnoreCase))
                    {
                        if (row[column] == DBNull.Value)
                            return null;

                        string value = Convert.ToString(row[column]);
                        return string.IsNullOrWhiteSpace(value) ? null : value.Trim();
                    }
                }
            }
            return null;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StandardMetals.DAL;
using StandardMetals.MODEL;

namespace StandardMetals.BAL
{
    public class BulckUploadBALApril2026
    {
        // Instance of your Data Access Layer (DAL)
        BulckUploadDALApril2026 dal = new BulckUploadDALApril2026();

        // ==========================================
        // Common Methods
        // ==========================================

        public List<BulkUploadeModel> GetTableList()
        {
            return dal.GetTableList();
        }

        public int InsertUploadMaster(string fileName, string masterType)
        {
            return dal.InsertUploadMaster(fileName, masterType);
        }

        // ==========================================
        // ITEM MASTER Methods
        // ==========================================

        public void BulkInsertItemMaster(DataTable excelData, int uploadId)
        {
            dal.BulkInsertItemMaster(excelData, uploadId);
        }

        public DataTable ValidateItemMasterUpload(int uploadId)
        {
            return dal.ValidateItemMasterUpload(uploadId);
        }

        public DataTable GetFailedItemMasterRecords(int uploadId)
        {
            return dal.GetFailedItemMasterRecords(uploadId);
        }


        // ==========================================
        // CUSTOMER MASTER Methods
        // ==========================================

        public void BulkInsertCustomerMaster(DataTable excelData, int uploadId)
        {
            dal.BulkInsertCustomerMaster(excelData, uploadId);
        }

        public DataTable ValidateCustomerMasterUpload(int uploadId)
        {
            return dal.ValidateCustomerMasterUpload(uploadId);
        }

        public DataTable GetFailedCustomerMasterRecords(int uploadId)
        {
            return dal.GetFailedCustomerMasterRecords(uploadId);
        }


        // ==========================================
        // SUPPLIER MASTER Methods
        // ==========================================

        public void BulkInsertSupplierMaster(DataTable excelData, int uploadId)
        {
            dal.BulkInsertSupplierMaster(excelData, uploadId);
        }

        public DataTable ValidateSupplierMasterUpload(int uploadId)
        {
            return dal.ValidateSupplierMasterUpload(uploadId);
        }

        public DataTable GetFailedSupplierMasterRecords(int uploadId)
        {
            return dal.GetFailedSupplierMasterRecords(uploadId);
        }
    }
}

using StandardMetals.CommonLibrary; 
using StandardMetals.DAL;
using StandardMetals.MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StandardMetals.BAL
{
    public class SupplierMasterBalApril2026
    {
        SupplierMasterDalApril2026 suppliermaster_Dal = new SupplierMasterDalApril2026();

        public List<SupplierMasterModelApril2026> GetAllSupplier()
        {
            LogInfo.LogDetailsToFile("SupplierMasterBal- GetAllSupplier Action- Start");
            var result = suppliermaster_Dal.GetAllSupplier();
            LogInfo.LogDetailsToFile("SupplierMasterBal- GetAllSupplier Action- Start-Success");
            return result;
        }

        public SupplierMasterModelApril2026 GetByIdSupplier(int id)
        {
            LogInfo.LogDetailsToFile("SupplierMasterBal- GetByIdSupplier Action- Start");
            var result = suppliermaster_Dal.GetByIdSupplier(id);
            LogInfo.LogDetailsToFile("SupplierMasterBal- GetByIdSupplier Action- Start-End");
            return result;
        }

        public string InsertSupplier(SupplierMasterModelApril2026 model)
        {
            LogInfo.LogDetailsToFile("SupplierMasterBal- InsertSupplier Action- Start");
            var result = suppliermaster_Dal.InsertSupplier(model);
            LogInfo.LogDetailsToFile("SupplierMasterBal- InsertSupplier Action- Start-End");
            return result;
        }

        public int UpdateSupplier(int id, SupplierMasterModelApril2026 model)
        {
            LogInfo.LogDetailsToFile("SupplierMasterBal- UpdateSupplier Action- Start");
            var result = suppliermaster_Dal.UpdateSupplier(id, model);
            LogInfo.LogDetailsToFile("SupplierMasterBal- UpdateSupplier Action- Start-End");
            return result;
        }
    }
}
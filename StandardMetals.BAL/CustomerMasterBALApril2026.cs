using StandardMetals.CommonLibrary;
using StandardMetals.DAL;
using StandardMetals.MODEL;
using System.Collections.Generic;

namespace StandardMetals.BAL
{
    public class CustomerMasterBALApril2026
    {
        CustomerMasterDALApril2026 customerDal = new CustomerMasterDALApril2026();

        // Customer Drp
        public List<CustomerMasterModelApril2026> GetAllCustomer()
        {
            LogInfo.LogDetailsToFile("CustomerMasterBAL- GetAllCustomer Action- Start");
            var result = customerDal.GetAllCustomer();
            LogInfo.LogDetailsToFile("CustomerMasterBAL- GetAllCustomer Action- Start-Success");
            return result;
        }

        // Get Customer By Id(PK)
        public CustomerMasterModelApril2026 GetById(int id)
        {
            LogInfo.LogDetailsToFile("CustomerMasterBAL- GetById Action- Start");
            var result = customerDal.GetById(id);
            LogInfo.LogDetailsToFile("CustomerMasterBAL- GetById Action- Start-End");
            return result;
        }

        // Branch Drp
        public List<CustomerMasterModelApril2026> GetBranch()
        {
            LogInfo.LogDetailsToFile("CustomerMasterBAL- GetBranch Action- Start");
            var result = customerDal.GetBranch();
            LogInfo.LogDetailsToFile("CustomerMasterBAL- GetBranch Action- Start-End");
            return result;
        }

        // Countries Drp
        public List<CustomerMasterModelApril2026> GetCountries()
        {
            LogInfo.LogDetailsToFile("CustomerMasterBAL- GetCountries Action- Start");
            var result = customerDal.GetCountries();
            LogInfo.LogDetailsToFile("CustomerMasterBAL- GetCountries Action- Start-End");
            return result;
        }

        // State Drp
        public List<CustomerMasterModelApril2026> GetStates()
        {
            LogInfo.LogDetailsToFile("CustomerMasterBAL- GetStates Action- Start");
            var result = customerDal.GetStates();
            LogInfo.LogDetailsToFile("CustomerMasterBAL- GetStates Action- Start-End");
            return result;
        }

        // Cities Drp
        public List<CustomerMasterModelApril2026> GetCities()
        {
            LogInfo.LogDetailsToFile("CustomerMasterBAL- GetCities Action- Start");
            var result = customerDal.GetCities();
            LogInfo.LogDetailsToFile("CustomerMasterBAL- GetCities Action- Start-End");
            return result;
        }

        // Inserting Records in DB
        public string SaveData(int id, CustomerMasterModelApril2026 model)
        {
            LogInfo.LogDetailsToFile("CustomerMasterBAL- SaveData Action- Start");
            var result = customerDal.SaveData(id, model).ToString();
            LogInfo.LogDetailsToFile("CustomerMasterBAL- SaveData Action- Start-End");
            return result;
        }

        // Updating existing Record in DB
        public string Update(int id, CustomerMasterModelApril2026 model)
        {
            LogInfo.LogDetailsToFile("CustomerMasterBAL- Update Action- Start");
            var result = customerDal.Update(id, model).ToString();
            LogInfo.LogDetailsToFile("CustomerMasterBAL- Update Action- Start-End");
            return result;
        }

        // Inserting Records in DB
        public string InsertCustomerMaster(CustomerMasterModelApril2026 model)
        {
            LogInfo.LogDetailsToFile("CustomerMasterBAL- InsertCustomerMaster Action- Start");
            var result = customerDal.InsertCustomerMaster(model);
            LogInfo.LogDetailsToFile("CustomerMasterBAL- InsertCustomerMaster Action- Start-End");
            return result;
        }

        // Updating existing Record in DB
        public int UpdateCustomerMaster(int id, CustomerMasterModelApril2026 model)
        {
            LogInfo.LogDetailsToFile("CustomerMasterBAL- UpdateCustomerMaster Action- Start");
            var result = customerDal.UpdateCustomerMaster(id, model);
            LogInfo.LogDetailsToFile("CustomerMasterBAL- UpdateCustomerMaster Action- Start-End");
            return result;
        }

        public List<CustomerMasterModelApril2026> GetCitiesFromMaster()
        {
            LogInfo.LogDetailsToFile("CustomerMasterBAL- GetCitiesFromMaster Action- Start");
            var result = customerDal.GetCitiesFromMaster();
            LogInfo.LogDetailsToFile("CustomerMasterBAL- GetCitiesFromMaster Action- Start-End");
            return result;
        }

    }
}
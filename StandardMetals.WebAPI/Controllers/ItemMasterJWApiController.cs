using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Cors;
using StandardMetals.BAL;
using StandardMetals.MODEL;
using StandardMetals.CommonLibrary; 

namespace StandardMetals.WebAPI.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ItemMasterJWApiController : ApiController
    {
        ItemMasterBALApril2026 itemBAL = new ItemMasterBALApril2026();


        // GET: http://localhost:62335/ItemMasterJWApi/GetItemCodes
        [HttpGet]
        public List<ItemMasterModalApril2026> GetItemCodes()
        {
            LogInfo.LogDetailsToFile("ItemMasterJWApiController- GetItemCodes Action- Start");
            var result = itemBAL.GetItemCodes();
            LogInfo.LogDetailsToFile("ItemMasterJWApiController- GetItemCodes Action- Start-End");
            return result;
        }


        // GET: http://localhost:62335/ItemMasterJWApi/GetItemNames
        [HttpGet]
        public List<ItemMasterModalApril2026> GetItemNames()
        {
            LogInfo.LogDetailsToFile("ItemMasterJWApiController- GetItemNames Action- Start");
            var result = itemBAL.GetItemNames();
            LogInfo.LogDetailsToFile("ItemMasterJWApiController- GetItemNames Action- Start-End");
            return result;
        }


        // GET: http://localhost:62335/ItemMasterJWApi/GetManufacturers
        [HttpGet]
        public List<ItemMasterModalApril2026> GetManufacturers()
        {
            LogInfo.LogDetailsToFile("ItemMasterJWApiController- GetManufacturers Action- Start");
            var result = itemBAL.GetManufacturerDetails();
            LogInfo.LogDetailsToFile("ItemMasterJWApiController- GetManufacturers Action- Start-End");
            return result;
        }


        // GET: http://localhost:62335/ItemMasterJWApi/GetCategories
        [HttpGet]
        public List<ItemMasterModalApril2026> GetCategories()
        {
            LogInfo.LogDetailsToFile("ItemMasterJWApiController- GetCategories Action- Start");
            var result = itemBAL.GetCategoriesDetails();
            LogInfo.LogDetailsToFile("ItemMasterJWApiController- GetCategories Action- Start-End");
            return result;
        }


        // GET: http://localhost:62335/ItemMasterJWApi/GetSubCategories?id=1
        [HttpGet]
        public List<ItemMasterModalApril2026> GetSubCategories(int id)
        {
            LogInfo.LogDetailsToFile("ItemMasterJWApiController- GetSubCategories Action- Start");
            var result = itemBAL.GetSubCategoriesDetails(id);
            LogInfo.LogDetailsToFile("ItemMasterJWApiController- GetSubCategories Action- Start-End");
            return result;
        }


        // GET: http://localhost:62335/ItemMasterJWApi/GetColours
        [HttpGet]
        public List<ItemMasterModalApril2026> GetColours()
        {
            LogInfo.LogDetailsToFile("ItemMasterJWApiController- GetColours Action- Start");
            var result = itemBAL.GetAllColours();
            LogInfo.LogDetailsToFile("ItemMasterJWApiController- GetColours Action- Start-End");
            return result;
        }


        // GET: http://localhost:62335/ItemMasterJWApi/GetUnits
        [HttpGet]
        public List<ItemMasterModalApril2026> GetUnits()
        {
            LogInfo.LogDetailsToFile("ItemMasterJWApiController- GetUnits Action- Start");
            var result = itemBAL.GetUnitDetails();
            LogInfo.LogDetailsToFile("ItemMasterJWApiController- GetUnits Action- Start-End");
            return result;
        }


        // GET: http://localhost:62335/ItemMasterJWApi/GetItemById?id=1
        [HttpGet]
        public ItemMasterModalApril2026 GetItemById(int id)
        {
            LogInfo.LogDetailsToFile("ItemMasterJWApiController- GetItemById Action- Start");
            var result = itemBAL.GetItemById(id);
            LogInfo.LogDetailsToFile("ItemMasterJWApiController- GetItemById Action- Start-End");
            return result;
        }


        // POST: http://localhost:62335/ItemMasterJWApi/CreateItem
        [HttpPost]
        public string CreateItem(ItemMasterModalApril2026 model)
        {
            LogInfo.LogDetailsToFile("ItemMasterJWApiController- CreateItem Action- Start");
            var result = itemBAL.SaveItemJW(model);
            LogInfo.LogDetailsToFile("ItemMasterJWApiController- CreateItem Action- Start-End");
            return result;
        }


        // PUT: http://localhost:62335/ItemMasterJWApi/UpdateItem?id=1
        [HttpPut]
        public string UpdateItem(int id, ItemMasterModalApril2026 model)
        {
            LogInfo.LogDetailsToFile("ItemMasterJWApiController- UpdateItem Action- Start");
            var result = itemBAL.UpdateItemJW(id, model);
            LogInfo.LogDetailsToFile("ItemMasterJWApiController- UpdateItem Action- Start-End");
            return result;
        }
    }
}
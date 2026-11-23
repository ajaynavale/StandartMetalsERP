using StandardMetals.BAL;
using StandardMetals.CommonLibrary;
using StandardMetals.MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace StandardMetals.WebAPI.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    //[RoutePrefix("api/ItemMaster")]
    public class ItemMasterCtrlController : ApiController
    {
         ItemMasterBALApril2026 itemBAL = new ItemMasterBALApril2026();

        [HttpGet]
        //[Route("GetAllItemMaster")]
        public List<ItemMasterModalApril2026> GetAllItemList()
        {
            LogInfo.LogDetailsToFile("ItemMasterCtrl GetAllItemList Action");

            return itemBAL.GetAllItemList();
        }

        [HttpGet]
        public ItemMasterModalApril2026 GetItemById(int id)
        {
            LogInfo.LogDetailsToFile("ItemMasterCtrl GetItemById Action : " + id);

            return itemBAL.GetItemById(id);
        }

        [HttpPost]
        public string InsertItemMaster(ItemMasterModalApril2026 model)
        {
            LogInfo.LogDetailsToFile("ItemMasterCtrl InsertItemMaster Action");

            return itemBAL.InsertItemMaster(model);
        }


        [HttpPut]
        public int UpdateItemMaster(int id, ItemMasterModalApril2026 model)
        {
            LogInfo.LogDetailsToFile("ItemMasterCtrl UpdateItemMaster Action");

            return itemBAL.UpdateItemMaster(id, model);
        }

        //Getting Data for dropDown list
        [HttpGet]
        public List<ItemMasterModalApril2026> GetAllColours()
        {
            LogInfo.LogDetailsToFile("ItemMasterCtrl GetAllColours Action");

            return itemBAL.GetAllColours();
        }

        [HttpGet]
        public List<ItemMasterModalApril2026> GetManufacturerDetails()
        {
            LogInfo.LogDetailsToFile("ItemMasterCtrl GetManufacturerDetails Action");

            return itemBAL.GetManufacturerDetails();
        }

        [HttpGet]
        public List<ItemMasterModalApril2026> GetCategoriesDetails()
        {
            LogInfo.LogDetailsToFile("ItemMasterCtrl GetCategoriesDetails Action");

            return itemBAL.GetCategoriesDetails();
        }

        [HttpGet]
        public List<ItemMasterModalApril2026> GetSubCategoriesDetails(int id)
        {
            LogInfo.LogDetailsToFile("ItemMasterCtrl GetSubCategoresDetails Action");

            return itemBAL.GetSubCategoriesDetails(id);
        }

        [HttpGet]
        public List<ItemMasterModalApril2026> GetUnitDetails()
        {
            LogInfo.LogDetailsToFile("ItemMasterCtrl GetUnitDetails Action");

            return itemBAL.GetUnitDetails();
        }
        //for jquery project
        //getting all the availbale itemcodes

        [HttpGet]
        public List<ItemMasterModalApril2026> GetItemCodes()
        {
            LogInfo.LogDetailsToFile("ItemMasterCtrl GetItemCodes Action");

            return itemBAL.GetItemCodes();
        }

        // getting all the available itemnames

        [HttpGet]
        public List<ItemMasterModalApril2026> GetItemNames()
        {
            LogInfo.LogDetailsToFile("ItemMasterCtrl GetItemNames Action");

            return itemBAL.GetItemNames();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StandardMetals.DAL;
using StandardMetals.MODEL;
using StandardMetals.CommonLibrary;

namespace StandardMetals.BAL
{
    public class ItemMasterBALApril2026
    {

        ItemMasterDALApril2026 ItemMasterDalDec17 = new ItemMasterDALApril2026();

        public List<ItemMasterModalApril2026> GetAllItemList()
        {
            LogInfo.LogDetailsToFile("ItemMasterBAL- GetAllItemList Action- Start");
            var result = ItemMasterDalDec17.GetAllItemList();
            LogInfo.LogDetailsToFile("ItemMasterBAL- GetAllItemList Action- Start-Success");
            return result;
        }

        public string InsertItemMaster(ItemMasterModalApril2026 model)
        {
            LogInfo.LogDetailsToFile("ItemMasterBAL- InsertItemMaster Action- Start");
            var result = ItemMasterDalDec17.InsertItemMaster(model);
            LogInfo.LogDetailsToFile("ItemMasterBAL- InsertItemMaster Action- Start-End");
            return result;
        }

        public ItemMasterModalApril2026 GetItemById(int id)
        {
            LogInfo.LogDetailsToFile("ItemMasterBAL- GetItemById Action- Start");
            var result = ItemMasterDalDec17.GetItemById(id);
            LogInfo.LogDetailsToFile("ItemMasterBAL- GetItemById Action- Start-End");
            return result;
        }

        public int UpdateItemMaster(int id, ItemMasterModalApril2026 model)
        {
            LogInfo.LogDetailsToFile("ItemMasterBAL- UpdateItemMaster Action- Start");
            var result = ItemMasterDalDec17.UpdateItemMaster(id, model);
            LogInfo.LogDetailsToFile("ItemMasterBAL- UpdateItemMaster Action- Start-End");
            return result;
        }

        //Getting Data for dropDown list

        public List<ItemMasterModalApril2026> GetAllColours()
        {
            LogInfo.LogDetailsToFile("ItemMasterBAL- GetAllColours Action- Start");
            var result = ItemMasterDalDec17.GetColours();
            LogInfo.LogDetailsToFile("ItemMasterBAL- GetAllColours Action- Start-End");
            return result;
        }

        public List<ItemMasterModalApril2026> GetManufacturerDetails()
        {
            LogInfo.LogDetailsToFile("ItemMasterBAL- GetManufacturerDetails Action- Start");
            var result = ItemMasterDalDec17.GetManufacturer();
            LogInfo.LogDetailsToFile("ItemMasterBAL- GetManufacturerDetails Action- Start-End");
            return result;
        }

        public List<ItemMasterModalApril2026> GetCategoriesDetails()
        {
            LogInfo.LogDetailsToFile("ItemMasterBAL- GetCategoriesDetails Action- Start");
            var result = ItemMasterDalDec17.GetCategories();
            LogInfo.LogDetailsToFile("ItemMasterBAL- GetCategoriesDetails Action- Start-End");
            return result;
        }

        public List<ItemMasterModalApril2026> GetSubCategoriesDetails(int id)
        {
            LogInfo.LogDetailsToFile("ItemMasterBAL- GetSubCategoriesDetails Action- Start");
            var result = ItemMasterDalDec17.GetSubCategories(id);
            LogInfo.LogDetailsToFile("ItemMasterBAL- GetSubCategoriesDetails Action- Start-End");
            return result;
        }

        public List<ItemMasterModalApril2026> GetUnitDetails()
        {
            LogInfo.LogDetailsToFile("ItemMasterBAL- GetUnitDetails Action- Start");
            var result = ItemMasterDalDec17.GetUnit();
            LogInfo.LogDetailsToFile("ItemMasterBAL- GetUnitDetails Action- Start-End");
            return result;
        }
        //for jquery project
        //getting all the availbale itemcodes

        public List<ItemMasterModalApril2026> GetItemCodes()
        {
            LogInfo.LogDetailsToFile("ItemMasterBAL- GetItemCodes Action- Start");
            var result = ItemMasterDalDec17.getItemCodes();
            LogInfo.LogDetailsToFile("ItemMasterBAL- GetItemCodes Action- Start-End");
            return result;
        }

        // getting all the available itemnames

        public List<ItemMasterModalApril2026> GetItemNames()
        {
            LogInfo.LogDetailsToFile("ItemMasterBAL- GetItemNames Action- Start");
            var result = ItemMasterDalDec17.getItemNames();
            LogInfo.LogDetailsToFile("ItemMasterBAL- GetItemNames Action- Start-End");
            return result;
        }
        // Save Item Master for JQuery Item Master form

        public string SaveItemJW(ItemMasterModalApril2026 model)
        {
            LogInfo.LogDetailsToFile("ItemMasterBAL- SaveItemJW Action- Start");
            var result = ItemMasterDalDec17.SaveItemJW(model);
            LogInfo.LogDetailsToFile("ItemMasterBAL- SaveItemJW Action- Start-End");
            return result;
        }
        // Update Item for JQuery Item Master form

        public string UpdateItemJW(int id, ItemMasterModalApril2026 model)
        {
            LogInfo.LogDetailsToFile("ItemMasterBAL- UpdateItemJW Action- Start");
            var result = ItemMasterDalDec17.UpdateItemJW(id, model);
            LogInfo.LogDetailsToFile("ItemMasterBAL- UpdateItemJW Action- Start-End");
            return result;
        }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using StandardMetals.MODEL;


namespace StandardMetals.WebApp.Controllers
{
    public class MasterCsharpController : Controller
    {
        // GET: MasterCsharp

        private void LoadDropDown()
        {
            HttpClient client = new HttpClient();
            //string url = "http://localhost:62335/ItemMasterCtrl/GetManufacturerDetails";

            // Manufacturers
            var response1 = client.GetAsync("http://localhost:62335/ItemMasterCtrl/GetManufacturerDetails").Result;
            if (response1.IsSuccessStatusCode)
            {
                string data = response1.Content.ReadAsStringAsync().Result;
                ViewBag.Manufacturers = JsonConvert.DeserializeObject<List<ItemMasterModalApril2026>>(data);
            }

            // Category
            var response2 = client.GetAsync("http://localhost:62335/ItemMasterCtrl/GetCategoriesDetails").Result;
            if (response2.IsSuccessStatusCode)
            {
                string data = response2.Content.ReadAsStringAsync().Result;
                ViewBag.Categories = JsonConvert.DeserializeObject<List<ItemMasterModalApril2026>>(data);
            }

            //Colour
            var response3 = client.GetAsync("http://localhost:62335/ItemMasterCtrl/GetAllColours").Result;
            if (response3.IsSuccessStatusCode)
            {
                string data = response3.Content.ReadAsStringAsync().Result;
                ViewBag.ColourList = JsonConvert.DeserializeObject<List<ItemMasterModalApril2026>>(data);
            }

            // Unit
            var response4 = client.GetAsync("http://localhost:62335/ItemMasterCtrl/GetUnitDetails").Result;
            if (response4.IsSuccessStatusCode)
            {
                string data = response4.Content.ReadAsStringAsync().Result;
                ViewBag.UnitList = JsonConvert.DeserializeObject<List<ItemMasterModalApril2026>>(data);
            }

        }

        [HttpGet]
        public ActionResult ItemMasterCsharplist()
        {
            List<ItemMasterModalApril2026> model = new List<ItemMasterModalApril2026>();

            HttpClient Client = new HttpClient();

            string url = "http://localhost:62335/ItemMasterCtrl/GetAllItemList";

            HttpResponseMessage response = Client.GetAsync(url).Result;

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                model = JsonConvert.DeserializeObject<List<ItemMasterModalApril2026>>(data);
            }

            return View(model);
        }

        [HttpGet]
        public JsonResult GetSubCategoryN(int id)
        {
            HttpClient client = new HttpClient();

            string url = "http://localhost:62335/ItemMasterCtrl/GetSubCategoriesDetails/" + id;

            HttpResponseMessage responseMessage = client.GetAsync(url).Result;

            if (responseMessage.IsSuccessStatusCode)
            {
                string data = responseMessage.Content.ReadAsStringAsync().Result;
                var result = JsonConvert.DeserializeObject<List<ItemMasterModalApril2026>>(data);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            return Json(null, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult AddNewItem17Dec()
        {
            LoadDropDown();

            // empty sbucate for first load
            ViewBag.SubCategories = new List<ItemMasterModalApril2026>();

            return PartialView("_AddNewItem17Dec");
        }

        [HttpPost]
        public ActionResult AddNewItem17Dec(ItemMasterModalApril2026 model)
        {
            HttpClient client = new HttpClient();

            if (ModelState.IsValid == true)
            {
                string url = "http://localhost:62335/ItemMasterCtrl/InsertItemMaster";
                string json = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage responseMessage = client.PostAsync(url, content).Result;
                if (responseMessage.IsSuccessStatusCode)
                {
                    return Json(new { success = true, code = responseMessage.Content.ReadAsStringAsync().Result, message = "Item Saved Successfully" });
                }
            }

            //  return PartialView("_AddNewItem17Dec", model);
            return Json(new
            {
                success = false,
                errors = ModelState.Where(x => x.Value.Errors.Count > 0)
                .ToDictionary(x => x.Key, x => x.Value.Errors.Select(e => e.ErrorMessage).ToArray())
            });

        }

        [HttpGet]
        public ActionResult EditItem17Dec(int id)
        {
            HttpClient client = new HttpClient();
            ItemMasterModalApril2026 model = new ItemMasterModalApril2026();

            var response = client.GetAsync("http://localhost:62335/ItemMasterCtrl/GetItemById/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                model = JsonConvert.DeserializeObject<ItemMasterModalApril2026>(data);
            }

            LoadDropDown();
            // SubCategory
            var response1 = client.GetAsync("http://localhost:62335/ItemMasterCtrl/GetSubCategoriesDetails/" + model.CategoryId).Result;
            if (response1.IsSuccessStatusCode)
            {
                string data = response1.Content.ReadAsStringAsync().Result;
                ViewBag.SubCategories = JsonConvert.DeserializeObject<List<ItemMasterModalApril2026>>(data);
            }

            return PartialView("_EditItem17Dec", model);
        }

        [HttpPost]
        public ActionResult EditItem17Dec(ItemMasterModalApril2026 model)
        {
            HttpClient client = new HttpClient();

            //if (ModelState.IsValid == true)
            //{}
            string url = "http://localhost:62335/ItemMasterCtrl/UpdateItemMaster/" + model.Id;
            string json = JsonConvert.SerializeObject(model);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage responseMessage = client.PutAsync(url, content).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                return Json(new { success = true, message = "Item Updated Successfully" });
            }

            LoadDropDown();

            return PartialView("_EditItem17Dec", model);
        }
    }
}
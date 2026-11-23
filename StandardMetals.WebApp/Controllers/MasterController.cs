using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using StandardMetals.BAL;
using StandardMetals.MODEL;
using StandardMetals.WebApp.UserAccessDtl;
using StandardMetals.CommonLibrary;
using StandardMetals.DAL;
using System.IO;
using ExcelDataReader;
using System.Text;
using ClosedXML.Excel;
using System.Reflection;
using System.ComponentModel.DataAnnotations;

namespace StandardMetals.WebApp.Controllers
{

    public class MasterController : Controller
    {
        public ActionResult DefaultDashboard()
        {
            LogInfo.LogDetailsToFile("Master Ctrl- DefaultDashboard Action(httpget)");

            DefaultDashBoard_BAL defaultDashboard_Bal = new DefaultDashBoard_BAL();

            DefaultModal supplierData = defaultDashboard_Bal.GetSupplierWiseCount();
            DefaultModal customerData = defaultDashboard_Bal.GetCustomerWiseCount();

            DefaultModal model = new DefaultModal
            {
                SupplierList = supplierData.SupplierList,
                TotalSuppCount = supplierData.TotalSuppCount,
                CustomerList = customerData.CustomerList,
                TotalCustCount = customerData.TotalCustCount,
                TotalItemCount = supplierData.TotalItemCount 
            };

            return View(model);
        }
        //protected override void OnException(ExceptionContext filterContext)
        //{
        //    Exception ex = filterContext.Exception;

        //    LogInfo.LogDetailsToFile("Unhandled Exception occurred in MasterController.", ex);
        //    filterContext.ExceptionHandled = true; 
        //    filterContext.Result = new ContentResult  
        //    {
        //        Content = "ERROR: " + ex.Message
        //    };
        //}

        // GET: Master
        //public ActionResult DefaultDashboard()
        //{
        //    return View();
        //}

        [HttpGet]
        public ActionResult ItemMaster()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ItemMaster(ItemMaster_MODEL_27March model)
        {

            return View();
        }


        public ActionResult ItemMasterDemo()
        {
            return View();
        }
        [HttpGet]
        public ActionResult ItemMasterScaffolding()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ItemMasterScaffolding(ItemMaster_MODEL_27March model)
        {

            return View();
        }

        public ActionResult BindDropDownThrouHtmlSTronglyConrol()
        {
            return View();
        }
        //Company Information
        public ActionResult CompanyMaster()
        {
            return View();
        }

        // Item Master Crud from Web Api
        public async Task<ActionResult> ItemMasterFromWebApi()
        {
            // variables are used to create URI
            //string FillItemMaster = "http://localhost:62335/ItemMaster/FillItemMaster";
            string BindManufacturer = "http://localhost:62335/ItemMaster/BindManfacturer";
            string BindItemCategory = "http://localhost:62335/ItemMaster/BindCategory";
            string BindColorMaster = "http://localhost:62335/ItemMaster/BindColor";
            string BindUnitMaster = "http://localhost:62335/ItemMaster/BindUom";

            //string FillItemMaster = ConfigurationManager.AppSettings[""].ToString();

            // Code to bind FillItemMaster dropdown
            //List<ItemMasterModel> listFillItemMaster = new List<ItemMasterModel>();
            //using (HttpClient client = new HttpClient())
            //{
            //    client.BaseAddress = new Uri(FillItemMaster);
            //    client.DefaultRequestHeaders.Accept.Clear();
            //    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            //    HttpResponseMessage response = await client.GetAsync(FillItemMaster);
            //    if (response.IsSuccessStatusCode)
            //    {

            //        var data = await response.Content.ReadAsStringAsync();
            //        //table = Newtonsoft.Json.JsonConvert.DeserializeObject<System.Data.DataTable>(data);
            //        //list = Newtonsoft.Json.JsonConvert.DeserializeObject<ItemMasterModel>(data);
            //        listFillItemMaster = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ItemMasterModel>>(data);

            //    }
            //}

            //var fillItemMasterList = listFillItemMaster.ToList();
            //ViewBag.ItemCodeList = new SelectList(fillItemMasterList, "ItemCode", "ItemCode");
            //ViewBag.ItemNameList = new SelectList(fillItemMasterList, "ItemCode", "ItemName");

            //code to bind Manufacturre dropdown
            DataTable dt = new DataTable();
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(BindManufacturer);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync(BindManufacturer);
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    //table = Newtonsoft.Json.JsonConvert.DeserializeObject<System.Data.DataTable>(data);
                    //list = Newtonsoft.Json.JsonConvert.DeserializeObject<ItemMasterModel>(data);
                    dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(data);

                }
            }
            List<ItemMasterModel> ManuFactList = new List<ItemMasterModel>();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ManuFactList.Add(new ItemMasterModel()
                    {
                        ManufacturerId = Convert.ToInt32(dt.Rows[i]["ManufacturerId"]),
                        ManufacturerName = dt.Rows[i]["ManufacturerName"].ToString()
                    });

                }
            }
            var ManufacturereList = ManuFactList;
            ViewBag.ManuList = new SelectList(ManufacturereList, "ManufacturerId", "ManufacturerName");
            // End Of the Manufacturere Binding Code dropdown

            //Code to bind Item Category dropdown
            DataTable tableIC = new DataTable();
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(BindItemCategory);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync(BindItemCategory);
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    //table = Newtonsoft.Json.JsonConvert.DeserializeObject<System.Data.DataTable>(data);
                    //list = Newtonsoft.Json.JsonConvert.DeserializeObject<ItemMasterModel>(data);
                    tableIC = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(data);

                }
            }
            List<ItemMasterModel> ItemCategoryList = new List<ItemMasterModel>();
            if (tableIC.Rows.Count > 0)
            {
                for (int i = 0; i < tableIC.Rows.Count; i++)
                {
                    ItemCategoryList.Add(new ItemMasterModel()
                    {
                        //ItemCategoryId = Convert.ToInt32(tableIC.Rows[i]["CategoryId"]),
                        CategoryId = Convert.ToInt32(tableIC.Rows[i]["CategoryId"]),
                        CategoryName = tableIC.Rows[i]["CategoryName"].ToString()
                    });

                }
            }
            var CategoryList = ItemCategoryList;
            ViewBag.CList = new SelectList(CategoryList, "CategoryId", "CategoryName");

            // End Of the Item Category Code dropdown


            //Code to Bind  Color Master dropdown

            DataTable TableColor = new DataTable();
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(BindColorMaster);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync(BindColorMaster);
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    //table = Newtonsoft.Json.JsonConvert.DeserializeObject<System.Data.DataTable>(data);
                    //list = Newtonsoft.Json.JsonConvert.DeserializeObject<ItemMasterModel>(data);
                    TableColor = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(data);

                }
            }
            List<ItemMasterModel> ColorList = new List<ItemMasterModel>();
            if (TableColor.Rows.Count > 0)
            {
                for (int i = 0; i < TableColor.Rows.Count; i++)
                {
                    ColorList.Add(new ItemMasterModel()
                    {
                        ColourId = Convert.ToInt32(TableColor.Rows[i]["ColourId"]),
                        ColourName = TableColor.Rows[i]["ColourName"].ToString()
                    });

                }
            }
            var ColorName = ColorList;
            ViewBag.ColorList = new SelectList(ColorName, "ColourId", "ColourName");

            // End Of the color Bind Master 

            //Code to Bind  Unit Master dropdown

            DataTable TableUnitMaster = new DataTable();
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(BindUnitMaster);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync(BindUnitMaster);
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    //table = Newtonsoft.Json.JsonConvert.DeserializeObject<System.Data.DataTable>(data);
                    //list = Newtonsoft.Json.JsonConvert.DeserializeObject<ItemMasterModel>(data);
                    TableUnitMaster = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(data);

                }
            }
            List<ItemMasterModel> UnitList = new List<ItemMasterModel>();
            if (TableUnitMaster.Rows.Count > 0)
            {
                for (int i = 0; i < TableUnitMaster.Rows.Count; i++)
                {
                    UnitList.Add(new ItemMasterModel()
                    {
                        UnitId = Convert.ToInt32(TableUnitMaster.Rows[i]["UnitId"]),
                        UnitName = TableUnitMaster.Rows[i]["UnitName"].ToString()
                    });

                }
            }
            var UnitMasterName = UnitList;
            ViewBag.UnitMater = new SelectList(UnitMasterName, "UnitId", "UnitName");

            return View();

        }

        [HttpPost]
        public async Task<ActionResult> GetItemSubCategory(int? CategoryId)
        {
            string BindSubCategory = "http://localhost:62335/ItemMaster/BindSubCategory1?CategoryId=" + CategoryId;

            DataTable TableSubCatgeory = new DataTable();
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(BindSubCategory);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync(BindSubCategory);
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    //table = Newtonsoft.Json.JsonConvert.DeserializeObject<System.Data.DataTable>(data);
                    //list = Newtonsoft.Json.JsonConvert.DeserializeObject<ItemMasterModel>(data);
                    TableSubCatgeory = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(data);

                }
            }
            List<ItemMasterModel> SubCategory = new List<ItemMasterModel>();
            if (TableSubCatgeory.Rows.Count > 0)
            {
                for (int i = 0; i < TableSubCatgeory.Rows.Count; i++)
                {
                    SubCategory.Add(new ItemMasterModel()
                    {
                        SubCategoryId = Convert.ToInt32(TableSubCatgeory.Rows[i]["SubCategoryId"]),
                        SubCategoryName = TableSubCatgeory.Rows[i]["SubCategoryName"].ToString()
                    });

                }
            }
            return Json(SubCategory, JsonRequestBehavior.AllowGet);

        }

        //Code to bind Item Name Code Against Item Code

        [HttpPost]
        public async Task<ActionResult> FillItemMaster()
        {
            string FillItemMaster = "http://localhost:62335/ItemMaster/FillItemMaster";

            // Code to bind FillItemMaster dropdown
            List<ItemMasterModel> listFillItemMaster = new List<ItemMasterModel>();
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(FillItemMaster);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync(FillItemMaster);
                if (response.IsSuccessStatusCode)
                {

                    var data = await response.Content.ReadAsStringAsync();
                    //table = Newtonsoft.Json.JsonConvert.DeserializeObject<System.Data.DataTable>(data);
                    //list = Newtonsoft.Json.JsonConvert.DeserializeObject<ItemMasterModel>(data);
                    listFillItemMaster = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ItemMasterModel>>(data);

                }
            }

            var fillItemMasterList = listFillItemMaster.ToList();
            //ViewBag.ItemCodeList = new SelectList(fillItemMasterList, "ItemCode", "ItemCode");
            //ViewBag.ItemNameList = new SelectList(fillItemMasterList, "ItemCode", "ItemName");


            return Json(fillItemMasterList, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> SearchItemMaster(string ItemCode)
        {
            string SearchIM = "http://localhost:62335/ItemMaster/SearchItemMaster?ItemCode=" + ItemCode;
            ItemMasterModel itemMasterModel = new ItemMasterModel();
            DataTable dt = new DataTable();

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(SearchIM);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync(SearchIM);
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    //table = Newtonsoft.Json.JsonConvert.DeserializeObject<System.Data.DataTable>(data);
                    //list = Newtonsoft.Json.JsonConvert.DeserializeObject<ItemMasterModel>(data);
                    itemMasterModel = Newtonsoft.Json.JsonConvert.DeserializeObject<ItemMasterModel>(data);

                }
            }
            //List<ItemMasterModel> SearchItemMaster = new List<ItemMasterModel>();
            //if (dt.Rows.Count > 0)
            //{
            //    for (int i = 0; i < dt.Rows.Count; i++)
            //    {
            //        SearchItemMaster.Add(new ItemMasterModel()
            //        {
            //            ItemCode = dt.Rows[i]["ItemCode"].ToString(),
            //            ItemName = dt.Rows[i]["ItemCode"].ToString(),
            //            ManufacturerId = Convert.ToInt32(dt.Rows[i]["ManufacturerId"]),
            //            Material = dt.Rows[i]["Material"].ToString(),
            //            CategoryId = Convert.ToInt32(dt.Rows[i]["CategoryId"]),
            //            SubCategoryId = Convert.ToInt32(dt.Rows[i]["SubCategoryId"]),
            //            ColourId = Convert.ToInt32(dt.Rows[i]["ColourId"]),
            //            UnitId = Convert.ToInt32(dt.Rows[i]["UnitId"]),
            //            HSNCODE = dt.Rows[i]["HSNCODE"].ToString(),
            //            GSTRate = Convert.ToDecimal(dt.Rows[i]["GSTRate"]),
            //            PurchaseCost = Convert.ToDecimal(dt.Rows[i]["PurchaseCost"]),
            //            SellingPrice = Convert.ToDecimal(dt.Rows[i]["SellingPrice"]),

            //        });

            //    }
            //}
            var SearchIMaster = itemMasterModel;
            return Json(SearchIMaster, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public async Task<ActionResult> ItemMasterFromWebApi(ItemMasterModel model)
        {

            if (ModelState.IsValid)
            {
                string SaveItemMasterModel = "http://localhost:62335/ItemMaster/SaveData";

                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(SaveItemMasterModel);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage response = await client.GetAsync(SaveItemMasterModel);
                    if (response.IsSuccessStatusCode)
                    {
                        var data = await response.Content.ReadAsStringAsync();
                        //table = Newtonsoft.Json.JsonConvert.DeserializeObject<System.Data.DataTable>(data);
                        //list = Newtonsoft.Json.JsonConvert.DeserializeObject<ItemMasterModel>(data);
                        model = Newtonsoft.Json.JsonConvert.DeserializeObject<ItemMasterModel>(data);

                    }
                }
                if (model != null)
                {
                    return RedirectToAction("DefaultDashboard", "Master");
                }
                else
                {
                    ViewBag.Alert = "Error Occured While Saving data";
                }

            }


            return View();
        }


        public ActionResult ItemMasterGridView()
        {
            return View();
        }



        ////////////////////////////////////////ItemMasterMVC////////////////////////////////

        ItemMasterMVCBAL bal = new ItemMasterMVCBAL();
        //ItemMasterBAL_Oct2025 bal = new ItemMasterBAL_Oct2025();
        //ItemMasterMVCBAL17Dec bal17 = new ItemMasterMVCBAL17Dec();


        [HttpGet]
        public ActionResult ItemList()
        {

            return View(bal.GetAllItemMaster());
        }


        [HttpGet]
        public JsonResult GetSubCategoryName(int categoryID)
        {
            return Json(bal.GetSubCategoriesDetails(categoryID), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult AddNewItem()
        {

            ViewBag.Manufacturers = bal.GetManufacturerDetails();
            ViewBag.Categories = bal.GetCategoriesDetails();
            ViewBag.SubCategories = new List<ItemMasterMVCModal>();
            ViewBag.ColourList = bal.GetAllColours();
            ViewBag.UnitList = bal.GetUnitDetails();

            return View();
        }

        [HttpPost]
        public ActionResult AddNewItem(ItemMasterMVCModal model)
        {
            if (ModelState.IsValid)
            {
                bal.SaveNewItem(model);
                return RedirectToAction("ItemList");
            }
            ViewBag.Manufacturers = bal.GetManufacturerDetails();
            ViewBag.Categories = bal.GetCategoriesDetails();
            ViewBag.ColourList = bal.GetAllColours();
            ViewBag.UnitList = bal.GetUnitDetails();
            ViewBag.SubCategories = bal.GetSubCategoriesDetails(model.CategoryId);

            return View(model);
        }

        [HttpGet]
        public ActionResult EditItemDetails(int id)
        {
            ItemMasterMVCModal model = bal.GetItemDetailsByID(id);

            ViewBag.Manufacturers = bal.GetManufacturerDetails();
            ViewBag.Categories = bal.GetCategoriesDetails();
            ViewBag.SubCategories = bal.GetSubCategoriesDetails(model.CategoryId);
            ViewBag.ColourList = bal.GetAllColours();
            ViewBag.UnitList = bal.GetUnitDetails();

            return PartialView(bal.GetItemDetailsByID(id));
        }

        [HttpPost]
        public ActionResult EditItemDetails(int id, ItemMasterMVCModal model)
        {
            if (ModelState.IsValid)
            {
                bal.UpdateExistingItem(id, model);
                return RedirectToAction("ItemList");
            }
            ViewBag.Manufacturers = bal.GetManufacturerDetails();
            ViewBag.Categories = bal.GetCategoriesDetails();
            ViewBag.ColourList = bal.GetAllColours();
            ViewBag.UnitList = bal.GetUnitDetails();
            ViewBag.SubCategories = bal.GetSubCategoriesDetails(model.CategoryId);


            return PartialView(model);
        }

        /// ////////////////////////////////////////////////////////////////////
        //ItemMasterApril2026

        SupplierMasterBalApril2026 SupplierMasterBalApr = new SupplierMasterBalApril2026();


        ItemMasterBALApril2026 bulkbal = new ItemMasterBALApril2026();
        //CustomerMasterBALApril2026 customerBal = new CustomerMasterBALApril2026();

        [HttpGet]
        public ActionResult ItemList17Dec()
        {
            LogInfo.LogDetailsToFile("Master Ctrl- ItemList17Dec Action(httpget)- Start");
            var list = bulkbal.GetAllItemList();
            LogInfo.LogDetailsToFile("Master Ctrl- ItemList17Dec Action(httpget)- Start-End");

            return View(list);
        }

        [HttpGet]
        public JsonResult GetSubCategoryN(int id)
        {
            LogInfo.LogDetailsToFile("Master Ctrl- GetSubCategoryN Action(httpget)- Start");

            if (id <= 0)
            {
                LogInfo.LogDetailsToFile("try - if- Inside subcategory If condition True");
                return Json(new List<ItemMasterModalApril2026>(), JsonRequestBehavior.AllowGet);
            }

            var list = bulkbal.GetSubCategoriesDetails(id);
            LogInfo.LogDetailsToFile("Master Ctrl- GetSubCategoryN Action(httpget)- Start-End");


            return Json(list, JsonRequestBehavior.AllowGet);

        }

        [HttpGet]
        public ActionResult AddNewItem17Dec()
        {
            try
            {
                LogInfo.LogDetailsToFile("MasterCtrl-AddNew Action(httpget)- Start");
                LoadDropDowns();
                ViewBag.SubCategories = new List<ItemMasterModalApril2026>();
                ItemMasterModalApril2026 model = new ItemMasterModalApril2026();
                var supplierList = SupplierMasterBalApr.GetAllSupplier()
                                   ?? new List<SupplierMasterModelApril2026>();
                ViewBag.SupplierCode = new SelectList(supplierList, "SupplierID", "SupplierCode");
                ViewBag.SupplierName = new SelectList(supplierList, "SupplierID", "SupplierName");
                ViewBag.SupplierData = supplierList;
                var CustList = customerBal.GetAllCustomer()
                               ?? new List<CustomerMasterModelApril2026>();
                ViewBag.CustomerCode = new SelectList(CustList, "Id", "CustCode");
                ViewBag.CustomerName = new SelectList(CustList, "Id", "CustName");

                return PartialView("_AddNewItem17Dec", model);
            }
            catch (Exception ex)
            {
                // This will show you the REAL error
                return Content("ERROR: " + ex.Message + " | " + ex.StackTrace);
            }
        }
        //[HttpGet]
        //public ActionResult AddNewItem17Dec()
        //{
        //    LogInfo.LogDetailsToFile("MasterCtrl-AddNew Action(httpget)- Start");

        //    LoadDropDowns();

        //    ViewBag.SubCategories = new List<ItemMasterModalApril2026>();
        //    LogInfo.LogDetailsToFile("AddNew - SubCategoriess Load");

        //    ItemMasterModalApril2026 model = new ItemMasterModalApril2026();

        //    var supplierList = SupplierMasterBalApr.GetAllSupplier();

        //    ViewBag.SupplierCode = new SelectList(supplierList, "SupplierID", "SupplierCode");
        //    ViewBag.SupplierName = new SelectList(supplierList, "SupplierID", "SupplierName");

        //    ViewBag.SupplierData = supplierList;

        //    var CustList = customerBal.GetAllCustomer();

        //    ViewBag.CustomerCode = new SelectList(CustList, "Id", "CustCode");
        //    ViewBag.CustomerName = new SelectList(CustList, "Id", "CustName");


        //    LogInfo.LogDetailsToFile("MasterCtrl-AddNew Action(httpget)- Start-End");

        //    return PartialView("_AddNewItem17Dec", model);
        //}

        [HttpPost]
        public ActionResult AddNewItem17Dec(ItemMasterModalApril2026 model)
        {
            LogInfo.LogDetailsToFile("MasterCtrl-AddNew Action(httppost)- Start");

            if (ModelState.IsValid)
            {
                // Item Save Action Call 
                string ItemCode = bulkbal.InsertItemMaster(model);

                return Json(new { success = true, code = ItemCode, message = "Item Saved Successfully" });
            }

            return Json(new { success = false, message = "Duplicate Name Not Allowed" });
        }


        [HttpGet]
        public ActionResult EditItem17Dec(int id)
        {
            LogInfo.LogDetailsToFile("MasterCtrl-Edit Action(httpget)- Start");

            ItemMasterModalApril2026 model = bulkbal.GetItemById(id);

            LoadDropDowns();

            ViewBag.SubCategories = bulkbal.GetSubCategoriesDetails(model.CategoryId);

            //ItemMaster_Modal_AprBtch model = new ItemMaster_Modal_AprBtch();

            var supplierList = SupplierMasterBalApr.GetAllSupplier();

            ViewBag.SupplierCode = new SelectList(supplierList, "SupplierID", "SupplierCode");
            ViewBag.SupplierName = new SelectList(supplierList, "SupplierID", "SupplierName");

            ViewBag.SupplierData = supplierList;

            var CustList = customerBal.GetAllCustomer();

            ViewBag.CustomerCode = new SelectList(CustList, "Id", "CustCode");
            ViewBag.CustomerName = new SelectList(CustList, "Id", "CustName");

            LogInfo.LogDetailsToFile("MasterCtrl-Edit Action(httpget)- Start-End");

            return PartialView("_EditItem17Dec", model);
        }

        [HttpPost]
        public ActionResult EditItem17Dec(int id, ItemMasterModalApril2026 model)
        {
            LogInfo.LogDetailsToFile("MasterCtrl-Edit Action(httppost)- Start");


            if (ModelState.IsValid)
            {
                LogInfo.LogDetailsToFile("MasterCtrl-Edit Action(httppost)-try-if- Start");

                int result = bulkbal.UpdateItemMaster(id, model);


                if (result > 0)
                {
                    LogInfo.LogDetailsToFile("MasterCtrl-Edit Action(httppost)-try-if-if Start- End");

                    return Json(new { success = true, message = "Item Updated Successfully" });
                }
            }

            LoadDropDowns();

            ViewBag.SubCategories = bulkbal.GetSubCategoriesDetails(model.CategoryId);

            var supplierList = SupplierMasterBalApr.GetAllSupplier();

            ViewBag.SupplierCode = new SelectList(supplierList, "SupplierID", "SupplierCode");
            ViewBag.SupplierName = new SelectList(supplierList, "SupplierID", "SupplierName");

            ViewBag.SupplierData = supplierList;

            var CustList = customerBal.GetAllCustomer();

            ViewBag.CustomerCode = new SelectList(CustList, "Id", "CustCode");
            ViewBag.CustomerName = new SelectList(CustList, "Id", "CustName");

            LogInfo.LogDetailsToFile("MasterCtrl-Edit Action(httppost)- Start-End");

            var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();


            return PartialView("_EditItem17Dec", model);

        }

        private void LoadDropDowns()
        {
            LogInfo.LogDetailsToFile("MasterCtrl-Item -LoadDropDowns Action- Start");

            ViewBag.Manufacturers = bulkbal.GetManufacturerDetails();

            ViewBag.Categories = bulkbal.GetCategoriesDetails();

            ViewBag.ColourList = bulkbal.GetAllColours();

            ViewBag.UnitList = bulkbal.GetUnitDetails();

            LogInfo.LogDetailsToFile("MasterCtrl- Item -LoadDropDowns Action- Start-End");

        }

        ///////////////////////////////////////////////////////////////////////////////
        //Supplier Master

        SupplierMasterBalApril2026 suppliermaster_Bal = new SupplierMasterBalApril2026();

        private void BindSuppDrp()
        {
            LogInfo.LogDetailsToFile("MasterCtrl- Supplier -BindSuppDrp Action- Start");

            var supplierList = SupplierMasterBalApr.GetAllSupplier();
            ViewBag.SupplierCode = new SelectList(supplierList, "SupplierID", "SupplierCode");
            ViewBag.SupplierName = new SelectList(supplierList, "SupplierID", "SupplierName");
            LogInfo.LogDetailsToFile("MasterCtrl- Supplier -BindSuppDrp Action- Start-End");

        }

        public ActionResult SupplierMaster()
        {
            LogInfo.LogDetailsToFile("MasterCtrl- Supplier -SupplierMaster Action- Start");

            SupplierMasterModelApril2026 model = new SupplierMasterModelApril2026();

            BindSuppDrp();
            LogInfo.LogDetailsToFile("MasterCtrl- Supplier -SupplierMaster Action- Start-End");

            return View(model);
        }

        public ActionResult SuppList()
        {
            LogInfo.LogDetailsToFile("SupplierController- SuppList Action- Start");
            var result = View(SupplierMasterBalApr.GetAllSupplier());
            LogInfo.LogDetailsToFile("SupplierController- SuppList Action- Start-End");
            return result;
        }

        public ActionResult AddNewSupplier()
        {
            LogInfo.LogDetailsToFile("SupplierController- AddNewSupplier Action- Start");
            BindDropdown();
            var result = PartialView("_AddNewSupplier");
            LogInfo.LogDetailsToFile("SupplierController- AddNewSupplier Action- Start-End");
            return result;
        }

        [HttpPost]
        public ActionResult AddNewSupplier(SupplierMasterModelApril2026 model)
        {
            LogInfo.LogDetailsToFile("SupplierController- AddNewSupplier POST Action- Start");
            if (ModelState.IsValid)
            {
                string code = SupplierMasterBalApr.InsertSupplier(model);

                LogInfo.LogDetailsToFile("SupplierController- AddNewSupplier POST Action- Start-Success");
                return Json(new { success = true, code, Message = "Supplier Save Successfully" });
            }
            LogInfo.LogDetailsToFile("SupplierController- AddNewSupplier POST Action- Start-End");
            return PartialView("_AddNewSupplier", model);
        }

        public ActionResult EditSupplier(int id)
        {
            LogInfo.LogDetailsToFile("SupplierController- EditSupplier Action- Start");
            BindDropdown();
            var result = PartialView("_EditSupplier", SupplierMasterBalApr.GetByIdSupplier(id));
            LogInfo.LogDetailsToFile("SupplierController- EditSupplier Action- Start-End");
            return result;
        }

        [HttpPost]
        public ActionResult EditSupplier(int id, SupplierMasterModelApril2026 model)
        {
            LogInfo.LogDetailsToFile("SupplierController- EditSupplier POST Action- Start");
            if (ModelState.IsValid)
            {
                suppliermaster_Bal.UpdateSupplier(id, model);

                LogInfo.LogDetailsToFile("SupplierController- EditSupplier POST Action- Start-Success");
                return Json(new { success = true, Message = "Supplier Update Successfully" });
            }
            LogInfo.LogDetailsToFile("SupplierController- EditSupplier POST Action- Start-End");
            return PartialView("_EditSupplier", model);
        }
        /// //////////////////////////////////////////////////////////////////////////////


        CustomerMasterBALApril2026 customerBal = new CustomerMasterBALApril2026();


        private void BindDropdown()
        {
            LogInfo.LogDetailsToFile("MasterCtrl- Customer -BindDrp Action- Start");

            ViewBag.CustomerList = new SelectList(customerBal.GetAllCustomer(), "Id", "CustName");

            ViewBag.BranchList = new SelectList(customerBal.GetBranch(), "BranchId", "BranchName");

            ViewBag.CountryList = new SelectList(customerBal.GetCountries(), "CountryId", "Country");

            ViewBag.StateList = new SelectList(customerBal.GetStates(), "StateId", "StateName");

            ViewBag.CityList = new SelectList(customerBal.GetCities(), "CityId", "City");

            LogInfo.LogDetailsToFile("MasterCtrl- Customer -BindDrp Action- Start-End");

        }

        public ActionResult CustomerListApril2026()
        {
            LogInfo.LogDetailsToFile("MasterCtrl- Customer -CustomerListApril2026 Action- Start");

            BindDropdown();
            CustomerMasterModelApril2026 model = new CustomerMasterModelApril2026();

            LogInfo.LogDetailsToFile("MasterCtrl- Customer -CustomerListApril2026 Action- Start-End");

            return View(model);
        }
        
        [HttpGet]
        public JsonResult SearchCustomer(int id)
        {
            LogInfo.LogDetailsToFile("MasterCtrl- Customer -Search Action");

            return Json(customerBal.GetById(id), JsonRequestBehavior.AllowGet);
        }
        

        //CustomerMasterBALApril2026 CustomerMasterBalApr = new CustomerMasterBALApril2026();
        

        [HttpPost]
        public JsonResult InsertCustomerMaster(CustomerMasterModelApril2026 model)
        {
            LogInfo.LogDetailsToFile("MasterCtrl- Customer -Save Action");
            string CustCode = customerBal.InsertCustomerMaster(model);
            return Json(new { success = true, code = CustCode, message = "Customer Save Succesfully" });
        }

        [HttpPost]
        public JsonResult UpdateCustomerMaster(CustomerMasterModelApril2026 model)
        {
            LogInfo.LogDetailsToFile("MasterCtrl- Customer -Update Action");

            return Json(customerBal.UpdateCustomerMaster(model.Id, model), JsonRequestBehavior.AllowGet);
        }

        public ActionResult CustList()
        {
            LogInfo.LogDetailsToFile("CustomerController- CustList Action- Start");
            var result = View(customerBal.GetAllCustomer());
            LogInfo.LogDetailsToFile("CustomerController- CustList Action- Start-End");
            return result;
        }

        public ActionResult AddNewCustomer()
        {
            LogInfo.LogDetailsToFile("CustomerController- AddNewCustomer Action- Start");
            BindDropdown();
            var result = PartialView("_AddNewCustomer");
            LogInfo.LogDetailsToFile("CustomerController- AddNewCustomer Action- Start-End");
            return result;
        }

        [HttpPost]
        public ActionResult AddNewCustomer(CustomerMasterModelApril2026 model)
        {
            LogInfo.LogDetailsToFile("CustomerController- AddNewCustomer POST Action- Start");
            if (ModelState.IsValid)
            {
                string code = customerBal.InsertCustomerMaster(model);
                LogInfo.LogDetailsToFile("CustomerController- AddNewCustomer POST Action- Start-Success");
                return Json(new { success = true, code, Messages = "Customer Save Succesfully" });
            }

            LogInfo.LogDetailsToFile("CustomerController- AddNewCustomer POST Action- Start-End");
            return PartialView("_AddNewCustomer", model);
        }

        [HttpGet]
        public ActionResult EditCustomer(int id)
        {
            LogInfo.LogDetailsToFile("CustomerController- EditCustomer Action- Start");
            BindDropdown();

            var result = PartialView("_EditCustomer", customerBal.GetById(id));
            LogInfo.LogDetailsToFile("CustomerController- EditCustomer Action- Start-End");
            return result;
        }

        [HttpPost]
        public ActionResult EditCustomer(int id, CustomerMasterModelApril2026 model)
        {
            LogInfo.LogDetailsToFile("CustomerController- EditCustomer POST Action- Start");
            if (ModelState.IsValid)
            {
                customerBal.UpdateCustomerMaster(id, model);
                LogInfo.LogDetailsToFile("CustomerController- EditCustomer POST Action- Start-Success");
                return Json(new { success = true, Messages = "Customer Save Succesfully" });
            }

            LogInfo.LogDetailsToFile("CustomerController- EditCustomer POST Action- Start-End");
            return PartialView("_EditCustomer", model);
        }

        //****************************************************************************************************************
        //*****************************UploadItemMasterExcelApr26*************************************************************
        //**********************************************************************************************************

        //BulckUploadBALApril2026 Bulk = new BulckUploadBALApril2026();


        //[HttpGet]
        //public ActionResult UploadItemMasterExcelApr26()
        //{
        //    ViewBag.SuccessCount = 0;
        //    ViewBag.FailCount = 0;
        //    return View();
        //}


        //[HttpPost]
        //public ActionResult UploadItemMasterExcelApr26(string MasterType, HttpPostedFileBase ExcelFile)
        //{
        //    int successCount = 0;
        //    int failCount = 0;
        //    int uploadId = 0;

        //    if (string.IsNullOrEmpty(MasterType))
        //    {
        //        ViewBag.Message = "Please select Master Type.";
        //        return View();
        //    }

        //    if (ExcelFile == null || ExcelFile.ContentLength == 0)
        //    {
        //        ViewBag.Message = "Please attach Excel file.";
        //        return View();
        //    }

        //    string folderPath = Server.MapPath("~/Uploads/");

        //    if (!Directory.Exists(folderPath))
        //        Directory.CreateDirectory(folderPath);

        //    string filePath = Path.Combine(
        //        folderPath,
        //        Path.GetFileName(ExcelFile.FileName)
        //    );

        //    ExcelFile.SaveAs(filePath);

        //    DataTable dt = new DataTable();

        //    using (var stream = System.IO.File.Open(
        //        filePath,
        //        FileMode.Open,
        //        FileAccess.Read))
        //    {
        //        using (var reader = ExcelDataReader.ExcelReaderFactory.CreateReader(stream))
        //        {
        //            var result = reader.AsDataSet(
        //                new ExcelDataSetConfiguration()
        //                {
        //                    ConfigureDataTable = (_) =>
        //                        new ExcelDataTableConfiguration()
        //                        {
        //                            UseHeaderRow = true
        //                        }
        //                });

        //            dt = result.Tables[0];
        //        }
        //    }
        //    ///////////// // 1. CREATE UPLOAD ID/////////////////////////////////
        //    uploadId = Bulk.InsertUploadMaster(ExcelFile.FileName);
        //   // uploadId = Bulk.InsertUploadMasterIMApr26(ExcelFile.FileName, MasterType);

        //    // 2. ADD UploadId COLUMN ///////////////

        //    if (!dt.Columns.Contains("UploadId"))
        //    {
        //        dt.Columns.Add("UploadId", typeof(int));
        //    }

        //    foreach (DataRow row in dt.Rows)
        //    {
        //        row["UploadId"] = uploadId;
        //    }

        //    int excelRowCount = dt.Rows.Count;


        //    ////////////// 3. MASTER TYPE WISE UPLOAD//////////////////


        //    //if (MasterType == "ItemMaster")
        //    //{
        //    //    // ----------------------------------------
        //    //    // ITEM MASTER
        //    //    // ----------------------------------------

        //    //    Bulk.BulkInsertItemMasterDummyExcel(dt);

        //    //    DataTable dtRes =
        //    //        Bulk.ValidateItemMasterUploadIMApr26(uploadId);

        //    //    if (dtRes.Rows.Count > 0)
        //    //    {
        //    //        successCount =
        //    //            Convert.ToInt32(dtRes.Rows[0]["SuccessCount"]);

        //    //        failCount =
        //    //            Convert.ToInt32(dtRes.Rows[0]["FailCount"]);
        //    //    }
        //    //}
        //    //else if (MasterType == "SuplierMaster")
        //    //{
        //    //    // ----------------------------------------
        //    //    // SUPPLIER MASTER
        //    //    // ----------------------------------------

        //    //    bulkUploadBAL.BulkInsertSupplierMasterDummyExcel(dt);

        //    //    DataTable dtRes =Bulk.ValidateMasterUploadApr26(uploadId,MasterType);

        //    //    if (dtRes.Rows.Count > 0)
        //    //    {
        //    //        successCount =Convert.ToInt32(dtRes.Rows[0]["SuccessCount"]);

        //    //        failCount =
        //    //            Convert.ToInt32(dtRes.Rows[0]["FailCount"]);
        //    //    }
        //    //}
        //    //else if (MasterType == "CustomerMaster")
        //    //{
        //    //    // ----------------------------------------
        //    //    // CUSTOMER MASTER
        //    //    // ----------------------------------------

        //    //    bulkUploadBAL.BulkInsertCustomerMasterDummyExcel(dt);

        //    //    DataTable dtRes =
        //    //        Bulk.ValidateMasterUploadApr26(
        //    //            uploadId,
        //    //            MasterType
        //    //        );

        //    //    if (dtRes.Rows.Count > 0)
        //    //    {
        //    //        successCount =
        //    //            Convert.ToInt32(dtRes.Rows[0]["SuccessCount"]);

        //    //        failCount =
        //    //            Convert.ToInt32(dtRes.Rows[0]["FailCount"]);
        //    //    }
        //    //}
        //    //else
        //    //{
        //    //    ViewBag.Message = "Invalid Master Type.";
        //    //    return View();
        //    //}

        //    // ============================================
        //    // 4. SEND RESULT TO VIEW
        //    // ============================================

        //    ViewBag.SuccessCount = successCount;
        //    ViewBag.FailCount = failCount;

        //    Session["UploadId"] = uploadId;
        //    Session["MasterType"] = MasterType;

        //    return View();
        //}
        //public ActionResult DownloadFailItemMasterExcelApr26()
        //{
        //    int uploadId = 0;

        //    if (Session["UploadId"] != null)
        //    {
        //        uploadId = Convert.ToInt32(Session["UploadId"]);
        //    }

        //    if (uploadId == 0)
        //    {
        //        return Content("Upload ID not found. Please upload the Excel file first.");
        //    }

        //    string masterType = "";

        //    if (Session["MasterType"] != null)
        //    {
        //        masterType = Session["MasterType"].ToString();
        //    }

        //    if (string.IsNullOrEmpty(masterType))
        //    {
        //        return Content("Master Type not found. Please upload the Excel file first.");
        //    }

        //    DataTable dt = null;

        //    //////////// // ITEM MASTER///////////////////

        //    //if (masterType == "ItemMaster")
        //    //{
        //    //    dt = bulkUploadBAL.GetFailItemMasterExcelIMApr26(uploadId);
        //    //}
        //    //////// // SUPPLIER MASTER/////////////////////

        //    //else if (masterType == "SuplierMaster")
        //    //{
        //    //    dt = bulkUploadBAL.GetFailMasterExcelApr26(uploadId,masterType);
        //    //}
        //    ///////////  // CUSTOMER MASTER////////////////
        //    //else if (masterType == "CustomerMaster")
        //    //{
        //    //    dt = bulkUploadBAL.GetFailMasterExcelApr26(uploadId,masterType);
        //    //}

        //    //else
        //    //{
        //    //    return Content("Invalid Master Type.");
        //    //}

        //    // NO FAILED RECORD
        //    if (dt == null || dt.Rows.Count == 0)
        //    {
        //        return Content("No failed records found.");
        //    }


        //    //////// // CREATE EXCEL HTML////////////////////////
        //    StringBuilder sb = new StringBuilder();

        //    sb.Append("<html>");
        //    sb.Append("<head>");
        //    sb.Append("<meta charset='utf-8' />");
        //    sb.Append("</head>");
        //    sb.Append("<body>");

        //    sb.Append("<table border='1'>");

        //    // Header
        //    sb.Append("<tr>");

        //    foreach (DataColumn col in dt.Columns)
        //    {
        //        sb.Append("<th>");
        //        sb.Append(col.ColumnName);
        //        sb.Append("</th>");
        //    }

        //    sb.Append("</tr>");

        //    // Data
        //    foreach (DataRow row in dt.Rows)
        //    {
        //        sb.Append("<tr>");

        //        foreach (DataColumn col in dt.Columns)
        //        {
        //            sb.Append("<td>");

        //            sb.Append(row[col] == DBNull.Value? "": row[col].ToString());

        //            sb.Append("</td>");
        //        }

        //        sb.Append("</tr>");
        //    }

        //    sb.Append("</table>");

        //    sb.Append("</body>");
        //    sb.Append("</html>");

        //    byte[] bytes = Encoding.UTF8.GetBytes(sb.ToString());


        //    ////////////  // FILE NAME///////////////
        //    ///
        //    string fileName = "";

        //    if (masterType == "ItemMaster")
        //    {
        //        fileName = "Fail_ItemMaster.xls";
        //    }
        //    else if (masterType == "SupplierMaster")
        //    {
        //        fileName = "Fail_SupplierMaster.xls";
        //    }
        //    else if (masterType == "CustomerMaster")
        //    {
        //        fileName = "Fail_CustomerMaster.xls";
        //    }

        //    return File(bytes,"application/vnd.ms-excel",fileName);
        //}

        // =====================================================
        // ITEM MASTER BULK UPLOAD - GET
        // =====================================================

        BulckUploadBALApril2026 bulkUploadBAL = new BulckUploadBALApril2026();

        // =====================================================
        // GET: UPLOAD EXCEL
        // =====================================================
        [HttpGet]
        public ActionResult UploadItemMasterExcelApr26()
        {
            // LogInfo.LogDetailsToFile("MasterCtrl-UploadItemMasterExcelApr26 GET - Start");

            ViewBag.SuccessCount = 0;
            ViewBag.FailCount = 0;
            ViewBag.TableList = new SelectList(bulkUploadBAL.GetTableList(), "TableName", "DisplayName");

            // LogInfo.LogDetailsToFile("MasterCtrl-UploadItemMasterExcelApr26 GET - End");

            return View();
        }

        // =====================================================
        // POST: UPLOAD EXCEL (Dynamic Table Logic)
        // =====================================================
        [HttpPost]
       // [ValidateAntiForgeryToken]
        public ActionResult UploadItemMasterExcelApr26(string MasterType, HttpPostedFileBase ExcelFile)
        {
            ViewBag.SuccessCount = 0;
            ViewBag.FailCount = 0;
            ViewBag.TableList = new SelectList(bulkUploadBAL.GetTableList(), "TableName", "DisplayName");

            try
            {
                if (string.IsNullOrEmpty(MasterType))
                {
                    ViewBag.Message = "Please select a Master Table.";
                    return View();
                }

                if (ExcelFile == null || ExcelFile.ContentLength == 0)
                {
                    ViewBag.Message = "Please attach Excel file.";
                    return View();
                }

                string extension = Path.GetExtension(ExcelFile.FileName)?.ToLower();
                if (extension != ".xls" && extension != ".xlsx")
                {
                    ViewBag.Message = "Please upload a valid Excel file.";
                    return View();
                }

                DataTable dt;
                using (var reader = ExcelReaderFactory.CreateReader(ExcelFile.InputStream))
                {
                    dt = reader.AsDataSet(new ExcelDataSetConfiguration
                    {
                        ConfigureDataTable = (_) => new ExcelDataTableConfiguration { UseHeaderRow = true }
                    }).Tables[0];
                }

                if (dt == null || dt.Rows.Count == 0)
                {
                    ViewBag.Message = "Uploaded Excel file has no records.";
                    return View();
                }

                int uploadId = bulkUploadBAL.InsertUploadMaster(Path.GetFileName(ExcelFile.FileName), MasterType);
                if (uploadId <= 0)
                {
                    ViewBag.Message = "Unable to create upload record.";
                    return View();
                }

                DataTable result = null;

                // Handles Item, Customer, and Supplier dynamically
                switch (MasterType)
                {
                    case "ItemMaster":
                        bulkUploadBAL.BulkInsertItemMaster(dt, uploadId);
                        result = bulkUploadBAL.ValidateItemMasterUpload(uploadId);
                        break;

                    case "CustomerMaster":
                        bulkUploadBAL.BulkInsertCustomerMaster(dt, uploadId);
                        result = bulkUploadBAL.ValidateCustomerMasterUpload(uploadId);
                        break;

                    case "SupplierMaster":
                        bulkUploadBAL.BulkInsertSupplierMaster(dt, uploadId);
                        result = bulkUploadBAL.ValidateSupplierMasterUpload(uploadId);
                        break;

                    default:
                        ViewBag.Message = "Selected table processing is not configured.";
                        return View();
                }

                if (result != null && result.Rows.Count > 0)
                {
                    ViewBag.SuccessCount = Convert.ToInt32(result.Rows[0]["SuccessCount"]);
                    ViewBag.FailCount = Convert.ToInt32(result.Rows[0]["FailCount"]);
                }

                Session["UploadId"] = uploadId;
                Session["MasterType"] = MasterType;
                ViewBag.Message = "Excel upload processing completed.";
            }
            catch (Exception ex)
            {
                // LogInfo.LogDetailsToFile("UploadItemMasterExcelApr26 Exception: " + ex.ToString());
                ViewBag.Message = "Upload failed. Check the application log for details.";
            }

            return View();
        }


        // =====================================================
        // DOWNLOAD FAILED RECORDS (Using ClosedXML)
        // =====================================================
        [HttpGet]
        public ActionResult DownloadFailItemMasterExcelApr26()
        {
            try
            {
                if (Session["MasterType"] == null || Session["UploadId"] == null)
                {
                    return Content("Session expired or upload details not found. Please upload the Excel file again.");
                }

                string masterType = Session["MasterType"].ToString();
                int currentUploadId = Convert.ToInt32(Session["UploadId"]);

                DataTable dtFail = null;

                switch (masterType)
                {
                    case "ItemMaster":
                        dtFail = bulkUploadBAL.GetFailedItemMasterRecords(currentUploadId);
                        break;
                    case "CustomerMaster":
                        dtFail = bulkUploadBAL.GetFailedCustomerMasterRecords(currentUploadId);
                        break;
                    case "SupplierMaster":
                        dtFail = bulkUploadBAL.GetFailedSupplierMasterRecords(currentUploadId);
                        break;
                    default:
                        return Content("Invalid table selected.");
                }

                if (dtFail == null || dtFail.Rows.Count == 0)
                {
                    return Content("No failed records found for this upload.");
                }

                if (dtFail.Columns.Contains("Id")) dtFail.Columns.Remove("Id");
                if (dtFail.Columns.Contains("UploadId")) dtFail.Columns.Remove("UploadId");
                if (dtFail.Columns.Contains("IsValid")) dtFail.Columns.Remove("IsValid");

                using (XLWorkbook wb = new XLWorkbook())
                {
                    var ws = wb.Worksheets.Add(dtFail, "Failed_Records");

                    ws.Row(1).Style.Font.Bold = true;
                    ws.Row(1).Style.Fill.BackgroundColor = XLColor.LightGray;
                    ws.Columns().AdjustToContents();

                    using (MemoryStream stream = new MemoryStream())
                    {
                        wb.SaveAs(stream);
                        string fileName = $"{masterType}_Failed_Records_{DateTime.Now:yyyyMMddHHmmss}.xlsx";

                        return File(
                            stream.ToArray(),
                            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                            fileName
                        );
                    }
                }
            }
            catch (Exception ex)
            {
                // LogInfo.LogDetailsToFile("DownloadFailItemMasterExcelApr26 Exception: " + ex.Message);
                return Content("Failed-record download failed. Please check the application log.");
            }
        }


        // =====================================================
        // DOWNLOAD TEMPLATE (MISSING METHOD ADDED HERE)
        // =====================================================
        [HttpGet]
        public ActionResult DownloadTemplate(string MasterType)
        {
            Type modelType = null;

            // Select the appropriate Model based on the Dropdown selection
            switch (MasterType)
            {
                case "ItemMaster":
                    modelType = typeof(ItemMasterModalApril2026); // Ensure this model exists
                    break;
                case "CustomerMaster":
                    modelType = typeof(CustomerMasterModelApril2026); // Ensure this model exists
                    break;
                case "SupplierMaster":
                    modelType = typeof(SupplierMasterModelApril2026); // Ensure this model exists
                    break;
                default:
                    return Content("<script>alert('Template not configured for selected table.');history.back();</script>");
            }

            using (XLWorkbook wb = new XLWorkbook())
            {
                var ws = wb.Worksheets.Add("Template");
                var properties = modelType.GetProperties();

                int columnIndex = 1;

                foreach (var prop in properties)
                {
                    var scaffoldAttr = prop.GetCustomAttribute<ScaffoldColumnAttribute>();
                    if (scaffoldAttr != null && !scaffoldAttr.Scaffold)
                    {
                        continue;
                    }

                    var displayAttr = prop.GetCustomAttribute<DisplayAttribute>();
                    string headerName = displayAttr != null ? displayAttr.Name : prop.Name;

                    var cell = ws.Cell(1, columnIndex);
                    cell.Value = headerName;
                    cell.Style.Font.Bold = true;
                    cell.Style.Fill.BackgroundColor = XLColor.LightGray;

                    columnIndex++;
                }

                ws.Columns().AdjustToContents();

                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    string fileName = MasterType.Replace("_DummyExcel", "").Replace("Dummy", "") + "_Template.xlsx";

                    return File(
                        stream.ToArray(),
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        fileName
                    );
                }
            }
        }
    }
}
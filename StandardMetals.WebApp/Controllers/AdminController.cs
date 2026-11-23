using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StandardMetals.MODEL;
using System.Net.Http;
using System.Configuration;
using Newtonsoft.Json;


namespace StandardMetals.WebApp.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult LoginUserMaster()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LoginUserMaster(LoginUserMasterModel logindtl)
        {
            return View();
        }

        public ActionResult MenuMaster()
        {
            return View();
        }

        //[HttpPost]
        //public ActionResult MenuMaster()
        //{
        //    return View();
        //}

        public ActionResult SubMenuMaster()
        {
            return View();
        }
        //[HttpPost]
        //public ActionResult SubMenuMaster()
        //{
        //    return View();
        //}

        public ActionResult RoleMaster()
        {
            return View();
        }

        //public ActionResult RoleMaster()
        //{
        //    return View();
        //}

        public ActionResult LoggedInUsers()
        {
            string apiUrl = ConfigurationManager.AppSettings["APIURL"] + "Admin/GetAllLoginUsers";

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = client.GetAsync(apiUrl).Result;

                if (response.IsSuccessStatusCode)
                {
                    string json = response.Content.ReadAsStringAsync().Result;

                    List<LoggedInUserModelApril26> users = JsonConvert.DeserializeObject<List<LoggedInUserModelApril26>>(json);

                    return View(users);
                }
            }

            return View(new List<LoggedInUserModelApril26>());
        }


        [HttpPost]
        public ActionResult LogoutUser(int LoginUserId)
        {
            string apiUrl = ConfigurationManager.AppSettings["APIURL"] + "Admin/LogoutLoginUser?LoginUserId=" + LoginUserId;

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = client.GetAsync(apiUrl).Result;

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("LoggedInUsers");
                }
            }

            return RedirectToAction("LoggedInUsers");
        }

        [HttpPost]
        public ActionResult LogoutLoginUser(int LoginUserId)
        {
            string apiUrl = ConfigurationManager.AppSettings["APIURL"] + "Admin/LogoutLoginUser?LoginUserId=" + LoginUserId;

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = client.GetAsync(apiUrl).Result;
            }

            return RedirectToAction("LoggedInUsers");
        }
    }
}
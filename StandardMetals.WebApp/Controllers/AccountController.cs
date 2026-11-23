using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;
using StandardMetals.MODEL;
using System.Threading.Tasks;
using StandardMetals.BAL;
using StandardMetals.CommonLibrary;

namespace StandardMetals.WebApp.Controllers
{
    public class AccountController : Controller
    {
        LoginDetails menuSubMenu = new LoginDetails();


        // Login Page

        public ActionResult Login()
        {
            return View();
        }


        // Login

        [HttpPost]
        public async Task<ActionResult> Login(LoginMaster Logindtl)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    LoginMaster objLoginDtl = new LoginMaster();

                    string apiUrl = "http://localhost:62335/LoginDetail/LoginDetails?UserName=" + Logindtl.UserName.ToString() + "&" + "Password=" + Logindtl.Password.ToString();

                    using (HttpClient client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(apiUrl);

                        client.DefaultRequestHeaders.Accept.Clear();

                        client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                        HttpResponseMessage response = await client.GetAsync(apiUrl);

                        if (response.IsSuccessStatusCode)
                        {
                            var data = await response.Content.ReadAsStringAsync();

                            objLoginDtl = Newtonsoft.Json.JsonConvert.DeserializeObject<LoginMaster>(data);
                        }
                    }


                    if (objLoginDtl != null && objLoginDtl.LoginStatus == "SUCCESS")
                    {
                        Session["Username"] = objLoginDtl.UserName;

                        Session["Branch"] = objLoginDtl.BranchName;

                        Session["BranchId"] = objLoginDtl.BranchId;

                        Session["EmpCode"] = objLoginDtl.EmpCode;

                        Session["LoginUserId"] = objLoginDtl.LoginUserId;


                        // Got user login id from Model

                        int LoginUserId = objLoginDtl.LoginUserId;

                        List<LoginDetailsModel> MenuSubMenu = menuSubMenu.GetMenuDetail(LoginUserId);

                        Session["MenuSubMenu"] = MenuSubMenu;


                        var palneTextByte = System.Text.Encoding.UTF8.GetBytes(Logindtl.UserName + ":" + Logindtl.Password);

                        string token = System.Convert.ToBase64String(palneTextByte);

                        Session["Token"] = token;


                        return RedirectToAction("DefaultDashboard", "Master");
                    }
                    else if (objLoginDtl != null && objLoginDtl.LoginStatus == "ALREADY_LOGGED_IN")
                    {
                        ViewBag.ErroMsg = "User is already logged in.";
                    }
                    else
                    {
                        ViewBag.ErroMsg = "User name or password is incorrect...";
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.ErroMsg = "An error occurred while processing login.";
                }
            }

            return View();
        }


        public async Task<ActionResult> SignOut()
        {
            try
            {
                if (Session["LoginUserId"] != null)
                {
                    int LoginUserId = Convert.ToInt32(Session["LoginUserId"]);

                    string apiUrl = "http://localhost:62335/LoginDetail/LogoutUser?LoginUserId=" + LoginUserId;

                    using (HttpClient client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(apiUrl);

                        HttpResponseMessage response = await client.GetAsync(apiUrl);
                    }
                }

                Session.Clear();
                Session.Abandon();

                return RedirectToAction("Login", "Account");
            }
            catch (Exception)
            {
                Session.Clear();
                Session.Abandon();

                return RedirectToAction("Login", "Account");
            }
        }


        // Exception Logging

        protected override void OnException(ExceptionContext filterContext)
        {
            Exception ex = filterContext.Exception;
        }

    }
}
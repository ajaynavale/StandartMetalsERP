using StandardMetals.CommonLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace StandardMetals.WebApp
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }


        protected void Session_End(object sender, EventArgs e)
        {
            if (Session["LoginUserId"] != null)
            {
                int loginUserId = Convert.ToInt32(Session["LoginUserId"]);
                // call your LogoutUser API here (synchronously, since session is ending)
            }
        }

        protected void Application_Error()
        {
            Exception ex = Server.GetLastError();
            LogInfo.LogDetailsToFile("Global Application Error", ex);

            LogInfo.LoginDetailToDB("Global Application Error", ex);
        }
    }
}

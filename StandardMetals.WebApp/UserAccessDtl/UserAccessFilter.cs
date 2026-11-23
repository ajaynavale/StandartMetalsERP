using StandardMetals.MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StandardMetals.WebApp.UserAccessDtl
{
    public class UserAccessFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            int str = Convert.ToInt32(System.Web.HttpContext.Current.Session["LoginUserId"].ToString());

            List<LoginDetailsModel> MenuSubMenu = (List<LoginDetailsModel>)System.Web.HttpContext.Current.Session["MenuSubMenu"];

            string controllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            string actionName = filterContext.ActionDescriptor.ActionName;

            string strUrl = "../" + controllerName + "/" + actionName;

            var masterMenu = MenuSubMenu.Select(m => new { m.MenuLink }).Distinct().Where(m => m.MenuLink == strUrl);

            int cnt = 0;
            
            foreach (var val in masterMenu)
            {
               
                cnt++;
                break;

            }

            if (strUrl == "../Master/DefaultDashboard")
            {
                cnt = 1;
            }

            if (cnt == 0)
            {
                //System.Web.HttpContext.Current.Session.Abandon();
                //  System.Web.HttpContext.Current.Session.Clear();


                filterContext.Result = new ViewResult()
                {
                    ViewName = "Error_UserAccess"
                };
            }

        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Data;
using System.Data.SqlClient;
using StandardMetals.BAL;
using StandardMetals.MODEL;
using StandardMetals.DAL;

namespace StandardMetals.WebAPI.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class AdminController : ApiController
    {
        //Bind Menu Master Dropdown 

        RoleMasterBal roleMasterBal = new RoleMasterBal();
        MenuMasterBal bal = new MenuMasterBal();
        MenuMasterModel model = new MenuMasterModel();
        LoginDetails loginDetailsBal = new LoginDetails();

        [HttpGet]
        public DataTable BindMasterMenu()
        {
            DataTable dt = new DataTable();
            dt = bal.FillMenuItem();
            return dt;
        }

        [HttpPost]
        public void SaveMasterMenu(MenuMasterModel masterModel)
        {
            int affrows = 0;
            string status = " ";
            //masterModel.BranchName = (System.Web.HttpContext.Current.Session["Branch"].ToString());
            affrows = bal.SaveMenuData(masterModel);
            //masterModel.BranchName = 


            if (affrows > 0)
            {
                status = "Data is saved";
            }
            else
            {
                status = "Data is not saved";

            }
        }

        [HttpPost]
        public int SaveSubMenuMaster(SubMenuModel subMenuModel)
        {
            int affrows = 0;
            string status = " ";
            //masterModel.BranchName = (System.Web.HttpContext.Current.Session["Branch"].ToString());
            affrows = bal.SaveSubMenuData(subMenuModel);
            //masterModel.BranchName = 
            if (affrows > 0)
            {
                status = "Data is saved";
            }
            else
            {
                status = "Data is not saved";

            }

            return affrows;
        }

        [HttpGet]
        public DataTable BindRoleMasterItem()
        {
            DataTable dt = new DataTable();
            dt = bal.FillRoleMasterItem();
            return dt;
        }

        public List<LoginDetailsModel> GetMenuDetail()
        {
            List<LoginDetailsModel> menuList = new List<LoginDetailsModel>();
            menuList = roleMasterBal.GetMenuDetail();

            return menuList;
        }


        [HttpGet]
        public List<LoggedInUserModelApril26> GetAllLoginUsers()
        {
            return loginDetailsBal.GetAllLoginUsers();
        }

        [HttpGet]
        public IHttpActionResult LogoutLoginUser(int LoginUserId)
        {
            loginDetailsBal.LogoutUser(LoginUserId);

            return Ok();
        }
    }
}
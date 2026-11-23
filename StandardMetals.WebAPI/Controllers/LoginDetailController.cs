using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using StandardMetals.BAL;
using StandardMetals.MODEL;
using StandardMetals.WebAPI.AuthFilter;

namespace StandardMetals.WebAPI.Controllers
{
    public class LoginDetailController : ApiController
    {
        LoginDetails bal = new LoginDetails();

        [HttpGet]
        //[BasicAuthentication]
        public LoginMaster LoginDetails(string UserName, string Password)
        {
            return bal.LoginDetailsvalue(UserName, Password);
        }


        [HttpGet]
        public IHttpActionResult LogoutUser(int LoginUserId)
        {
            bal.LogoutUser(LoginUserId);

            return Ok();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace OMS.WEBAPI.Controllers
{
    public class BaseController : ApiController
    {
        public BaseController()
        {
            //var Claimproperties = Request.GetOwinContext().Authentication.User.Claims;
            //foreach (var item in Claimproperties)
            //{
            //    //if(item.Properties == ClaimTypes.Name)
            //}
        }
    }
}
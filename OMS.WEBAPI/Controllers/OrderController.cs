using System;
using System.Collections.Generic;
using System.Linq;

using OMS.Entities;
using OMS.Business;
using System.Security.Claims;
using System.Web.Http;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.Owin.Security;
using System.Threading.Tasks;

namespace OMS.WEBAPI.Controllers
{
    [Authorize(Roles = "Admin,User")]
    //[RoutePrefix("api/Order")]
    public class OrderController : ApiController
    {
        private readonly OrderBLL orderBll = new OrderBLL();

        //public OrderController(IOrderBLL OrderBll)
        //{
        //    orderBll = new OrderBLL();
        //    //  var get = (ClaimsPrincipal) Request.GetOwinContext().Authentication.User.Identity;
        //}
        private IAuthenticationManager Authentication
        {
            get { return Request.GetOwinContext().Authentication; }
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetAllProducts()
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var result = await orderBll.GetAllProducts();
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        [HttpPost]
        public async Task<IHttpActionResult> SaveProductInfo(Product product)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                return Ok(await orderBll.SaveProductInfo(product));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [Route("GetOrders")]
        public async Task<IHttpActionResult> GetAllOrdersByUserID()
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                string UserID=string.Empty;
                var Claimproperties = Request.GetOwinContext().Authentication.User.Claims;
                foreach (var item in Claimproperties)
                {
                    if (item.Type == "UserID")
                    {
                        UserID = item.Value;
                    }
                }
                return Ok(await this.orderBll.GetAllOrdersByUserID(Convert.ToInt32(UserID)));
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        [Route("SaveOrder")]
        public async Task<IHttpActionResult> SaveOrderByUserID(OrderDetails orderDetails)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                string UserID = string.Empty;
                var Claimproperties = Request.GetOwinContext().Authentication.User.Claims;
                foreach (var item in Claimproperties)
                {
                    if (item.Type == "UserID")
                    {
                        UserID = item.Value;
                    }
                }
                if (orderDetails != null)
                    orderDetails.UserID = Convert.ToInt32(UserID);
                return Ok(await this.orderBll.SaveOrderByUserID(orderDetails));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

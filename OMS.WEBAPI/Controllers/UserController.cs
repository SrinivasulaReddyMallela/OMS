using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using OMS.Business;
using OMS.Entities;

namespace OMS.WEBAPI.Controllers
{
    //[RoutePrefix("api/User")]
    [Authorize(Roles="Admin,User")]
    public class UserController : ApiController
    {
        private readonly UserBLL userBLL = new UserBLL();
        //[Route("CreateUser")]
        [HttpPost]
        public async Task<IHttpActionResult> CreateUser(Login login)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                return Ok(await this.userBLL.UserCreation(login));                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [Route("CreateRole")]
        [HttpPost]
        public async Task<IHttpActionResult> CreateRole(Login_Role Login_Role)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                return Ok(await this.userBLL.CreateRole(Login_Role));             
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [Route("CreateUserRole")]
        [HttpPost]
        public async Task<IHttpActionResult> CreateUserRole(Login_Role_User Login_Role_User)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                return Ok(await this.userBLL.CreateUserRole(Login_Role_User));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}

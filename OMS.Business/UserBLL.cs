using OMS.Data;
using OMS.Entities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace OMS.Business
{
    public class UserBLL
    {
        UserDLL userDLL;
        public UserBLL()
        {
            userDLL = new UserDLL();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <param name="Password"></param>
        /// <returns></returns>
        public async Task<Logins> GetUserLoginInformation(string username, string Password)
        {
            try
            {
                return await userDLL.GetUserLoginInformation(username, Password);
            }
            catch (Exception ex)
            {
                throw ex;
                return await Task.FromResult<Logins>(new Logins());
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        public async Task<Response> UserCreation(Login login)
        {
            try
            {
                return await userDLL.UserCreation(login);
            }
            catch (Exception ex)
            {
                throw ex;
                return await Task.FromResult<Response>(new Response());
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Login_Role"></param>
        /// <returns></returns>
        public async Task<Response> CreateRole(Login_Role Login_Role)
        {
            try
            {
                return await userDLL.CreateRole(Login_Role);
            }
            catch (Exception ex)
            {
                throw ex;
                return await Task.FromResult<Response>(new Response());
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Login_Role_User"></param>
        /// <returns></returns>
        public async Task<Response> CreateUserRole(Login_Role_User Login_Role_User)
        {
            try
            {
                return await userDLL.CreateUserRole(Login_Role_User);
            }
            catch (Exception ex)
            {
                throw ex;
                return await Task.FromResult<Response>(new Response());
            }
        }

    }
}

using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using OMS.Entities;

using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Common;
using System.Data;

namespace OMS.Data
{
    public class UserDLL
    {
        public static int DATABASE_CONNECTION_TIMEOUT = 0;
        public static string ConnectionString = "OMSConnectionString";
        /// <summary>
        /// To get login info
        /// </summary>
        /// <param name="username"></param>
        /// <param name="Password"></param>
        /// <returns></returns>
        public async Task<Logins> GetUserLoginInformation(string username, string Password)
        {
            Logins logins = new Logins();
            logins.LoginEntityList = new List<Login>();
            try
            {
                Database database;
                DatabaseProviderFactory factory = new DatabaseProviderFactory();
                database = factory.Create(ConnectionString);
                string storedProcedureName = "LoginByUsernamePassword";
                DbCommand dbCommand = database.GetStoredProcCommand(storedProcedureName);
                dbCommand.CommandTimeout = DATABASE_CONNECTION_TIMEOUT;
                database.AddInParameter(dbCommand, "@username", DbType.String, username);
                database.AddInParameter(dbCommand, "@password", DbType.String, Password);
                DataSet dsLoginBasicInfo = new DataSet("Logins");
                database.LoadDataSet(dbCommand, dsLoginBasicInfo, "Logins");

                if (dsLoginBasicInfo != null && dsLoginBasicInfo.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < dsLoginBasicInfo.Tables[0].Rows.Count; i++)
                    {
                        logins.LoginEntityList.Add(new Login
                        {
                            username = dsLoginBasicInfo.Tables[0].Rows[i]["username"].ToString(),
                            password = dsLoginBasicInfo.Tables[0].Rows[i]["password"].ToString(),
                            ID = Convert.ToInt32(dsLoginBasicInfo.Tables[0].Rows[i]["id"].ToString()),
                            LOGINROLEID = Convert.ToInt32(dsLoginBasicInfo.Tables[0].Rows[i]["LOGINROLEID"].ToString()),
                            ROLENAME = dsLoginBasicInfo.Tables[0].Rows[i]["ROLENAME"].ToString(),
                        });
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return await Task.FromResult<Logins>(logins);
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
                Database database;
                DatabaseProviderFactory factory = new DatabaseProviderFactory();
                database = factory.Create(ConnectionString);
                string storedProcedureName = "usp_save_user";
                DbCommand dbCommand = database.GetStoredProcCommand(storedProcedureName);
                dbCommand.CommandTimeout = DATABASE_CONNECTION_TIMEOUT;
                database.AddInParameter(dbCommand, "@username", DbType.String, login.username);
                database.AddInParameter(dbCommand, "@password", DbType.String, login.password);
                DataSet dsLoginBasicInfo = new DataSet("Logins");
                database.LoadDataSet(dbCommand, dsLoginBasicInfo, "Logins");

                if (dsLoginBasicInfo != null && dsLoginBasicInfo.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < dsLoginBasicInfo.Tables[0].Rows.Count; i++)
                    {
                        return await Task.FromResult<Response>( new Response
                        {
                            Status = dsLoginBasicInfo.Tables[0].Rows[i]["Status"].ToString(),
                            Message =
                            dsLoginBasicInfo.Tables[0].Rows[i]["Message"].ToString()
                        });
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
                return await Task.FromResult<Response>(new Response { Status = "Error", Message = Convert.ToString(ex.Message) });
            }
            return new Response();
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
                Database database;
                DatabaseProviderFactory factory = new DatabaseProviderFactory();
                database = factory.Create(ConnectionString);
                string storedProcedureName = "USP_SAVE_LOGIN_ROLE";
                DbCommand dbCommand = database.GetStoredProcCommand(storedProcedureName);
                dbCommand.CommandTimeout = DATABASE_CONNECTION_TIMEOUT;
                database.AddInParameter(dbCommand, "@LOGINROLEID", DbType.Int32, Login_Role.LOGINROLEID);
                database.AddInParameter(dbCommand, "@ROLENAME", DbType.String, Login_Role.ROLENAME);
                DataSet dsLoginBasicInfo = new DataSet("Logins");
                database.LoadDataSet(dbCommand, dsLoginBasicInfo, "Logins");

                if (dsLoginBasicInfo != null && dsLoginBasicInfo.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < dsLoginBasicInfo.Tables[0].Rows.Count; i++)
                    {
                        return await Task.FromResult<Response>(new Response
                        {
                            Status = dsLoginBasicInfo.Tables[0].Rows[i]["Status"].ToString(),
                            Message =
                            dsLoginBasicInfo.Tables[0].Rows[i]["Message"].ToString()
                        });
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
                return await Task.FromResult<Response>(new Response { Status = "Error", Message = Convert.ToString(ex.Message) });
            }
            return new Response();
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
                Database database;
                DatabaseProviderFactory factory = new DatabaseProviderFactory();
                database = factory.Create(ConnectionString);
                string storedProcedureName = "USP_SAVE_LOGIN_ROLE_USER";
                DbCommand dbCommand = database.GetStoredProcCommand(storedProcedureName);
                dbCommand.CommandTimeout = DATABASE_CONNECTION_TIMEOUT;
                database.AddInParameter(dbCommand, "@LOGINROLEUSERID", DbType.Int32, Login_Role_User.LOGINROLEUSERID);
                database.AddInParameter(dbCommand, "@USERID", DbType.Int32, Login_Role_User.login.ID);
                database.AddInParameter(dbCommand, "@LOGINROLEID", DbType.Int32, Login_Role_User.login_Role.LOGINROLEID);
                DataSet dsLoginBasicInfo = new DataSet("Logins");
                database.LoadDataSet(dbCommand, dsLoginBasicInfo, "Logins");

                if (dsLoginBasicInfo != null && dsLoginBasicInfo.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < dsLoginBasicInfo.Tables[0].Rows.Count; i++)
                    {
                        return await Task.FromResult<Response>(new Response
                        {
                            Status = dsLoginBasicInfo.Tables[0].Rows[i]["Status"].ToString(),
                            Message =
                            dsLoginBasicInfo.Tables[0].Rows[i]["Message"].ToString()
                        });
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
                return await Task.FromResult<Response>(new Response { Status = "Error", Message = Convert.ToString(ex.Message) });
            }
            return new Response();
        }
    }

}

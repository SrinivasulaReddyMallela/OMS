using Microsoft.AspNetCore.Authentication;
using Microsoft.Owin.Security.DataHandler;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace OMS.WEBAPI.Controllers
{
    public class HomeController : Controller
    {
        public static string baseAddress = "https://desktop-a0onhmt/OMSAPI";
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";
            Token token = new Token();
            token = AuthenticateUser("admin", "adminpass");

            using (var client = new HttpClient())
            {
                var tokenResponse = client.GetAsync(baseAddress + "/api/Order/GetAllProducts").Result;
                var RawtokenResult = tokenResponse.Content.ReadAsStringAsync().Result;
                token = Newtonsoft.Json.JsonConvert.DeserializeObject<Token>(RawtokenResult);
            }
            

            return View();
        }
        public static Token AuthenticateUser(string username, string password)
        {
            Token token = new Token();
            using (var client = new HttpClient())
            {
                var form = new Dictionary<string, string>
               {
                   {"grant_type", "password"},
                   {"username", username},
                   {"password", password},
               };
                var tokenResponse = client.PostAsync(baseAddress + "/token", new FormUrlEncodedContent(form)).Result;
                var RawtokenResult = tokenResponse.Content.ReadAsStringAsync().Result;
                token = Newtonsoft.Json.JsonConvert.DeserializeObject<Token>(RawtokenResult);

            }
            return token;
        }
    }
    public class Token
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }
        [JsonProperty("token_type")]
        public string TokenType { get; set; }
        [JsonProperty("expires_in")]
        public int ExpiresIn { get; set; }
        [JsonProperty("refresh_token")]
        public string RefreshToken { get; set; }
        [JsonProperty("error")]
        public string Error { get; set; }
        [JsonProperty("userName")]
        public string userName { get; set; }
        [JsonProperty("ID")]
        public string UserID { get; set; }
        [JsonProperty("LoginRoleID")]
        public string LOGINROLEID { set; get; }
        [JsonProperty("LoginRoleName")]
        public string ROLENAME { set; get; }


    }
}

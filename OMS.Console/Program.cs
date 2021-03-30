using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI.WebControls;
using System.Security.Claims;


namespace OMS.Console
{
    class Program
    {
        public static string baseAddress = "https://desktop-a0onhmt/OMSAPI";

        static void Main(string[] args)
        {
            Token token = new Token();
            token = AuthenticateUser("admin", "adminpass");
            // Next Request 
            if(token.AccessToken!=null&&!string.IsNullOrEmpty(token.AccessToken))
            using (HttpClient httpClient = new HttpClient())
            {
                
                    

                httpClient.BaseAddress = new Uri(baseAddress);
                //httpClient1.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token.AccessToken);
                httpClient.DefaultRequestHeaders.Add("Authorization", String.Format("Bearer {0}", token.AccessToken));
                HttpResponseMessage response = httpClient.GetAsync("api/OrderGet").Result;
                if (response.IsSuccessStatusCode)
                {
                    System.Console.WriteLine("Success");
                }
                string message = response.Content.ReadAsStringAsync().Result;
                System.Console.WriteLine("URL responese : " + message);

               
            }

            System.Console.ReadLine();
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
}

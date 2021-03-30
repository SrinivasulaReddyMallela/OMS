using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMS.Console
{
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

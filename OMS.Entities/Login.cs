using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMS.Entities
{
    public class Login
    {
        public int ID { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public int LOGINROLEID { get; set; }
        public string ROLENAME { get; set; }
    }
}

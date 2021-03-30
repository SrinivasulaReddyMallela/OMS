using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMS.Entities
{
    public class Login_Role_User
    {
        public int LOGINROLEUSERID { get; set; }
        public Login_Role login_Role { get; set; }
        public Login login { get; set; }
    }
}

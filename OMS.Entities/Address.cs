using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMS.Entities
{
    public class Address
    {
       public int AddressID { get; set; }
        public string  AdressLine1 { get; set; }
        public string AdressLine2 { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string EmailID { get; set; }
        public string Landmark { get; set; }
    }
}

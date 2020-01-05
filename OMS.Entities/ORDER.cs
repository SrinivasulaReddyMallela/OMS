using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMS.Entities
{
    public class Order
    {
        public int OrderID { get; set; }
        public OrderStatus StatusID { get; set; }
        public DateTime OrderDate { get; set; }
        public int UserID { get; set; }
        public Address address { get; set; }

    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMS.Entities
{
    public class Product
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public int weight { get; set; }
        public int height { get; set; }
        public Byte[] image { get; set; }
        public string SKU { get; set; }
        public string barcode { get; set; }
        public int quantity { get; set; }
    }
}

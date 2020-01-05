using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMS.Entities
{
    public class OrderDetails
    {
        public OrderDetails()
        {
            //order = new Order();
        }
        public int OrderDetailID { get; set; }
      //  public Order order { get; set; }
        public int OrderID { get; set; }
        public int ProductID { get; set; }
        public int Quantity { get; set; }
        public int OrderQuantity{ get; set; }
        public OrderStatus StatusID { get; set; }
        public DateTime OrderDate { get; set; }
        public int UserID { get; set; }
        public string ProductName { get; set; }
        public int weight { get; set; }
        public int height { get; set; }
        //public Byte[] image { get; set; }
        public string SKU { get; set; }
        public string barcode { get; set; }
        public string AdressLine1 { get; set; }
        public string AdressLine2 { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string EmailID { get; set; }
        public string Landmark { get; set; }
    }
}

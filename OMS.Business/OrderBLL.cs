using OMS.Data;
using OMS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMS.Business
{
    public class OrderBLL /*: IOrderBLL*/
    {
        OrderDLL OrderDLL;
        public OrderBLL()
        {
            OrderDLL = new OrderDLL();
        }
        public async Task<Products> GetAllProducts()
        {
            try
            {
                return await OrderDLL.GetAllProducts();
            }
            catch (Exception ex)
            {
                throw ex;
                return await Task.FromResult<Products>(new Products());
            }
        }
        public async Task<string> SaveProductInfo(Product product)
        {
            try
            {
                return await OrderDLL.SaveProductInfo(product);
            }
            catch (Exception ex)
            {
                throw ex;
                return await Task.FromResult<string>(string.Empty);
            }
        }
        public async Task<Orders> GetAllOrdersByUserID(int UserID)
        {
            try
            {
                return await OrderDLL.GetAllOrdersByUserID(UserID);
            }
            catch (Exception ex)
            {
                throw ex;
                return await Task.FromResult<Orders>(new Orders());
            }
        }
        public async Task<string> SaveOrderByUserID(OrderDetails orderDetails)
        {
            try
            {
                return await OrderDLL.SaveOrderByUserID(orderDetails);
            }
            catch (Exception ex)
            {
                throw ex;
                return await Task.FromResult<string>(ex.Message.ToString());
            }
        }
    }
    public interface IOrderBLL
    {
        Task<Products> GetAllProducts();
        Task<string> SaveProductInfo(Product product);
        Task<Orders> GetAllOrdersByUserID(int UserID);
        Task<string> SaveOrderByUserID(OrderDetails orderDetails);
    }
}

using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using OMS.Entities;

using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Common;
using System.Data;

namespace OMS.Data
{
    public class OrderDLL
    {
        public static int DATABASE_CONNECTION_TIMEOUT = 0;
        public static string ConnectionString = "OMSConnectionString";
        public async Task<Products> GetAllProducts()
        {
            Products products = new Products();
            products.ProductList = new List<Product>();
            try
            {
                Database database;
                DatabaseProviderFactory factory = new DatabaseProviderFactory();
                database = factory.Create(ConnectionString);
                string storedProcedureName = "USP_GET_PRODUCT";
                DbCommand dbCommand = database.GetStoredProcCommand(storedProcedureName);
                dbCommand.CommandTimeout = DATABASE_CONNECTION_TIMEOUT;
                DataSet dsLoginBasicInfo = new DataSet("Logins");
                database.LoadDataSet(dbCommand, dsLoginBasicInfo, "Logins");

                if (dsLoginBasicInfo != null && dsLoginBasicInfo.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < dsLoginBasicInfo.Tables[0].Rows.Count; i++)
                    {
                        products.ProductList.Add(new Product
                        {
                            ProductID = Convert.ToInt32(dsLoginBasicInfo.Tables[0].Rows[i]["ProductID"].ToString()),
                            ProductName = dsLoginBasicInfo.Tables[0].Rows[i]["ProductName"].ToString(),
                            weight = Convert.ToInt32(dsLoginBasicInfo.Tables[0].Rows[i]["weight"].ToString()),
                            height = Convert.ToInt32(dsLoginBasicInfo.Tables[0].Rows[i]["height"].ToString()),
                            //image=Convert.ToByte(dsLoginBasicInfo.Tables[0].Rows[i]["image"].ToString()),
                            SKU = dsLoginBasicInfo.Tables[0].Rows[i]["SKU"].ToString(),
                            barcode = dsLoginBasicInfo.Tables[0].Rows[i]["barcode"].ToString(),
                            quantity = Convert.ToInt32(dsLoginBasicInfo.Tables[0].Rows[i]["quantity"].ToString()),
                        });
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return await Task.FromResult<Products>(products);
        }
        public async Task<string> SaveProductInfo(Product product)
        {
            try
            {
                Database database;
                DatabaseProviderFactory factory = new DatabaseProviderFactory();
                database = factory.Create(ConnectionString);
                string storedProcedureName = "USP_SAVE_PRODUCT";
                DbCommand dbCommand = database.GetStoredProcCommand(storedProcedureName);
                dbCommand.CommandTimeout = DATABASE_CONNECTION_TIMEOUT;
                database.AddInParameter(dbCommand, "@ProductID", DbType.Int32, product.ProductID);
                database.AddInParameter(dbCommand, "@ProductName", DbType.String, product.ProductName);
                database.AddInParameter(dbCommand, "@weight", DbType.Int32, product.weight);
                database.AddInParameter(dbCommand, "@height", DbType.Int32, product.height);
                database.AddInParameter(dbCommand, "@image", DbType.Byte, product.image);
                database.AddInParameter(dbCommand, "@SKU", DbType.String, product.SKU);
                database.AddInParameter(dbCommand, "@barcode", DbType.String, product.barcode);
                database.AddInParameter(dbCommand, "@quantity", DbType.Int32, product.quantity);
                DataSet dsLoginBasicInfo = new DataSet("Logins");
                database.LoadDataSet(dbCommand, dsLoginBasicInfo, "Logins");

                if (dsLoginBasicInfo != null && dsLoginBasicInfo.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < dsLoginBasicInfo.Tables[0].Rows.Count; i++)
                    {

                    }
                }

                return await Task.FromResult<string>("Product Saved Successfully");

            }
            catch (Exception ex)
            {
                throw ex;
                return await Task.FromResult<string>(string.Empty);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<Orders> GetAllOrdersByUserID(int UserID)
        {
            Orders Orders = new Orders();
            Orders.OrderList = new List<OrderDetails>();

            try
            {
                Database database;
                DatabaseProviderFactory factory = new DatabaseProviderFactory();
                database = factory.Create(ConnectionString);
                string storedProcedureName = "USP_GET_ORDERDETAILS";
                DbCommand dbCommand = database.GetStoredProcCommand(storedProcedureName);
                dbCommand.CommandTimeout = DATABASE_CONNECTION_TIMEOUT;
                database.AddInParameter(dbCommand, "@USERID", DbType.Int32, UserID);
                DataSet dsLoginBasicInfo = new DataSet("Logins");
                database.LoadDataSet(dbCommand, dsLoginBasicInfo, "Logins");

                if (dsLoginBasicInfo != null && dsLoginBasicInfo.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < dsLoginBasicInfo.Tables[0].Rows.Count; i++)
                    {
                        Orders.OrderList.Add(new OrderDetails
                        {
                            OrderID = Convert.ToInt32(dsLoginBasicInfo.Tables[0].Rows[i]["OrderID"].ToString()),
                            OrderDate = Convert.ToDateTime(dsLoginBasicInfo.Tables[0].Rows[i]["OrderDate"].ToString()),
                            UserID = Convert.ToInt32(dsLoginBasicInfo.Tables[0].Rows[i]["UserID"].ToString()),
                            Quantity = Convert.ToInt32(dsLoginBasicInfo.Tables[0].Rows[i]["Quantity"].ToString()),
                            OrderQuantity = Convert.ToInt32(dsLoginBasicInfo.Tables[0].Rows[i]["ORDERQUANTITY"].ToString()),
                            ProductName = Convert.ToString(dsLoginBasicInfo.Tables[0].Rows[i]["ProductName"].ToString()),
                            weight = Convert.ToInt32(dsLoginBasicInfo.Tables[0].Rows[i]["weight"].ToString()),
                            height = Convert.ToInt32(dsLoginBasicInfo.Tables[0].Rows[i]["height"].ToString()),
                            SKU = Convert.ToString(dsLoginBasicInfo.Tables[0].Rows[i]["SKU"].ToString()),
                            barcode = Convert.ToString(dsLoginBasicInfo.Tables[0].Rows[i]["barcode"].ToString()),
                            AdressLine1 = Convert.ToString(dsLoginBasicInfo.Tables[0].Rows[i]["AdressLine1"].ToString()),
                            AdressLine2 = Convert.ToString(dsLoginBasicInfo.Tables[0].Rows[i]["AdressLine2"].ToString()),
                            State = Convert.ToString(dsLoginBasicInfo.Tables[0].Rows[i]["State"].ToString()),
                            ZipCode = Convert.ToString(dsLoginBasicInfo.Tables[0].Rows[i]["ZipCode"].ToString()),
                            EmailID = Convert.ToString(dsLoginBasicInfo.Tables[0].Rows[i]["EmailID"].ToString()),
                            Landmark = Convert.ToString(dsLoginBasicInfo.Tables[0].Rows[i]["Landmark"].ToString()),
                        });
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return await Task.FromResult<Orders>(Orders);
        }
        public async Task<string> SaveOrderByUserID(OrderDetails orderDetails)
        {
            string status = string.Empty;
            try
            {
                Database database;
                DatabaseProviderFactory factory = new DatabaseProviderFactory();
                database = factory.Create(ConnectionString);
                string storedProcedureName = "USP_Save_ORDERDETAILSByUserID";
                DbCommand dbCommand = database.GetStoredProcCommand(storedProcedureName);
                dbCommand.CommandTimeout = DATABASE_CONNECTION_TIMEOUT;
                database.AddInParameter(dbCommand, "@OrderDetailID", DbType.Int32, orderDetails.OrderDetailID);
                database.AddInParameter(dbCommand, "@OrderID", DbType.Int32, orderDetails.OrderID);
                database.AddInParameter(dbCommand, "@ProductID", DbType.Int32, orderDetails.ProductID);
                database.AddInParameter(dbCommand, "@Quantity", DbType.Int32, orderDetails.OrderQuantity);
                database.AddInParameter(dbCommand, "@OrderQuantity", DbType.Int32, orderDetails.Quantity);
                database.AddInParameter(dbCommand, "@StatusID", DbType.Int32, orderDetails.StatusID);
                database.AddInParameter(dbCommand, "@OrderDate", DbType.DateTime, orderDetails.OrderDate);
                database.AddInParameter(dbCommand, "@UserID", DbType.Int32, orderDetails.UserID);
                database.AddInParameter(dbCommand, "@AdressLine1", DbType.String, orderDetails.AdressLine1);
                database.AddInParameter(dbCommand, "@AdressLine2", DbType.String, orderDetails.AdressLine2);
                database.AddInParameter(dbCommand, "@State", DbType.String, orderDetails.State);
                database.AddInParameter(dbCommand, "@ZipCode", DbType.String, orderDetails.ZipCode);
                database.AddInParameter(dbCommand, "@EmailID", DbType.String, orderDetails.EmailID);
                database.AddInParameter(dbCommand, "@Landmark", DbType.String, orderDetails.Landmark);
                DataSet dsLoginBasicInfo = new DataSet("Logins");
                database.LoadDataSet(dbCommand, dsLoginBasicInfo, "Logins");

                if (dsLoginBasicInfo != null && dsLoginBasicInfo.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < dsLoginBasicInfo.Tables[0].Rows.Count; i++)
                    {
                         
                    }
                }
                status = "Order Placed successfully";
            }
            catch (Exception ex)
            {
                throw ex;                
            }
            return await Task.FromResult<string>(status);
        }
    }
}


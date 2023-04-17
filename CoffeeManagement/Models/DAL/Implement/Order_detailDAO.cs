using CoffeeManagement.Models.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CoffeeManagement.Models.DAL.Implement
{
    public class Order_detailDAO : IOrder_detailDAO
    {
        public void delete(int productId)
        {
            DatabaseAccess db = new DatabaseAccess();
            db.connect();

            int ret = db.deleteById("DeleteOrderDetailByProduct", productId);
            db.close();
            if (ret == 0)
            {
                throw new Exception("Not found object!");
            }
        }

        public List<Order_detail> getAll()
        {
            DatabaseAccess db = new DatabaseAccess();
            db.connect();

            SqlDataReader dataReader = db.getAll("GetAllOrders");
            List<Order_detail> result = new List<Order_detail>();
            while (dataReader.Read())
            {
                int orderId = dataReader.GetInt32(0);
                int productId = dataReader.GetInt32(1);
                int quantity = dataReader.GetInt32(2);
                result.Add(new Order_detail(orderId, productId, quantity));
            }
            dataReader.Close();
            db.close();
            return result;
        }

        public Order_detail getById(int id)
        {
            DatabaseAccess db = new DatabaseAccess();
            db.connect();

            //SqlDataReader dataReader = DatabaseAccess.getById("GetOrderDetailById", id);
            SqlCommand command = new SqlCommand();
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandText = "GetOrderDetailById";
            command.Connection = db.connection;

            command.Parameters.Add("@id", System.Data.SqlDbType.Int).Value = id;
            SqlDataReader dataReader = command.ExecuteReader();
            if (dataReader.Read())
            {
                int orderId = dataReader.GetInt32(0);
                int productId = dataReader.GetInt32(1);
                int quantity = dataReader.GetInt32(2);
                dataReader.Close();
                db.close();
                return new Order_detail(orderId, productId, quantity);
            }
            dataReader.Close();
            db.close();
            return null;
        }

        public void insert(Order_detail data)
        {
            DatabaseAccess db = new DatabaseAccess();
            db.connect();

            SqlCommand command = db.connection.CreateCommand();
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandText = "InsertOrderDetail";
            command.Connection = db.connection;

            command.Parameters.Add("@Order_id", System.Data.SqlDbType.Int).Value = data.OrderId;
            command.Parameters.Add("@Product_id", System.Data.SqlDbType.Int).Value = data.ProductId;
            command.Parameters.Add("@Quantity", System.Data.SqlDbType.Int).Value = data.Quantity;
            
            int ret = command.ExecuteNonQuery();
            db.close();
            if (ret == 0)
            {
                throw new Exception("Update fail");
            }
        }

        public void update(Order_detail data)
        {
            DatabaseAccess db = new DatabaseAccess();
            db.connect();

            SqlCommand command = db.connection.CreateCommand();
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandText = "UpdateOrderDetailQuantity";
            command.Connection = db.connection;

            command.Parameters.Add("@orderId", System.Data.SqlDbType.Int).Value = data.OrderId;
            command.Parameters.Add("@product_Id", System.Data.SqlDbType.Int).Value = data.ProductId;
            command.Parameters.Add("@quantity", System.Data.SqlDbType.DateTime).Value = data.Quantity;


            int ret = command.ExecuteNonQuery();
            db.close();
            if (ret == 0)
            {
                throw new Exception("Update fail");
            }
        }
    }
}
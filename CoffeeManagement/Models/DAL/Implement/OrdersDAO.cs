using CoffeeManagement.Models.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CoffeeManagement.Models.DAL.Implement
{
    public class OrdersDAO : IOrdersDAO
    {
        public void delete(int id)
        {
            DatabaseAccess db = new DatabaseAccess();
            db.connect();

            int ret = db.deleteById("DeleteOrderById", id);
            db.close();
            if (ret == 0)
            {
                throw new Exception("Not found object!");
            }            
        }

        public List<Orders> getAll()
        {
            DatabaseAccess db = new DatabaseAccess();
            db.connect();

            SqlDataReader dataReader = db.getAll("GetAllOrders");
            List<Orders> result = new List<Orders>(); 
            while(dataReader.Read())
            {
                int id = dataReader.GetInt32(0);
                int tableNumber = dataReader.GetInt32(1);
                DateTime time = dataReader.GetDateTime(2);
                byte state = dataReader.GetByte(3);
                result.Add(new Orders(id, tableNumber, time, state));
            }
            db.close();
            return result;
        }


        public Orders getById(int id)
        {
            DatabaseAccess db = new DatabaseAccess();
            db.connect();

            //SqlDataReader dataReader = DatabaseAccess.getById("GetOrdersById", id);
            SqlCommand command = new SqlCommand();
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandText = "GetOrdersById";
            command.Connection = db.connection;

            command.Parameters.Add("@id", System.Data.SqlDbType.Int).Value = id;
            SqlDataReader dataReader = command.ExecuteReader();
            if (dataReader.Read())
            { 
                int idd = dataReader.GetInt32(0);
                int tableNumber = dataReader.GetInt32(1);
                DateTime time = dataReader.GetDateTime(2);
                byte state = dataReader.GetByte(3);
                dataReader.Close();
                db.close();
                return new Orders(idd, tableNumber, time, state);
            }
            dataReader.Close();
            db.close();
            return null;
        }

        public Orders getOrderProduct(int orderId)
        {
            DatabaseAccess db = new DatabaseAccess();
            db.connect();

            SqlCommand command = db.connection.CreateCommand();
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandText = "GetOrderProduct";
            command.Connection = db.connection;

            command.Parameters.Add("@order_Id", System.Data.SqlDbType.Int).Value = orderId;
            SqlDataReader reader = command.ExecuteReader();

            List<Product> listProduct = new List<Product>();
            List<int> lQuantity = new List<int>();
            while (reader.Read())
            {
                int productId = reader.GetInt32(0);
                string productName = reader.GetString(1);
                double productCost = reader.GetDouble(2);
                int quantity = reader.GetInt32(3);
                listProduct.Add(new Product(productId, productName, productCost));
                lQuantity.Add(quantity);
            }
            db.close();
            return new Orders(listProduct, lQuantity);
        }

        public double getTotalCostOfOrder(int orderId)
        {
            DatabaseAccess db = new DatabaseAccess();
            db.connect();

            SqlCommand command = db.connection.CreateCommand();
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandText = "GetTotalCostOfOrder";
            command.Connection = db.connection;

            command.Parameters.Add("@order_Id", System.Data.SqlDbType.Int).Value = orderId;

            SqlDataReader reader = command.ExecuteReader();
            
            if (reader.Read())
            {
                return reader.GetDouble(0);
            }
            return -1;
        }

        public void insert(Orders data)
        {
            DatabaseAccess db = new DatabaseAccess();
            db.connect();

            SqlCommand command = db.connection.CreateCommand();
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandText = "InsertOrder";
            command.Connection = db.connection;

            command.Parameters.Add("@id", System.Data.SqlDbType.Int).Value = data.Id;
            command.Parameters.Add("@tableNumber", System.Data.SqlDbType.Int).Value = data.TableNumber;
            command.Parameters.Add("@time", System.Data.SqlDbType.DateTime).Value = data.Time;
            command.Parameters.Add("@state", System.Data.SqlDbType.TinyInt).Value = data.State;

            int ret = command.ExecuteNonQuery();
            db.close();
            if (ret == 0)
            {
                throw new Exception("Update fail");
            }
        }

        public void update(Orders data)
        {
            DatabaseAccess db = new DatabaseAccess();
            db.connect();

            SqlCommand command = db.connection.CreateCommand();
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandText = "UpdateOrder";
            command.Connection = db.connection;

            command.Parameters.Add("@id", System.Data.SqlDbType.Int).Value = data.Id;
            command.Parameters.Add("@tableNumber", System.Data.SqlDbType.Int).Value = data.TableNumber;
            command.Parameters.Add("@time", System.Data.SqlDbType.DateTime).Value = data.Time;
            command.Parameters.Add("@state", System.Data.SqlDbType.TinyInt).Value = data.State;

            int ret = command.ExecuteNonQuery();
            db.close();
            if (ret == 0)
            {
                throw new Exception("Update fail");
            }
        }

        public void updateOrderState(int id, byte state)
        {
            DatabaseAccess db = new DatabaseAccess();
            db.connect();

            SqlCommand command = db.connection.CreateCommand();
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandText = "UpdateOrderState";
            command.Connection = db.connection;

            command.Parameters.Add("@id", System.Data.SqlDbType.Int).Value = id;
            command.Parameters.Add("@state", System.Data.SqlDbType.TinyInt).Value = state;

            int ret = command.ExecuteNonQuery();
            db.close();
            if (ret == 0)
            {
                throw new Exception("Update fail");
            }
        }

        public void updateOrderTableNumber(int id, int tableNumber)
        {
            DatabaseAccess db = new DatabaseAccess();
            db.connect();

            SqlCommand command = db.connection.CreateCommand();
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandText = "UpdateOrderTableNumber";
            command.Connection = db.connection;

            command.Parameters.Add("@id", System.Data.SqlDbType.Int).Value = id;
            command.Parameters.Add("@tableNumber", System.Data.SqlDbType.Int).Value = tableNumber;

            int ret = command.ExecuteNonQuery();
            db.close();
            if (ret == 0)
            {
                throw new Exception("Update fail");
            }
        }

        public List<DateTime> getAllDate()
        {
            DatabaseAccess db = new DatabaseAccess();
            db.connect();

            SqlCommand command = db.connection.CreateCommand();
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandText = "GetAllDate";
            command.Connection = db.connection;

            SqlDataReader dataReader = command.ExecuteReader();
            List<DateTime> result = new List<DateTime>();
            while (dataReader.Read())
            {
                DateTime tmp = dataReader.GetDateTime(0);
                result.Add(tmp);
            }
            dataReader.Close();
            db.close();
            return result;
        }

        public List<double> getTotalSales()
        {
            DatabaseAccess db = new DatabaseAccess();
            db.connect();

            List<DateTime> allDate = this.getAllDate();
            List<double> result = new List<double>();
            foreach(DateTime date in allDate)
            {
                SqlCommand command = db.connection.CreateCommand();
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.CommandText = "GetTotalCostInADay";
                command.Connection = db.connection;

                command.Parameters.Add("@dateTime", System.Data.SqlDbType.DateTime).Value = date;
                SqlDataReader dataReader = command.ExecuteReader();
                if (dataReader.Read())
                {
                    result.Add(dataReader.GetDouble(0));
                }
                dataReader.Close();
            }
            db.close();
            return result;
        }
    }
}
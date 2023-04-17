using CoffeeManagement.Models.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CoffeeManagement.Models.DAL.Implement
{
    public class ProductDAO : IProductDAO
    {
        public void delete(int id)
        {
            DatabaseAccess db = new DatabaseAccess();
            db.connect();
            int rowChanged = db.deleteById("DeleteProduct", id);
            db.close();
            if (rowChanged == 0)
            {
                throw new Exception("Not found object!");
            }
            
        }

        public List<Product> getAll()
        {
            DatabaseAccess db = new DatabaseAccess();
            db.connect();

            List<Product> result = new List<Product>();
            SqlDataReader sqlDataReader = db.getAll("GetAllProduct");
            while (sqlDataReader.Read()) 
            {
                int id = sqlDataReader.GetInt32(0);
                string name = sqlDataReader.GetString(1);
                double cost = sqlDataReader.GetDouble(2);
                result.Add(new Product(id, name, cost));
            }
            sqlDataReader.Close();
            db.close();
            return result;
         
        }

        public Product getById(int id)
        {
            DatabaseAccess db = new DatabaseAccess();
            db.connect();

            //SqlDataReader dataReader = DatabaseAccess.getById("GetProductById", id);
            SqlCommand command = new SqlCommand();
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandText = "GetProductById";
            command.Connection = db.connection;

            command.Parameters.Add("@id", System.Data.SqlDbType.Int).Value = id;
            SqlDataReader dataReader = command.ExecuteReader();
            if (dataReader.Read())
            {
                int idd = dataReader.GetInt32(0);
                string name = dataReader.GetString(1);
                float cost = dataReader.GetFloat(2);
                db.close();
                dataReader.Close();
                return new Product(id, name, cost);

            }
            dataReader.Close();
            db.close();
            return null;
           
        }

        public void insert(Product data)
        {
            DatabaseAccess db = new DatabaseAccess();
            db.connect();

            SqlCommand command = db.connection.CreateCommand();
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandText = "InsertProduct";
            command.Connection = db.connection;

            command.Parameters.Add("@id", System.Data.SqlDbType.Int).Value = data.Id;
            command.Parameters.Add("@name", System.Data.SqlDbType.NVarChar).Value = data.Name;
            command.Parameters.Add("@cost", System.Data.SqlDbType.Float).Value = data.Cost;
            int ret = command.ExecuteNonQuery();
            db.close();
            if (ret == 0)
            {
                throw new Exception("Update fail");
            }
            
        }

        public void update(Product data)
        {
            DatabaseAccess db = new DatabaseAccess();
            db.connect();

            SqlCommand command = db.connection.CreateCommand();
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandText = "UpdateProduct";
            command.Connection = db.connection;

            command.Parameters.Add("@id", System.Data.SqlDbType.Int).Value = data.Id;
            command.Parameters.Add("@name", System.Data.SqlDbType.NVarChar).Value = data.Name;
            command.Parameters.Add("@cost", System.Data.SqlDbType.Float).Value = data.Cost;
     

            int ret = command.ExecuteNonQuery();
            db.close();
            if (ret == 0)
            {
                throw new Exception("Update fail");
            }
            
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace CoffeeManagement.Models.DAL
{
    public class DatabaseAccess
    {
        private const string URL = @"Data Source = DESKTOP-8H3JR7M; Initial Catalog = CoffeeManagement; Integrated Security=True; MultipleActiveResultSets=true ";
        public SqlConnection connection = null;

        public void connect()
        {
            try
            {
                if (connection == null)
                {
                    connection = new SqlConnection(URL);
                    connection.Open();
                }
                else if (connection.State == System.Data.ConnectionState.Closed)
                {
                    connection.Open();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void close()
        {
            if (connection != null && connection.State == System.Data.ConnectionState.Open)
                connection.Close();
        }
        public SqlDataReader getAll(string procName)
        {
            if (connection != null && connection.State == System.Data.ConnectionState.Open)
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.CommandText = procName;
                command.Connection = connection;
                return command.ExecuteReader();
            }
            return null;
        }
   /*     public SqlDataReader getById(string proName, int id)
        {
            if (connection != null && connection.State == System.Data.ConnectionState.Open)
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.CommandText = proName;
                command.Connection = connection;

                command.Parameters.Add("@id", System.Data.SqlDbType.Int).Value = id;
                SqlDataReader result = command.ExecuteReader();
                return result;
            }
            return null;
        }*/
        public int deleteById(string proName, int id)
        {
            if (connection != null && connection.State == System.Data.ConnectionState.Open)
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.CommandText = proName;
                command.Connection = connection;

                command.Parameters.Add("@id", System.Data.SqlDbType.Int).Value = id;
                return command.ExecuteNonQuery();
            }
            return 0;
        }
        
    }
}
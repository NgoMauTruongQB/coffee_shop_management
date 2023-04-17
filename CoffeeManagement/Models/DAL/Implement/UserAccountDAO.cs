using CoffeeManagement.Models.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CoffeeManagement.Models.DAL.Implement
{
    public class UserAccountDAO : IUserAccountDAO
    {
        public List<UserAccount> getAll()
        {
            DatabaseAccess db = new DatabaseAccess();
            db.connect();
            List<UserAccount> result = new List<UserAccount>();
            SqlDataReader reader = db.getAll("GetAllUserAccount");
            while (reader.Read())
            {
                string Username = reader.GetString(0);
                string Password = reader.GetString(1);
                byte Role = reader.GetByte(2);
                result.Add(new UserAccount(Username, Password ,Role));
            }
            reader.Close();
            db.close();
            return result;
        }

        public UserAccount getById(string userName)
        {
            DatabaseAccess db = new DatabaseAccess();
            db.connect();

            SqlCommand command = db.connection.CreateCommand();
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandText = "GetUserAccount";
            command.Connection = db.connection;

            command.Parameters.Add("@userName", System.Data.SqlDbType.VarChar).Value = userName;
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                string Name = reader.GetString(0);
                string  Password= reader.GetString(1);
                byte Role = reader.GetByte(2);
                db.close();
                reader.Close();
                return new UserAccount(Name, Password, Role);
            }
            reader.Close();
            db.close();
            return null;
        }

        public void insert(UserAccount data)
        {
            DatabaseAccess db = new DatabaseAccess();
            db.connect();

            SqlCommand command = db.connection.CreateCommand();
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandText = "InsertUserAccount";
            command.Connection = db.connection;

            command.Parameters.Add("@Username", System.Data.SqlDbType.NVarChar).Value = data.UserName;
            command.Parameters.Add("@Password", System.Data.SqlDbType.NVarChar).Value = data.Password;
            command.Parameters.Add("@Role", System.Data.SqlDbType.TinyInt).Value = data.Role;
           

            int ret = command.ExecuteNonQuery();
            db.close();
            if (ret == 0)
            {
                throw new Exception("Insert fail");
            }
        }
        public void update(UserAccount data)
        {
            DatabaseAccess db = new DatabaseAccess();
            db.connect();

            SqlCommand command = db.connection.CreateCommand();
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandText = "UpdateUserAccountPassword";
            command.Connection = db.connection;

            command.Parameters.Add("@Username", System.Data.SqlDbType.NVarChar).Value = data.UserName;
            command.Parameters.Add("@Password", System.Data.SqlDbType.NVarChar).Value = data.Password;
        

            int ret = command.ExecuteNonQuery();
            db.close();
            if (ret == 0)
            {
                throw new Exception("Update fail");
            }
            
        }

        public void delete(string userName)
        {
            DatabaseAccess db = new DatabaseAccess();
            db.connect();

            SqlCommand command = db.connection.CreateCommand();
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandText = "DeleteUserAccount";
            command.Connection = db.connection;

            command.Parameters.Add("@userName", System.Data.SqlDbType.VarChar).Value = userName;

            int ret = command.ExecuteNonQuery();
            db.close();
            if (ret == 0)
            {
                throw new Exception("Not found object!");
            }  
        }

        public byte isValid(UserAccount data)
        {
            DatabaseAccess db = new DatabaseAccess();
            db.connect();
            UserAccount fromDB = this.getById(data.UserName);
            db.close();
            if (fromDB == null || fromDB.Password.CompareTo(data.Password) != 0)
            {
                return 0;
            }
            else return fromDB.Role;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoffeeManagement.Models.Model
{
    public class UserAccount
    {
        private string userName;
        private string password;
        private byte role;

        public UserAccount(string userName, string password, byte role = 0)
        {
            this.userName = userName;
            this.password = password;
            this.role = role;
        }

        public string UserName { get => userName; set => userName = value; }
        public string Password { get => password; set => password = value; }
        public byte Role { get => role; set => role = value; }

        
    }
}
using CoffeeManagement.Models.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeManagement.Models.DAL
{
    interface IUserAccountDAO : IDAO<UserAccount, string>
    {
        byte isValid(UserAccount data);
    }
}

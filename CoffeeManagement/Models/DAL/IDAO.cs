using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeManagement.Models.DAL
{
    interface IDAO<T, U>
    {
        List<T> getAll();
        T getById(U id);
        void insert(T data);
        void update(T data);
        void delete(U id);
    }
}

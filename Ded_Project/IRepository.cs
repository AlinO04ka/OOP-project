using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ded_Project
{
    interface IRepository<T> : IDisposable where T : class 
    {
        void GetUsers();
        T GetUser(string login);
        void createUser(T user);
        void updateUser(T user);
        void DeleteUser(string login);

        void Save();
    }
}

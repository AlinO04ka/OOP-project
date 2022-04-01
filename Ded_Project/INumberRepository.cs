using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ded_Project
{
    interface INumberRepository<T> : IDisposable where T : class
    {
        void GetNumbers();
        T GetNumber(int id);
        void createNumber(T number);
        void updateNumber(T number);
        void DeleteNumber(int id);

        void Save();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicAlbumsEF.Repository
{
    public interface IRepository<T> where T : class
    {
        ICollection<T> GetAll();
        void Add(T item);
        void Update(T item);
        void Delete(T item);
        void Delete(int id);
        T Get(int id);
    }
}

using Microsoft.EntityFrameworkCore;
using MusicAlbumsEF.Context;
using MusicAlbumsEF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicAlbumsEF.Repository
{
    public class OrderRepository : IRepository<Order>
    {
        private readonly MusicDbContext _musicDbContext;

        public OrderRepository(MusicDbContext musicDbContext)
        {
            _musicDbContext = musicDbContext;
        }
        public void Add(Order item)
        {
            _musicDbContext.Orders.Add(item);
            _musicDbContext.SaveChanges();
        }

        public void Delete(Order item)
        {
            _musicDbContext.Orders.Remove(item);
            _musicDbContext.SaveChanges();
        }

        public void Delete(int id)
        {
            var order = _musicDbContext.Orders.FirstOrDefault(o => o.Id == id);
            if (order != null)
            {
                _musicDbContext.Orders.Remove(order);
                _musicDbContext.SaveChanges();
            }
        }

        public Order Get(int id)
        {
            var order = _musicDbContext.Orders.FirstOrDefault(o => o.Id == id);
            return order;
        }

        public ICollection<Order> GetAll()
        {
            return _musicDbContext.Orders.ToList();
        }

        public void Update(Order item)
        {
            _musicDbContext.Entry(item).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _musicDbContext.SaveChanges();
        }
    }
}

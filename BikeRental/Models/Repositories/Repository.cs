using BikeRental.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BikeRental.Models.Repositories
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        BikeRentalDbContext _context;
        DbSet<T> _entities;

        public Repository(BikeRentalDbContext context)
        {
            _context = context;
            _entities = _context.Set<T>();
        }

        public T Delete(int id)
        {
            T entity = _entities.Find(id);
            _entities.Remove(entity);
            Save();
            return entity;
        }

        public IEnumerable<T> Get()
        {
            return _entities.AsEnumerable();
        }

        public T Get(int id)
        {
            return _entities.Find(id);
        }

        public T Insert(T item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item), "Cannot insert null");

            _entities.Add(item);
            Save();

            return item;
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public T Update(T item)
        {
            _context.Entry(item).State = EntityState.Modified;
            Save();
            return item;
        }
    }
}

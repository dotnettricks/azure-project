using ePizzaHub.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ePizzaHub.Repositories.Implementations
{
    public class RepositoryCosmos<TEntity> : IRepositoryCosmos<TEntity> where TEntity : class
    {
        protected DbContext _dbContextCosmos;
        public RepositoryCosmos(DbContext dbContextCosmos)
        {
            _dbContextCosmos = dbContextCosmos;
        }
        public void Add(TEntity entity)
        {
            _dbContextCosmos.Set<TEntity>().Add(entity);
        }

        public void Delete(object Id)
        {
            TEntity entity = _dbContextCosmos.Set<TEntity>().Find(Id);
            if (entity != null)
                _dbContextCosmos.Set<TEntity>().Remove(entity);
        }

        public TEntity Find(object Id)
        {
            return _dbContextCosmos.Set<TEntity>().Find(Id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _dbContextCosmos.Set<TEntity>().ToList();
        }

        public void Remove(TEntity entity)
        {
            _dbContextCosmos.Set<TEntity>().Remove(entity);
        }

        public int SaveChanges()
        {
            return _dbContextCosmos.SaveChanges();
        }

        public void Update(TEntity entity)
        {
            _dbContextCosmos.Set<TEntity>().Update(entity);
        }
    }
}


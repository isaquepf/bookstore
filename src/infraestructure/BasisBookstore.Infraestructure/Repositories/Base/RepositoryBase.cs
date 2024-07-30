﻿using Basis.Bookstore.Core.Domain.Contracts;
using Basis.Bookstore.Core.Domain.Contracts.Repositories.Base;
using BasisBookstore.Infraestructure.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;

namespace BasisBookstore.Infraestructure.Repositories.Base
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : EntityBase
    {
        private readonly ILogger<RepositoryBase<T>> _logger;
        protected readonly BookstoreContext _context;
        protected DbSet<T> Entity;
        private string _entityName;

        public RepositoryBase(BookstoreContext contexto, ILogger<RepositoryBase<T>> logger)
        {
            _context = contexto;
            Entity = _context.Set<T>();
            _logger = logger;
            _entityName = nameof(T);
        }

        public virtual T Add(T entidade)
        {
            try
            {
                Entity.Add(entidade);
                Save();
                return entidade;
            }
            catch (Exception error)
            {
                _logger.LogError(error, $"Error when save entity {_entityName}, error {error.Message}");
                throw;
            }
        }

        public virtual void Remove(T entidade)
        {
            try
            {
                Entity.Remove(entidade);
                Save();
            }
            catch (Exception error)
            {
                _logger.LogError(error, $"Error when remove entity {_entityName}, error {error.Message}");
                throw;
            }
        }

        public virtual T GetById(int id) => Entity.Find(id);

        public virtual IEnumerable<T> GetAll() => Entity.ToList();

        public virtual void Update(T entidade)
        {
            try
            {
                Entity.Entry(entidade).State = EntityState.Modified;
                Save();
            }
            catch (Exception error)
            {
                _logger.LogError(error, $"Error when remove entity {_entityName}, error {error.Message}");
                throw;
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    
        public IEnumerable<T> Get(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = "")
        {
            IQueryable<T> query = Entity;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace OnlineTestRepository
{
    public class GenericRepository<TObject> : IRepository<TObject> where TObject : class
    {
        private DbContext _context = null;
        private DbSet<TObject> _entitySet = null;

        public GenericRepository(DbContext Context)
        {
            this._context = Context;
            this._entitySet = Context.Set<TObject>();
        }

        protected DbSet<TObject> DbSet
        {
            get
            { return _entitySet; }
        }

        public virtual IEnumerable<TObject> All()
        {
            return DbSet.AsEnumerable<TObject>();
        }

        public virtual IEnumerable<TObject> Get(Expression<Func<TObject, bool>> predicate)
        {
            return DbSet.Where(predicate).AsEnumerable<TObject>();
        }
        public virtual IEnumerable<TObject> Get(Expression<Func<TObject, bool>> predicate, string includeProperties = "")
        {
            IQueryable<TObject> query = DbSet;

            query = query.Where(predicate);

            foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            { query = query.Include(includeProperty); }

            return query.ToList();
        }

        public virtual TObject Find(params object[] keys)
        {
            return DbSet.Find(keys);
        }
        public virtual TObject Find(Expression<Func<TObject, bool>> predicate)
        {
            return DbSet.FirstOrDefault(predicate);
        }
        public virtual TObject Find(Expression<Func<TObject, bool>> predicate, string includeProperties = "")
        {
            IQueryable<TObject> query = DbSet;

            foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            { query = query.Include(includeProperty); }

            return query.FirstOrDefault(predicate);
        }

        public virtual TObject Create(TObject entry)
        {
            var newEntry = DbSet.Add(entry);
            Save();
            return newEntry;

        }
        public virtual void Update(TObject entry)
        {
            DbSet.Attach(entry);
            _context.Entry(entry).State = EntityState.Modified;
            Save();
        }

        public virtual void Delete(object id)
        {
            TObject entityToDelete = DbSet.Find(id);
            Delete(entityToDelete);
        }
        public virtual void Delete(TObject entry)
        {
            if (_context.Entry(entry).State == EntityState.Detached)
            {
                DbSet.Attach(entry);
            }
            DbSet.Remove(entry);
            Save();
        }

        public virtual int Count()
        {
            return DbSet.Count();
        }

        public DbContext DbContext
        {
            get { return _context; }
        }


        public void Save()
        {
            _context.SaveChanges();
        }
    }
}

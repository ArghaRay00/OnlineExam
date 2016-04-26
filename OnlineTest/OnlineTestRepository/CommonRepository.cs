using System;
using System.Collections.Generic;
using System.Linq;

namespace OnlineTestRepository
{
    public class CommonRepository<T> : IDisposable where T : class
    {

        public void Create(T obj)
        {
            using (var context = new ExamDbContext())
            {
                IRepository<T> repository = new GenericRepository<T>(context);
                repository.Create(obj);
                repository.Save();
            }
        }

        public T Get(int id)
        {
            using (var context = new ExamDbContext())
            {
                IRepository<T> repository = new GenericRepository<T>(context);
                return repository.Find(id);
            }
        }

        public IEnumerable<T> GetAll()
        {
            using (var context = new ExamDbContext())
            {
                IRepository<T> repository = new GenericRepository<T>(context);
                return repository.All().ToList();
            }
        }

        public void Update(T obj)
        {
            using (var context = new ExamDbContext())
            {
                IRepository<T> repository = new GenericRepository<T>(context);
                repository.Update(obj);
                repository.Save();
            }
        }

        public int Count
        {
            get
            {
                using (var context = new ExamDbContext())
                {
                    IRepository<T> repository = new GenericRepository<T>(context);
                    return repository.Count();
                }
            }
        }

        public void Delete(T obj)
        {
            using (var context = new ExamDbContext())
            {
                IRepository<T> repository = new GenericRepository<T>(context);
                repository.Delete(obj);
                repository.Save();
            }
        }

        public void DeleteById(int id)
        {
            using (var context = new ExamDbContext())
            {
                IRepository<T> repository = new GenericRepository<T>(context);
                repository.Delete(id);
                repository.Save();
            }
        }

        public void Dispose()
        {
            //throw new NotImplementedException();
        }
    }
}

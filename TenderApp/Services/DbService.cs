using TenderApp.Data;
using Microsoft.EntityFrameworkCore;

namespace TenderApp.Services
{
    public interface IDbService<T> where T : class
    {
        IEnumerable<T> GetAll();
        T? FindById(int id);

        void Validate(T entity);
        void Add(T entity);
        void Remove(T entity);
        void Update(T entity);
        void Update(T copy, int id);
    }

    public interface ICopyable<T>
    {
        T Clone(T source);
        void CopyTo(T source, T target);
    }

    public abstract class DbService<T>(TenderDbContext db)
        : IDbService<T>, ICopyable<T> where T : class
    {
        protected readonly TenderDbContext _db = db;
        protected readonly DbSet<T> _set = db.Set<T>();

        public virtual IEnumerable<T> GetAll() => [.. _set];

        public T? FindById(int id) => _set.Find(id);

        public void Add(T entity)
        {
            Validate(entity); 
            _set.Add(entity);
            _db.SaveChanges();
        }

        public void Update(T copy, int id)
        {
            Validate(copy);
            T? origin = FindById(id);
            if (origin != null)
            {
                CopyTo(copy, origin);
                _db.SaveChanges();
            }
        }

        // считаем, что entity прошло Validate
        public void Update(T entity)
        {
            Validate(entity);
            _db.SaveChanges();
        }

        public virtual void Remove(T entity)
        {
            try
            {
                _set.Remove(entity);
                _db.SaveChanges();
            }
            catch(DbUpdateException ex)
            {
                throw new InvalidOperationException(GetDeleteErrorMessage(entity), ex);
            }
        }

        public abstract T Clone(T source);

        public abstract void CopyTo(T source, T target);

        public abstract void Validate(T entity);

        protected virtual string GetDeleteErrorMessage(T entity) => "Ошибка при удалении";
            //=> $"Невозможно удалить объект типа {typeof(T).Name}" +
            //   $" — он связан с другими данными.";
    }
}

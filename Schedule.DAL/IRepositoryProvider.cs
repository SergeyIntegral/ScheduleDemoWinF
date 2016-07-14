using System;

namespace Schedule.DAL
{
    public interface IRepositoryProvider : IDisposable
    {
        IRepository<T> GetRepository<T>() where T : class, IDomainObject;
        void SaveChanges();
        void RevertChanges();
    }
}
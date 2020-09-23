using System;
using System.Collections.Generic;
using System.Text;

namespace Maximov.SunMessenger.Core.Interfaces
{
    public interface IWriteRepository<T> : IReadRepository<T> where T:class
    {
        void Create(T entity);
        void Delete(Guid id);
        void Update(T entity);
    }
}

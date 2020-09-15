using System;
using System.Collections.Generic;
using System.Text;

namespace Maximov.SunMessenger.Core.Interfaces.Repositories
{
    public interface IWriteRepository<T>:IReadRepository<T>
    {
        void Create(T entity);
        void Delete(Guid id);
        void Update(T entity);
    }
}

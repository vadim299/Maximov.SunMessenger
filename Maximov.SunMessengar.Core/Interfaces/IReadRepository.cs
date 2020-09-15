using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Maximov.SunMessenger.Core.Interfaces
{
    public interface IReadRepository<T>
    {
        IQueryable<T> GetAll();
        T FindById(Guid id);
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Maximov.SunMessenger.Core.Interfaces
{
    public interface IReadRepository<T> where T:class
    {
        IEnumerable<T> GetAll();
        IEnumerable<T> GetBySpecification(ISpecification<T> specification);
        T FindById(Guid id);
    }
}

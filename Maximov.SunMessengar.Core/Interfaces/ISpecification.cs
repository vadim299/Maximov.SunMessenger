using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Maximov.SunMessenger.Core.Interfaces
{
    public interface ISpecification<T> where T: class
    {
        IQueryable<T> Apply(IQueryable<T> querable);
    }
}

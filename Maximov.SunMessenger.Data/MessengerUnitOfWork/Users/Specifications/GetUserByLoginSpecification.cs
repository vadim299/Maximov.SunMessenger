using Maximov.SunMessenger.Core.Interfaces;
using Maximov.SunMessenger.Core.Users.Objects;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Maximov.SunMessenger.Data.MessengerUnitOfWork.Users.Specifications
{
    public class GetUserByLoginSpecification:ISpecification<User>
    {
        private readonly string login;

        public GetUserByLoginSpecification(string login)
        {
            this.login = login;
        }

        public IQueryable<User> Apply(IQueryable<User> querable)
        {
            return querable.Where(user => user.Login == login);
        }
    }
}

using Maximov.SunMessenger.Core.Interfaces;
using Maximov.SunMessenger.Core.Messages.Objects;
using Maximov.SunMessenger.Core.Users.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Maximov.SunMessenger.Data.MessengerUnitOfWork.Users.Specifications
{
    public class GetUsersByIdSpecification : ISpecification<User>
    {
        private readonly IEnumerable<Guid> userIds;

        public GetUsersByIdSpecification(params Guid[] userIds)
        {
            this.userIds = userIds;
        }

        public IQueryable<User> Apply(IQueryable<User> users)
        {
            return from user in users
                   where userIds.Any(id => id == user.Id)
                   select user;
        }
    }
}

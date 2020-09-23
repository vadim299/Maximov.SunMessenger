using Maximov.SunMessenger.Core.Chats.Objects.Entities;
using Maximov.SunMessenger.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Maximov.SunMessenger.Data.MessengerUnitOfWork.GroupChats.Specifications
{
    public class GetGroupChatsWithActiveUsersSpecification : ISpecification<GroupChat>
    {
        private readonly IEnumerable<Guid> userIds;

        public GetGroupChatsWithActiveUsersSpecification(params Guid[] userIds) 
        {
            this.userIds = userIds;
        }

        public IQueryable<GroupChat> Apply(IQueryable<GroupChat> querable)
        {
            foreach (var userId in userIds)
                querable = from chat in querable
                           where chat.UserLinks.Any(ul => ul.UserId == userId)
                           select chat;
            return querable;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Maximov.SunMessenger.Core.Interfaces;
using Maximov.SunMessenger.Core.Chats.Objects.Entities;
using Maximov.SunMessenger.Core.Users.Objects;

namespace Maximov.SunMessenger.Data.MessengerUnitOfWork.DirectChats.Specifications
{
    public class GetDirectChatWithUsersSpecification : ISpecification<DirectChat>
    {
        private readonly Guid userId1;
        private readonly Guid userId2;

        public GetDirectChatWithUsersSpecification(Guid userId1, Guid userId2)
        {
            this.userId1 = userId1;
            this.userId2 = userId2;
        }

        public GetDirectChatWithUsersSpecification(Guid userId1)
        {
            this.userId1 = userId1;
        }

        public IQueryable<DirectChat> Apply(IQueryable<DirectChat> querable)
        {
            var chats = querable.Where(chat => chat.UserLinks.Any(ul => ul.UserId == userId1));
            if (userId2 != Guid.Empty)
                chats = chats.Where(chat => chat.UserLinks.Any(ul => ul.UserId == userId2));
            return chats;
        }
    }
}

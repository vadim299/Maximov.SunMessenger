using Maximov.SunMessenger.Core.Interfaces;
using Maximov.SunMessenger.Core.Users.Objects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Maximov.SunMessenger.Core.Chats.Objects.ValueObjects
{
    public class UserLink:IEntity
    {
        protected UserLink() { }
        internal UserLink(User user)
        {
            Id = new Guid();
            UserId = user.Id;
        }

        public Guid Id { get; protected set; }

        public Guid UserId { get; protected set; }
    }
}

using Maximov.SunMessenger.Core.Chats.Objects.ValueObjects;
using Maximov.SunMessenger.Core.Messages.Objects;
using Maximov.SunMessenger.Core.Users.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Maximov.SunMessenger.Core.Chats.Objects.Entities
{
    public class DirectChat:Chat
    {
        private readonly List<UserLink> userLinks = new List<UserLink>();

        protected DirectChat(){ }
        public DirectChat(User user1, User user2):base()
        {
            AddUser(user1);
            AddUser(user2);
        }

        public override IEnumerable<UserLink> UserLinks => userLinks;

        private void AddUser(User user)
        {
            if (userLinks.Any(ul => ul.UserId == user.Id))
                throw new ArgumentException("Пользователь уже состоит в чате", nameof(user));

            UserLink userLink = new UserLink(user);
            userLinks.Add(userLink);
        }
    }
}

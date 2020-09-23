using Maximov.SunMessenger.Core.Chats.Objects.ValueObjects;
using Maximov.SunMessenger.Core.Messages.Objects;
using Maximov.SunMessenger.Core.Users.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Maximov.SunMessenger.Core.Chats.Objects.Entities
{
    public class GroupChat:Chat
    {
        private readonly List<UserLink> userLinks = new List<UserLink>();
        private readonly List<UserHistoryItem> userHistory = new List<UserHistoryItem>();

        protected GroupChat() { }
        public GroupChat(User creator, string chatName):base()
        {
            this.CreatorId = creator.Id;
            this.AddUser(creator);
            Name = chatName;
        }

        public string Name { get; protected set; }
        public Guid CreatorId { get; protected set; }
        public IEnumerable<UserHistoryItem> UserHistory => userHistory;

        public override IEnumerable<UserLink> UserLinks => userLinks;

        public void AddUser(User user)
        {
            
            if (userLinks.Any(ul => ul.UserId == user.Id))
                throw new ArgumentException("Пользователь уже состоит в чате", nameof(user));

            UserLink userLink = new UserLink(user);
            userLinks.Add(userLink);

            UserHistoryItem userHistoryItem = new UserHistoryItem(user);
            userHistory.Add(userHistoryItem);
        }
    }
}

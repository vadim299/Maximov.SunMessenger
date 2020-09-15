using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Maximov.SunMessenger.Core.Chats.Objects.ValueObjects;
using Maximov.SunMessenger.Core.Interfaces;
using Maximov.SunMessenger.Core.Messages.Objects;
using Maximov.SunMessenger.Core.Users.Objects;

namespace Maximov.SunMessenger.Core.Chats.Objects.Entities
{
    public abstract class Chat:IEntity
    {
        private readonly List<MessageLink> messageLinks = new List<MessageLink>();

        public Chat()
        {
            this.Id = new Guid();
            this.ActiveFrom = DateTime.UtcNow;
        }

        public Guid Id { get; protected set; }
        public DateTime ActiveFrom { get; protected set; }
        public IEnumerable<MessageLink> MessageLinks => messageLinks;
        public abstract IEnumerable<UserLink> UserLinks { get; }

        public virtual void AddMessage(Message message)
        {
            if (!UserLinks.Any(ul => ul.UserId == message.SenderId))
                throw new ArgumentException("Отправитель не состоит в чате", nameof(message));

            MessageLink messageLink = new MessageLink(message);
            messageLinks.Add(messageLink);
        }
    }
}

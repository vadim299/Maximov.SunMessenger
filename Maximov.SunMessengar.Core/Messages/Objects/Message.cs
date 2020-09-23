using Maximov.SunMessenger.Core.Chats.Objects.Entities;
using Maximov.SunMessenger.Core.Interfaces;
using Maximov.SunMessenger.Core.Users.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Maximov.SunMessenger.Core.Messages.Objects
{
    public class Message : IEntity
    {
        private List<ChatLink> chatLinks = new List<ChatLink>();

        protected Message() { }
        public Message(User sender, string text, Chat chat) : this(sender, text, new Chat[] { chat }) { }
        public Message(User sender, string text, IEnumerable<Chat> chats)
        {
            this.Id = new Guid();
            this.Text = string.IsNullOrWhiteSpace(text) ? throw new ArgumentNullException(nameof(text)) : text;
            this.SenderId = sender.Id;
            ActiveFrom = DateTime.UtcNow;
            ChangeDate = ActiveFrom;
            foreach(var chat in chats)
            {
                if (!chat.UserLinks.Any(ul => ul.UserId == SenderId))
                    throw new ArgumentException("Чат не содержит отправителя", nameof(chats));

                if (ChatLinks.Any(cl => cl.ChatId == chat.Id))
                    continue;

                ChatLink chatLink = new ChatLink(chat);
                chatLinks.Add(chatLink);
            }
        }

        public Guid Id { get; protected set; }
        public string Text { get; protected set; }
        public DateTime ActiveFrom { get; protected set; }
        public DateTime ChangeDate { get; protected set; }
        public Guid SenderId { get; protected set; }
        public IEnumerable<ChatLink> ChatLinks { get => chatLinks; }

        public void ChangeText(string text)
        {
            this.Text = string.IsNullOrWhiteSpace(text) ? throw new ArgumentNullException(nameof(text)) : text;
            this.ChangeDate = DateTime.UtcNow;
        }
    }
}
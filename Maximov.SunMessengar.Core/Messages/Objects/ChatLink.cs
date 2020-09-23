using Maximov.SunMessenger.Core.Chats.Objects.Entities;
using Maximov.SunMessenger.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Maximov.SunMessenger.Core.Messages.Objects
{
    public class ChatLink:IEntity
    {
        protected ChatLink() { }
        internal ChatLink(Chat chat)
        {
            Id = new Guid();
            ChatId = chat.Id;
        }

        public Guid Id { get; protected set; }

        public Guid ChatId { get; protected set; }
    }
}

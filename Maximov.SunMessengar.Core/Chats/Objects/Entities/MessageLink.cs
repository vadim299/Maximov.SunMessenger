using Maximov.SunMessenger.Core.Interfaces;
using Maximov.SunMessenger.Core.Messages.Objects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Maximov.SunMessenger.Core.Chats.Objects.ValueObjects
{
    public class MessageLink:IEntity
    {
        protected MessageLink() { }
        internal MessageLink(Message message)
        {
            Id = new Guid();
            MessageId = message.Id;
        }

        public Guid Id { get; protected set; }

        public Guid MessageId { get; protected set; }
    }
}

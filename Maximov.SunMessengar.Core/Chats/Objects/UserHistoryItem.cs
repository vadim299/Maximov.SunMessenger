using Maximov.SunMessenger.Core.Chats.Objects.ValueObjects;
using Maximov.SunMessenger.Core.Interfaces;
using Maximov.SunMessenger.Core.Users.Objects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Maximov.SunMessenger.Core.Chats.Objects.Entities
{
    public class UserHistoryItem:IEntity
    {
        protected UserHistoryItem() { }

        public UserHistoryItem(User user)
        {
            Id = new Guid();
            UserId = user.Id;
            DateFrom = DateTime.UtcNow;
        }

        public Guid Id { get; protected set; }
        public Guid UserId { get; protected set; }
        public DateTime DateFrom { get; protected set; }
        public DateTime? DateTo { get; protected set; }

        internal void Close()
        {
            if (DateTo == null)
                throw new InvalidOperationException("Пользователь уже был отключен");

            DateTo = DateTime.UtcNow;
        }
    }
}

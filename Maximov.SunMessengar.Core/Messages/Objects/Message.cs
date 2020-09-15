using Maximov.SunMessenger.Core.Interfaces;
using Maximov.SunMessenger.Core.Users.Objects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Maximov.SunMessenger.Core.Messages.Objects
{
    public class Message:IEntity
    {
        protected Message() { }
        public Message(User sender, string text)
        {
            this.Id = new Guid();
            this.Text = string.IsNullOrWhiteSpace(text) ? throw new ArgumentNullException(nameof(text)) : text;
            this.SenderId = sender.Id;
            ActiveFrom = DateTime.UtcNow;
            ChangeDate = ActiveFrom;
        }

        public Guid Id { get; protected set; }
        public string Text { get; protected set; }
        public DateTime ActiveFrom { get; protected set; }
        public DateTime ChangeDate { get; protected set; }
        public Guid SenderId { get; protected set; }

        public void ChangeText(string text)
        {
            this.Text = string.IsNullOrWhiteSpace(text) ? throw new ArgumentNullException(nameof(text)) : text;
            this.ChangeDate = DateTime.UtcNow;
        }
    }
}
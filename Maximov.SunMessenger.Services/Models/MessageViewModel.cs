using System;
using System.Collections.Generic;
using System.Text;

namespace Maximov.SunMessenger.Services.Models
{
    public class MessageViewModel
    {
        public enum MessageType { Message, Join, Out}

        public string Message { get; set; }

        public MessageType Type { get; set; }

        public DateTime ActiveFrom { get; set; }
    }
}

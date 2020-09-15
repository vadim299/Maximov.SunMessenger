using System;
using System.Collections.Generic;
using System.Text;
using Maximov.SunMessenger.Core;

namespace Maximov.SunMessenger.Services.Models
{
    public class DialogSimpleInfo
    {
        public string ChatName { get; set; }

        public Message LastMessage { get; set; }

        public Chat Chat { get; set; }
    }
}

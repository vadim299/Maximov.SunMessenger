using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Maximov.SunMessenger.Web.Models.Shared.Chat
{
    public class ChatViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<Guid> UserIds { get; set; }
    }
}

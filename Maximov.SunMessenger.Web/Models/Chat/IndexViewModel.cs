using Maximov.SunMessenger.Web.Models.Shared.Chat;
using Maxinov.SunMessenger.Services.ChatService.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Maximov.SunMessenger.Web.Models.Chat
{
    public class IndexViewModel
    {
        public IEnumerable<(ChatViewModel, MessageDto)> ChatsWithLastMessage { get; set; }
    }
}

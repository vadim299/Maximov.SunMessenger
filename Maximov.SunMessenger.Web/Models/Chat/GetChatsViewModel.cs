using Maximov.SunMessenger.Web.Models.Shared.Chat;
using Maximov.SunMessenger.Services.ChatService.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Maximov.SunMessenger.Web.Models.Chat
{
    public class GetChatsViewModel
    {
        public ChatViewModel Chat { get; set; }
        public MessageDto Message { get; set; }
    }
}

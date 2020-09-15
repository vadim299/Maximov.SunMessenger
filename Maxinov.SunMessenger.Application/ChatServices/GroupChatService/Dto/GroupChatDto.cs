using Maxinov.SunMessenger.Services.ChatServices.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Maxinov.SunMessenger.Services.ChatService.DTO
{
    public class GroupChatDto:ChatDto
    {
        public Guid CreatorId { get; set; }
        public string ChatName { get; set; }
    }
}

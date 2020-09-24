using Maximov.SunMessenger.Services.ChatServices.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Maximov.SunMessenger.Services.ChatService.DTO
{
    public class GroupChatDto
    {
        public Guid Id { get; set; }
        public IEnumerable<Guid> UserIds { get; set; }
        public DateTime Date { get; set; }
        public Guid CreatorId { get; set; }
        public string ChatName { get; set; }
    }
}

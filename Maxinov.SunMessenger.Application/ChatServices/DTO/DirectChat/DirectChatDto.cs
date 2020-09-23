using Maxinov.SunMessenger.Services.ChatServices.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Maxinov.SunMessenger.Services.ChatService.DTO
{
    public class DirectChatDto
    {
        public Guid Id { get; set; }
        public IEnumerable<Guid> UserIds { get; set; }
        public DateTime Date { get; set; }
    }
}

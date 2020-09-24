using System;
using System.Collections.Generic;
using System.Text;

namespace Maximov.SunMessenger.Services.ChatService.DTO
{
    public class MessageDto
    {
        public Guid Id { get; set; }
        public Guid SenderId { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
        public IEnumerable<Guid> ChatIds { get; set; }
    }
}

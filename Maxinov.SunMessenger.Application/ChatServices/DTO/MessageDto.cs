﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Maxinov.SunMessenger.Services.ChatService.DTO
{
    public class MessageDto
    {
        public Guid Id { get; set; }
        public Guid SenderId { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
    }
}

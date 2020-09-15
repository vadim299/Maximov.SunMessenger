﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Maxinov.SunMessenger.Services.ChatServices.DTO
{
    public class ChatDto
    {
        public Guid Id { get; set; }
        public IEnumerable<Guid> UserIds { get; set; }
        public DateTime Date { get; set; }
    }
}
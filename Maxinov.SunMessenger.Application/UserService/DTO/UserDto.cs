using System;
using System.Collections.Generic;
using System.Text;

namespace Maxinov.SunMessenger.Services.DTO
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string Login { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
    }
}

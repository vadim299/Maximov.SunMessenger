using System;
using System.Collections.Generic;
using System.Text;
using Maximov.SunMessenger.Core.Chats.Objects.Entities;
using Maximov.SunMessenger.Core.Interfaces.Repositories;
using Maximov.SunMessenger.Core.Messages.Objects;
using Maximov.SunMessenger.Core.Users.Objects;

namespace Maximov.SunMessenger.Core.Interfaces
{
    public interface IMessengerUnit
    {
        IWriteRepository<DirectChat> DirectChats { get; }
        IWriteRepository<GroupChat> GroupChats { get; }
        IMessageRepository Messages { get; }
        IWriteRepository<User> Users { get; }

        void SaveChanges();
    }
}

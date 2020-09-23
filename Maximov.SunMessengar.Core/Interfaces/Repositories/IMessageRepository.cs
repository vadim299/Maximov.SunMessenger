using Maximov.SunMessenger.Core.Messages.Objects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Maximov.SunMessenger.Core.Interfaces.Repositories
{
    public interface IMessageRepository:IWriteRepository<Message>
    {
        IEnumerable<Message> GetLastMessages(Guid userId, Guid[] chatIds);

        IEnumerable<Message> GetMessages(Guid userId, Guid chatId);
    }
}

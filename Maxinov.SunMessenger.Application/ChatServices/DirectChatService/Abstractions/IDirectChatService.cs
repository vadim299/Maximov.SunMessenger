using Maximov.SunMessenger.Core.Chats.Objects.Entities;
using Maximov.SunMessenger.Core.Messages.Objects;
using Maxinov.SunMessenger.Services.ChatService.DTO;
using System;
using System.Collections.Generic;

namespace Maxinov.SunMessenger.Services.DirectChatService
{
    public interface IDirectChatService
    {
        MessageDto AddMessage(Guid chatId, Guid senderId, string text);
        DirectChatDto Create(Guid userId1, Guid userId2);
        DirectChatDto FindById(Guid chatId);
        DirectChatDto FindByUsers(Guid userId1, Guid userId2);
        IEnumerable<DirectChatDto> GetChats(Guid userId);
        IEnumerable<MessageDto> GetMessages(Guid chatId, Guid userId);
        IEnumerable<(DirectChatDto Chat, MessageDto Message)> GetChatAndLastMessageList(Guid userId);
    }
}
using Maxinov.SunMessenger.Services.ChatService.DTO;
using System;
using System.Linq;

namespace Maxinov.SunMessenger.Services.DirectChatService
{
    public interface IDirectChatService
    {
        MessageDto AddMessage(Guid chatId, Guid senderId, string text);
        DirectChatDto Create(Guid userId1, Guid userId2);
        DirectChatDto FindById(Guid chatId);
        DirectChatDto FindByUsers(Guid userId1, Guid userId2);
        IQueryable<DirectChatDto> GetChats(Guid userId);
        IQueryable<MessageDto> GetMessages(Guid chatId, Guid userId);
    }
}
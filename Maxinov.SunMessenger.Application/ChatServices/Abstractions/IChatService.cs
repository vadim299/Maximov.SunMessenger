using Maxinov.SunMessenger.Services.ChatService.DTO;
using Maxinov.SunMessenger.Services.ChatServices.DTO;
using Maxinov.SunMessenger.Services.DirectChatService;
using Maxinov.SunMessenger.Services.GroupChatService.Abstractions;
using System;
using System.Linq;

namespace Maxinov.SunMessenger.Services.ChatServices
{
    public interface IChatService
    {
        IDirectChatService DirectChatService { get; }
        IGroupChatService GroupChatService { get; }

        MessageDto AddMessage(Guid chatId, Guid senderId, string text);
        ChatDto FindById(Guid chatId);
        IQueryable<ChatDto> GetChats(Guid userId);
        IQueryable<MessageDto> GetMessages(Guid chatId, Guid userId);
    }
}
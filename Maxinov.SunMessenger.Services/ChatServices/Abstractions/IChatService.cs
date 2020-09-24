using Maximov.SunMessenger.Services.ChatService.DTO;
using Maximov.SunMessenger.Services.ChatServices.DTO;
using Maximov.SunMessenger.Services.DirectChatService;
using Maximov.SunMessenger.Services.GroupChatService.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Maximov.SunMessenger.Services.ChatServices
{
    public interface IChatService
    {
        IDirectChatService DirectChatService { get; }
        IGroupChatService GroupChatService { get; }

        MessageDto AddMessage(Guid chatId, Guid senderId, string text);
        ChatDto FindById(Guid chatId);
        IEnumerable<ChatDto> GetChats(Guid userId);
        IEnumerable<MessageDto> GetMessages(Guid chatId, Guid userId);
        IEnumerable<(ChatDto Chat, MessageDto LastMessage)> GetChatAndLastMessageList(Guid userId);
    }
}
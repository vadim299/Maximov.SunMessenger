using Maximov.SunMessenger.Services.ChatService.DTO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Maximov.SunMessenger.Services.GroupChatService.Abstractions
{
    public interface IGroupChatService
    {
        MessageDto AddMessage(Guid chatId, Guid senderId, string text);
        void AddUser(Guid chatId, Guid userId);
        GroupChatDto Create(Guid creatorId, string chatName);
        IEnumerable<GroupChatDto> GetChats(Guid userId);
        IEnumerable<(GroupChatDto Chat, MessageDto Message)> GetChatAndLastMessageList(Guid userId);
        IEnumerable<MessageDto> GetMessages(Guid chatId, Guid userId);
        GroupChatDto FindById(Guid chatId);
    }
}
using Maxinov.SunMessenger.Services.ChatService.DTO;
using System;
using System.Linq;

namespace Maxinov.SunMessenger.Services.GroupChatService.Abstractions
{
    public interface IGroupChatService
    {
        MessageDto AddMessage(Guid chatId, Guid senderId, string text);
        void AddUser(Guid chatId, Guid userId);
        GroupChatDto Create(Guid creatorId, string chatName);
        IQueryable<GroupChatDto> GetChats(Guid userId);

        /// <summary>
        /// Получает сообщения для чата отсортированные по убыванию date
        /// </summary>
        /// <param name="chatId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        IQueryable<MessageDto> GetMessages(Guid chatId, Guid userId);
        GroupChatDto FindById(Guid chatId);
    }
}
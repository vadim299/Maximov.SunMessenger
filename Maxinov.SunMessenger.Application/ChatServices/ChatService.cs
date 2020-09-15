using Maxinov.SunMessenger.Services.ChatService.DTO;
using Maxinov.SunMessenger.Services.ChatServices.DTO;
using Maxinov.SunMessenger.Services.DirectChatService;
using Maxinov.SunMessenger.Services.GroupChatService.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Maxinov.SunMessenger.Services.ChatServices
{
    public class ChatService : IChatService
    {
        private readonly IDirectChatService directChatService;
        private readonly IGroupChatService groupChatService;

        public ChatService(IDirectChatService directChatService, IGroupChatService groupChatService)
        {
            this.directChatService = directChatService;
            this.groupChatService = groupChatService;
        }

        public IDirectChatService DirectChatService { get => directChatService; }
        public IGroupChatService GroupChatService { get => groupChatService; }

        public ChatDto FindById(Guid chatId)
        {
            return (ChatDto)directChatService.FindById(chatId) ?? groupChatService.FindById(chatId);
        }

        public MessageDto AddMessage(Guid chatId, Guid senderId, string text)
        {
            if (directChatService.FindById(chatId) != null)
                return directChatService.AddMessage(chatId, senderId, text);
            else
                return groupChatService.AddMessage(chatId, senderId, text);
        }

        public IQueryable<ChatDto> GetChats(Guid userId)
        {
            return directChatService.GetChats(userId).Cast<ChatDto>()
                .Union(groupChatService.GetChats(userId));
        }

        public IQueryable<MessageDto> GetMessages(Guid chatId, Guid userId)
        {
            if (directChatService.FindById(chatId) != null)
                return directChatService.GetMessages(chatId, userId);
            else
                return groupChatService.GetMessages(chatId, userId);
        }
    }
}

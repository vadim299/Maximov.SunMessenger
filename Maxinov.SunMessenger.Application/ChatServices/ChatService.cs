using AutoMapper;
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
        private readonly IMapper mapper;

        public ChatService(IDirectChatService directChatService, IGroupChatService groupChatService, IMapper mapper)
        {
            this.DirectChatService = directChatService;
            this.GroupChatService = groupChatService;
            this.mapper = mapper;
        }

        public IDirectChatService DirectChatService { get; }
        public IGroupChatService GroupChatService { get; }

        public ChatDto FindById(Guid chatId)
        {
            return mapper.Map<ChatDto>(DirectChatService.FindById(chatId))
                ?? mapper.Map<ChatDto>(GroupChatService.FindById(chatId));
        }

        public MessageDto AddMessage(Guid chatId, Guid senderId, string text)
        {
            if (FindById(chatId)?.ChatType == ChatTypes.Direct)
                return DirectChatService.AddMessage(chatId, senderId, text);
            else return GroupChatService.AddMessage(chatId, senderId, text);
        }

        public IEnumerable<ChatDto> GetChats(Guid userId)
        {
            return mapper.Map<IEnumerable<DirectChatDto>,IEnumerable<ChatDto>>(DirectChatService.GetChats(userId))
                .Union(mapper.Map<IEnumerable<GroupChatDto>, IEnumerable<ChatDto>>(GroupChatService.GetChats(userId)));
        }

        public IEnumerable<MessageDto> GetMessages(Guid chatId, Guid userId)
        {
            if (FindById(chatId)?.ChatType==ChatTypes.Direct)
                return DirectChatService.GetMessages(chatId, userId);
            else
                return GroupChatService.GetMessages(chatId, userId);
        }

        public IEnumerable<(ChatDto Chat, MessageDto LastMessage)> GetChatAndLastMessageList(Guid userId)
        {
            var directChatMessageList = from item in DirectChatService.GetChatAndLastMessageList(userId)
                                        select (mapper.Map<DirectChatDto, ChatDto>(item.Chat), item.Message);
            var groupChatMessageList = from item in GroupChatService.GetChatAndLastMessageList(userId)
                                       select (mapper.Map<GroupChatDto, ChatDto>(item.Chat), item.Message);

            return directChatMessageList.Union(groupChatMessageList);
        }
    }
}

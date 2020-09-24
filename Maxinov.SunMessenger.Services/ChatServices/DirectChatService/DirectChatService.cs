using AutoMapper;
using AutoMapper.QueryableExtensions;
using Maximov.SunMessenger.Core.Chats.Objects.Entities;
using Maximov.SunMessenger.Core.Interfaces;
using Maximov.SunMessenger.Core.Messages.Objects;
using Maximov.SunMessenger.Core.Users.Objects;
using Maximov.SunMessenger.Data.MessengerUnitOfWork.DirectChats.Specifications;
using Maximov.SunMessenger.Services.ChatService.DTO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Maximov.SunMessenger.Services.DirectChatService
{
    public class DirectChatService : IDirectChatService
    {
        private readonly IMessengerUnit messengerUnit;
        private readonly IMapper mapper;

        public DirectChatService(IMessengerUnit messengerUnit, IMapper mapper)
        {
            this.messengerUnit = messengerUnit;
            this.mapper = mapper;
        }

        #region Queries

        public DirectChatDto FindById(Guid chatId)
        {
            var chat = messengerUnit.DirectChats.FindById(chatId);
            return mapper.Map<DirectChat, DirectChatDto>(chat);
        }

        public DirectChatDto FindByUsers(Guid userId1, Guid userId2)
        {
            var specification = new GetDirectChatWithUsersSpecification(userId1, userId2);
            var chat = messengerUnit.DirectChats.GetBySpecification(specification)
                       .FirstOrDefault();
            return mapper.Map<DirectChat, DirectChatDto>(chat);
        }

        public IEnumerable<DirectChatDto> GetChats(Guid userId)
        {
            var specification = new GetDirectChatWithUsersSpecification(userId);
            var chats = messengerUnit.DirectChats.GetBySpecification(specification);

            return mapper.Map<IEnumerable<DirectChat>, IEnumerable<DirectChatDto>>(chats);
        }

        public IEnumerable<MessageDto> GetMessages(Guid chatId, Guid userId)
        {
            var chat = messengerUnit.DirectChats.FindById(chatId);
            if (chat?.UserLinks.Any(ul => ul.UserId == userId)!=true)
                return Enumerable.Empty<MessageDto>();

            var messages = messengerUnit.Messages.GetMessages(userId, chatId);

            return mapper.Map<IEnumerable<Message>, IEnumerable<MessageDto>>(messages);
        }

        public IEnumerable<(DirectChatDto, MessageDto)> GetChatAndLastMessageList(Guid userId)
        {
            var chatSpecification = new GetDirectChatWithUsersSpecification(userId);
            var chats = messengerUnit.DirectChats.GetBySpecification(chatSpecification);
            var chatDtos = mapper.Map<IEnumerable<DirectChat>, IEnumerable<DirectChatDto>>(chats);
            var chatIds = chats.Select(c => c.Id);
            
            var messages = messengerUnit.Messages.GetLastMessages(userId, chatIds.ToArray());
            var messageDtos = mapper.Map<IEnumerable<Message>, IEnumerable<MessageDto>>(messages);

            var chatMessageList = from chat in chatDtos
                                  from message in messageDtos
                                  where message.ChatIds.Any(id => id == chat.Id)
                                  select (chat, message);
            return chatMessageList;
        }

        #endregion

        #region Commands

        public DirectChatDto Create(Guid userId1, Guid userId2)
        {
            User user1 = messengerUnit.Users.FindById(userId1);
            User user2 = messengerUnit.Users.FindById(userId2);

            var specification = new GetDirectChatWithUsersSpecification(userId1, userId2);

            if (messengerUnit.DirectChats.GetBySpecification(specification)
                .Count() > 0)
                throw new ArgumentException("Чат c этими пользователем уже существует");

            DirectChat directChat = new DirectChat(user1, user2);

            messengerUnit.DirectChats.Create(directChat);
            messengerUnit.SaveChanges();

            return mapper.Map<DirectChat, DirectChatDto>(directChat);
        }

        public MessageDto AddMessage(Guid chatId, Guid senderId, string text)
        {
            User sender = messengerUnit.Users.FindById(senderId);
            DirectChat chat = messengerUnit.DirectChats.FindById(chatId);
            Message message = new Message(sender, text, chat);

            messengerUnit.Messages.Create(message);
            messengerUnit.SaveChanges();

            return mapper.Map<Message, MessageDto>(message);
        }

        #endregion
    }
}

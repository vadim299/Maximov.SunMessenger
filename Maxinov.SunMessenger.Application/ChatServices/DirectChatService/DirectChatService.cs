using AutoMapper;
using AutoMapper.QueryableExtensions;
using Maximov.SunMessenger.Core.Chats.Objects.Entities;
using Maximov.SunMessenger.Core.Chats.Queries;
using Maximov.SunMessenger.Core.Interfaces;
using Maximov.SunMessenger.Core.Messages.Objects;
using Maximov.SunMessenger.Core.Users.Objects;
using Maxinov.SunMessenger.Services.ChatService.DTO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Maxinov.SunMessenger.Services.DirectChatService
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

            return mapper.Map<DirectChatDto>(chat);
        }

        public DirectChatDto FindByUsers(Guid userId1, Guid userId2)
        {
            DirectChat directChat = messengerUnit.DirectChats.GetAll()
                .WithUser(userId1)
                .WithUser(userId2)
                .FirstOrDefault();
            return mapper.Map<DirectChatDto>(directChat);
        }

        public IQueryable<DirectChatDto> GetChats(Guid userId)
        {
            User user = messengerUnit.Users.FindById(userId);

            IQueryable<DirectChat> chats = messengerUnit.DirectChats.GetAll().WithUser(userId);

            IQueryable < DirectChatDto > chatsDto = mapper.ProjectTo<DirectChatDto>(chats);

            return chatsDto;
        }

        public IQueryable<MessageDto> GetMessages(Guid chatId, Guid userId)
        {
            IQueryable<DirectChat> directChat = messengerUnit.DirectChats.GetAll()
                .WithUser(userId).Where(c => c.Id == chatId);

            IQueryable<Message> messages = from chat in directChat
                                           from messageLink in chat.MessageLinks
                                           join message in messengerUnit.Messages.GetAll() on messageLink.MessageId equals message.Id
                                           orderby message.ActiveFrom descending
                                           select message;

            IQueryable<MessageDto> messageDtos = mapper.ProjectTo<MessageDto>(messages);

            return messageDtos;
        }

        #endregion

        #region Commands

        public DirectChatDto Create(Guid userId1, Guid userId2)
        {
            User user1 = messengerUnit.Users.FindById(userId1);
            User user2 = messengerUnit.Users.FindById(userId2);

            if (messengerUnit.DirectChats.GetAll()
                .WithUser(userId1)
                .WithUser(userId2)
                .Count() > 0)
                throw new ArgumentException("Чат c этими пользователем уже существует");

            DirectChat directChat = new DirectChat(user1, user2);

            messengerUnit.DirectChats.Create(directChat);
            messengerUnit.SaveChanges();

            DirectChatDto directChatDto = mapper.Map<DirectChatDto>(directChat);
            return directChatDto;
        }

        public MessageDto AddMessage(Guid chatId, Guid senderId, string text)
        {
            User sender = messengerUnit.Users.FindById(senderId);
            DirectChat chat = messengerUnit.DirectChats.FindById(chatId);
            Message message = new Message(sender, text);

            messengerUnit.Messages.Create(message); //TODO: проблема с последовательностью добавления данных
            try
            {
                chat.AddMessage(message);
            }
            catch
            {
                messengerUnit.Messages.Delete(message.Id);
            }
            messengerUnit.SaveChanges();

            MessageDto messageDto = mapper.Map<MessageDto>(message);
            return messageDto;
        }

        #endregion
    }
}

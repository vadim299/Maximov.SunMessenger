using Maximov.SunMessenger.Core.Chats.Objects.Entities;
using Maximov.SunMessenger.Core.Chats.Objects.ValueObjects;
using Maximov.SunMessenger.Core.Chats.Queries;
using Maximov.SunMessenger.Core.Interfaces;
using Maximov.SunMessenger.Core.Messages.Objects;
using Maximov.SunMessenger.Core.Users.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Maxinov.SunMessenger.Services.ChatService.DTO;
using Maxinov.SunMessenger.Services.GroupChatService.Abstractions;
using AutoMapper;

namespace Maxinov.SunMessenger.Services.GroupChatService
{
    public class GroupChatService : IGroupChatService
    {
        private readonly IMessengerUnit messengerUnit;
        private readonly IMapper mapper;

        public GroupChatService(IMessengerUnit messengerUnit, IMapper mapper)
        {
            this.messengerUnit = messengerUnit;
            this.mapper = mapper;
        }

        #region Queries

        public IQueryable<GroupChatDto> GetChats(Guid userId)
        {   
            IQueryable<GroupChat> groupChats = messengerUnit.GroupChats.GetAll()
                                                                       .WithUser(userId);

            IQueryable<GroupChatDto> chatDtos = mapper.ProjectTo<GroupChatDto>(groupChats);
            
            return chatDtos;
        }

        public IQueryable<MessageDto> GetMessages(Guid chatId, Guid userId)
        {

            IQueryable<Message> messages = from chat in messengerUnit.GroupChats.GetAll()
                                           from userHistory in chat.UserHistory
                                           from messageLink in chat.MessageLinks
                                           join message in messengerUnit.Messages.GetAll() on messageLink.MessageId equals message.Id
                                           where chat.Id == chatId && userHistory.UserId == userId
                                                 && message.ActiveFrom >= userHistory.DateFrom
                                                 && message.ActiveFrom <= (userHistory.DateTo ?? DateTime.MaxValue)
                                           select message;

            IQueryable<MessageDto> messageDtos = mapper.ProjectTo<MessageDto>(messages);

            return messageDtos;
        }

        public GroupChatDto FindById(Guid chatId)
        {
            GroupChat chat = messengerUnit.GroupChats.FindById(chatId);
            return mapper.Map<GroupChatDto>(chat);
        }

        #endregion

        #region Commands

        public GroupChatDto Create(Guid creatorId, string chatName)
        {
            User creator = messengerUnit.Users.FindById(creatorId);
            GroupChat groupChat = new GroupChat(creator, chatName);

            messengerUnit.GroupChats.Create(groupChat);
            messengerUnit.SaveChanges();

            GroupChatDto groupChatDto = mapper.Map<GroupChatDto>(groupChat);
            return groupChatDto;
        }

        public void AddUser(Guid chatId, Guid userId)
        {
            User user = messengerUnit.Users.FindById(userId);
            GroupChat groupChat = messengerUnit.GroupChats.FindById(chatId);

            groupChat.AddUser(user);
            messengerUnit.GroupChats.Update(groupChat);
            messengerUnit.SaveChanges();
        }

        public MessageDto AddMessage(Guid chatId, Guid senderId, string text)
        {
            GroupChat chat = messengerUnit.GroupChats.FindById(chatId);
            User user = messengerUnit.Users.FindById(senderId);
            Message message = new Message(user, text);

            chat.AddMessage(message);
            messengerUnit.Messages.Create(message);
            messengerUnit.GroupChats.Update(chat);
            messengerUnit.SaveChanges();

            MessageDto messageDto = mapper.Map<MessageDto>(message);
            return messageDto;
        }

        #endregion
    }
}

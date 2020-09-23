using Maximov.SunMessenger.Core.Chats.Objects.Entities;
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
using Maximov.SunMessenger.Data.MessengerUnitOfWork.GroupChats.Specifications;

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

        public IEnumerable<GroupChatDto> GetChats(Guid userId)
        {
            var specification = new GetGroupChatsWithUsersSpecification(userId);

            IEnumerable<GroupChat> groupChats = messengerUnit.GroupChats.GetBySpecification(specification);
            
            return mapper.Map<IEnumerable<GroupChat>, IEnumerable<GroupChatDto>>(groupChats);
        }

        public IEnumerable<MessageDto> GetMessages(Guid chatId, Guid userId)
        {
            GroupChat groupChat = messengerUnit.GroupChats.FindById(chatId);

            if (groupChat?.UserHistory.Any(uh => uh.UserId == userId)!=true)
                return Enumerable.Empty<MessageDto>();

            var messages = messengerUnit.Messages.GetMessages(userId, groupChat.Id);

            return mapper.Map<IEnumerable<Message>, IEnumerable<MessageDto>>(messages);
        }

        public IEnumerable<(GroupChatDto, MessageDto)> GetChatAndLastMessageList(Guid userId)
        {
            var chatSpecification = new GetGroupChatsWithUsersSpecification(userId);
            IEnumerable<GroupChat> chats = messengerUnit.GroupChats.GetBySpecification(chatSpecification);
            var chatDtos = mapper.Map<IEnumerable<GroupChat>, IEnumerable<GroupChatDto>>(chats);
            var chatIds = chats.Select(c => c.Id).ToArray();

            var messages = messengerUnit.Messages.GetLastMessages(userId, chatIds);
            var messageDtos = mapper.Map<IEnumerable<Message>, IEnumerable<MessageDto>>(messages);
            var chatMessageList = from chat in chatDtos
                                  from message in messageDtos
                                  where message.ChatIds.Any(chatId => chatId == chat.Id)
                                  select (chat, message);
            return chatMessageList;
        }

        public GroupChatDto FindById(Guid chatId)
        {
            GroupChat chat = messengerUnit.GroupChats.FindById(chatId);
            return mapper.Map<GroupChat, GroupChatDto>(chat);
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
            messengerUnit.SaveChanges();
        }

        public MessageDto AddMessage(Guid chatId, Guid senderId, string text)
        {
            GroupChat chat = messengerUnit.GroupChats.FindById(chatId);
            User user = messengerUnit.Users.FindById(senderId);
            Message message = new Message(user, text, chat);

            messengerUnit.Messages.Create(message);
            messengerUnit.SaveChanges();

            MessageDto messageDto = mapper.Map<MessageDto>(message);
            return messageDto;
        }

        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Maximov.SunMessenger.Core;
using Maximov.SunMessenger.Core.Managers;
using Maximov.SunMessenger.Services.Models;

namespace Maximov.SunMessenger.Services
{
    public class DialogService
    {
        private readonly ChatManager chatManager;
        private readonly UserManager userManager;

        public DialogService(ChatManager chatManager)
        {
            this.chatManager=chatManager;
        }

        public IQueryable<DialogSimpleInfo> GetChatsAndLastMessageByUser(User user)
        {
            var res = from chat in chatManager.GetChats(user.Id)
                      from message in chatManager.GetMessages(user.Id, chat.Id)
                      where message.ActiveFrom == chatManager.GetMessages(user.Id, chat.Id).Max(m => m.ActiveFrom)
                      orderby message.ActiveFrom descending
                      select new DialogSimpleInfo()
                      {
                          Chat = chat,
                          LastMessage = message,
                          ChatName = chat is GroupChat ? "" : chat.UserChats.First(uc => uc.User != user).User.Name
                      };
            return res;
        }

        public IQueryable<MessageViewModel> GetMessagesWithChatEvents(Guid userId, Guid chatId)
        {
            var messageViews = from message in chatManager.GetMessages(userId, chatId)
                               select new MessageViewModel()
                               {
                                   Message = message.Text,
                                   ActiveFrom = message.ActiveFrom,
                                   Type = MessageViewModel.MessageType.Message
                               };
            var connections = from userChat in chatManager.GetConnections(userId, chatId)
                              select new MessageViewModel()
                              {
                                  ActiveFrom = userChat.ActiveFrom,
                                  Message = $"Пользователь {userManager.FindById(userChat.UserId).Name} присоединился к чату",
                                  Type = MessageViewModel.MessageType.Join
                              };
            var disconnections = from userChat in chatManager.GetDisconnections(userId, chatId)
                                 select new MessageViewModel()
                                 {
                                     ActiveFrom = userChat.ActiveTo.Value,
                                     Message = $"Пользователь {userManager.FindById(userChat.UserId).Name} отсоединился от чата",
                                     Type = MessageViewModel.MessageType.Out
                                 };
            return messageViews.Union(connections)
                .Union(disconnections)
                .OrderByDescending(mv => mv.ActiveFrom);
        }
    }
}

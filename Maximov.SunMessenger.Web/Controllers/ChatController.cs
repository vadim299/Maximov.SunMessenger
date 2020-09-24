using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Maximov.SunMessenger.Web.Services.AuthenticationService.Abstractions;
using Maximov.SunMessenger.Web.Models.Chat;
using Maximov.SunMessenger.Services.UserService.Abstractions;
using Maximov.SunMessenger.Services.ChatService;
using Maximov.SunMessenger.Services.ChatService.DTO;
using Maximov.SunMessenger.Services.DirectChatService;
using Maximov.SunMessenger.Services.GroupChatService.Abstractions;
using Maximov.SunMessenger.Web.Models.Shared.Chat;
using Maximov.SunMessenger.Web.Models.Shared.User;
using Maximov.SunMessenger.Services.ChatServices;
using AutoMapper;

namespace Maximov.SunMessenger.Web.Controllers
{
    [Authorize]
    public class ChatController : Controller
    {
        private readonly IAuthenticationService authenticationService;
        private readonly IUserService userService;
        private readonly IChatService chatService;
        private readonly IDirectChatService directChatService;
        private readonly IGroupChatService groupChatService;
        private readonly IMapper mapper;

        public ChatController(IUserService userService,
                              IAuthenticationService authenticationService,
                              IChatService chatService,
                              IMapper mapper)
        {
            this.mapper = mapper;
            this.userService = userService;
            this.chatService = chatService;
            this.directChatService = chatService.DirectChatService;
            this.groupChatService = chatService.GroupChatService;
            this.authenticationService = authenticationService;
        }

        //отрефакторено

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CreateDirectChat()
        {
            return PartialView("_CreateDirect");
        }

        #region Ajax requests

        #endregion

        /// <summary>
        /// Получает чаты для пользователя отсортированные от недавно обновленного к самому старому
        /// </summary>
        /// <returns></returns>
        public JsonResult GetChats()
        {
            Guid userId = authenticationService.GetUserId();
            var directChatAndLastMessageList = directChatService.GetChatAndLastMessageList(userId);
            var groupChatAndLastMessageList = groupChatService.GetChatAndLastMessageList(userId);
            List<GetChatsViewModel> getChatsViewModels = new List<GetChatsViewModel>();
            
            foreach(var chatAndMessage in directChatAndLastMessageList)
            {
                ChatViewModel chatViewModel = new ChatViewModel()
                {
                    Id = chatAndMessage.Chat.Id,
                    Name = userService.FindById(chatAndMessage.Chat.UserIds.First(id => id != userId)).Name,
                    UserIds = chatAndMessage.Chat.UserIds
                };

                getChatsViewModels.Add(new GetChatsViewModel()
                {
                    Chat = chatViewModel,
                    Message = chatAndMessage.Message
                });
            }

            foreach(var chatAndMessage in groupChatAndLastMessageList)
            {
                ChatViewModel chatViewModel = new ChatViewModel()
                {
                    Id = chatAndMessage.Chat.Id,
                    Name = chatAndMessage.Chat.ChatName,
                    UserIds = chatAndMessage.Chat.UserIds
                };

                getChatsViewModels.Add(new GetChatsViewModel()
                {
                    Chat = chatViewModel,
                    Message = chatAndMessage.Message
                });
            }

            var model = getChatsViewModels.OrderByDescending(c => c.Message.Date).ToArray();
            return Json(model);
        }

        public JsonResult GetDirectChatIdByUser(Guid userId)
        {
            Guid currentUserId = authenticationService.GetUserId();
            DirectChatDto chat = directChatService.FindByUsers(userId, currentUserId);
            if (chat == null)
                chat = directChatService.Create(currentUserId, userId);
            return Json(chat.Id);
        }

        public JsonResult GetChatDetails(Guid chatId)
        {
            Guid userId = authenticationService.GetUserId();
            DirectChatDto directChat = directChatService.FindById(chatId);
            ChatViewModel chatViewModel = new ChatViewModel()
            {
                Id = chatId
            };

            if (directChat != null)
            {
                chatViewModel.Name = userService.FindById(directChat.UserIds.First(id => id != userId)).Name;
                chatViewModel.UserIds = directChat.UserIds;
            }
            else {
                chatViewModel.Name = groupChatService.FindById(chatId).ChatName;
                chatViewModel.UserIds = directChat.UserIds;
            }

            return Json(chatViewModel);
        }

        /// <summary>
        /// Получение сообщений для чата отсортированных от самого нового к самому старому
        /// </summary>
        /// <param name="chatId">Id чата</param>
        /// <param name="afterDate">дата и время, до которой сообщения были отправлены. Если null, вернутся самые новые сообщения</param>
        /// <param name="count">"Количество сообщений. Если null, вернутся все сообщения"</param>
        /// <returns></returns>
        public JsonResult GetMessages(Guid chatId, DateTime? beforeDate, int? count)
        {
            Guid userId = authenticationService.GetUserId();
            var messages = chatService.GetMessages(chatId, userId)
                .Where(message => message.Date<(beforeDate??DateTime.MaxValue))
                .OrderByDescending(m => m.Date)
                .Take(count ?? int.MaxValue)
                .ToArray();
            return Json(messages);
        }
    }
}

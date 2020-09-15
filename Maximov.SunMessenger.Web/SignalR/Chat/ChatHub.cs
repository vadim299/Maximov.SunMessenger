using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.Authorization;
using Maxinov.SunMessenger.Services.ChatService.DTO;
using Maxinov.SunMessenger.Services.DirectChatService;
using Maxinov.SunMessenger.Services.GroupChatService.Abstractions;
using Maximov.SunMessenger.Web.Services.AuthenticationService.Abstractions;
using Maxinov.SunMessenger.Services.ChatServices;
using Maxinov.SunMessenger.Services.ChatServices.DTO;

namespace Maximov.SunMessenger.Web.SignalR.Chat
{
    [Authorize]
    public class ChatHub:Hub
    {
        public readonly IChatService chatService;
        public readonly IAuthenticationService authenticationService;

        public ChatHub(IChatService chatService, IAuthenticationService authenticationService)
        {
            this.chatService = chatService;
            this.authenticationService = authenticationService;
        }

        public async Task Send(Guid chatId, string message)
        {
            Guid userId = authenticationService.GetUserId();
            ChatDto chat = chatService.FindById(chatId);
            IReadOnlyList<string> userIds = chat.UserIds.Select(id => id.ToString()).ToList().AsReadOnly();
            var messageDto = chatService.AddMessage(chatId, userId, message);
            await Clients.Users(userIds).SendAsync("Receive", chatId, messageDto);
        }
    }
}

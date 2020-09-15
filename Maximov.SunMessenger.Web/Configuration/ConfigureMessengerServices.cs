using Maximov.SunMessenger.Core.Interfaces;
using Maximov.SunMessenger.Data;
using Maximov.SunMessenger.Web.Services.AuthenticationService;
using Maximov.SunMessenger.Web.Services.AuthenticationService.Abstractions;
using Maxinov.SunMessenger.Services.ChatServices;
using Maxinov.SunMessenger.Services.DirectChatService;
using Maxinov.SunMessenger.Services.GroupChatService;
using Maxinov.SunMessenger.Services.GroupChatService.Abstractions;
using Maxinov.SunMessenger.Services.UserService;
using Maxinov.SunMessenger.Services.UserService.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Maximov.SunMessenger.Web.Configuration
{
    public static class ConfigureMessengerServices
    {
        public static IServiceCollection AddMessengerServices(this IServiceCollection services)
        {
            services.AddScoped<IMessengerUnit, MessengerUnit>();

            services.AddScoped<IDirectChatService, DirectChatService>();
            services.AddScoped<IGroupChatService, GroupChatService>();
            services.AddScoped<IChatService, ChatService>();
            services.AddScoped<Maxinov.SunMessenger.Services.UserService.Abstractions.IUserService, UserService>();

            services.AddTransient<IAuthenticationService, AuthenticationService>();

            return services;
        }

    }
}

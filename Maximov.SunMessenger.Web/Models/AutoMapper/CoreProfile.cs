using AutoMapper;
using Maximov.SunMessenger.Web.Models.Shared.Chat;
using Maximov.SunMessenger.Web.Models.Shared.Message;
using Maximov.SunMessenger.Web.Models.Shared.User;
using Maximov.SunMessenger.Services.ChatService.DTO;
using Maximov.SunMessenger.Services.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Maximov.SunMessenger.Web.Models.AutoMapper
{
    public class CoreProfile:Profile
    {
        public CoreProfile()
        {
            CreateMap<MessageDto, MessageViewModel>()
                .ForMember(e => e.DateInMilliseconds, opt => opt.MapFrom(x => x.Date));

            CreateMap<DirectChatDto, ChatViewModel>();

            CreateMap<UserViewModel, UserDto>();
        }
    }
}

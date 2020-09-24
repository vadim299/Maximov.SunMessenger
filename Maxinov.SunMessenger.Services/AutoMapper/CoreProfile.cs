using AutoMapper;
using Maximov.SunMessenger.Core.Chats.Objects.Entities;
using Maximov.SunMessenger.Core.Messages.Objects;
using Maximov.SunMessenger.Core.Users.Objects;
using Maximov.SunMessenger.Services.ChatService.DTO;
using Maximov.SunMessenger.Services.ChatServices.DTO;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Maximov.SunMessenger.Services.DTO.AutoMapper
{
    public class CoreProfile:Profile
    {
        public CoreProfile()
        {
            CreateMap<DirectChat, DirectChatDto>()
                .ForMember(e => e.Date, opt => opt.MapFrom(x => x.ActiveFrom))
                .ForMember(e => e.UserIds, opt => opt.MapFrom(x => x.UserLinks.Select(x => x.UserId)));

            CreateMap<GroupChat, GroupChatDto>()
                .ForMember(e => e.ChatName, opt => opt.MapFrom(x => x.Name))
                .ForMember(e => e.Date, opt => opt.MapFrom(x => x.ActiveFrom))
                .ForMember(e => e.UserIds, opt => opt.MapFrom(x => x.UserLinks.Select(d => d.UserId)));

            CreateMap<Message, MessageDto>()
                .ForMember(e => e.Date, opt => opt.MapFrom(x => x.ActiveFrom))
                .ForMember(e => e.ChatIds, opt => opt.MapFrom(x => x.ChatLinks.Select(cl => cl.ChatId)));

            CreateMap<User, UserDto>();

            CreateMap<GroupChatDto, ChatDto>()
                .ForMember(e => e.ChatType, opt => opt.MapFrom(x => ChatTypes.Group));

            CreateMap<DirectChatDto, ChatDto>()
               .ForMember(e => e.ChatType, opt => opt.MapFrom(x => ChatTypes.Direct ));
        }
    }
}

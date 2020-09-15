using AutoMapper;
using Maximov.SunMessenger.Core.Chats.Objects.Entities;
using Maximov.SunMessenger.Core.Messages.Objects;
using Maximov.SunMessenger.Core.Users.Objects;
using Maxinov.SunMessenger.Services.ChatService.DTO;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Maxinov.SunMessenger.Services.DTO.AutoMapper
{
    public class CoreProfile:Profile
    {
        public CoreProfile()
        {
            CreateMap<DirectChat, DirectChatDto>()
                .ForMember(e => e.Id, opt => opt.MapFrom(x => x.Id))
                .ForMember(e => e.Date, opt => opt.MapFrom(x => x.ActiveFrom))
                .ForMember(e => e.UserIds, opt => opt.MapFrom(x => x.UserLinks.Select(x => x.UserId)));

            CreateMap<GroupChat, GroupChatDto>()
                .ForMember(e => e.Id, opt => opt.MapFrom(x => x.Id))
                .ForMember(e => e.ChatName, opt => opt.MapFrom(x => x.Name))
                .ForMember(e => e.CreatorId, opt => opt.MapFrom(x => x.CreatorId))
                .ForMember(e => e.Date, opt => opt.MapFrom(x => x.ActiveFrom))
                .ForMember(e => e.UserIds, opt => opt.MapFrom(x => x.UserLinks.Select(d => d.UserId)));

            CreateMap<Message, MessageDto>()
                .ForMember(e => e.Date, opt => opt.MapFrom(x => x.ActiveFrom))
                .ForMember(e => e.Id, opt => opt.MapFrom(x => x.Id))
                .ForMember(e => e.SenderId, opt => opt.MapFrom(x => x.SenderId))
                .ForMember(e => e.Text, opt => opt.MapFrom(x => x.Text));

            CreateMap<User, UserDto>()
                .ForMember(e => e.Id, opt => opt.MapFrom(x => x.Id))
                .ForMember(e => e.Login, opt => opt.MapFrom(x => x.Login))
                .ForMember(e => e.Name, opt => opt.MapFrom(x => x.Name))
                .ForMember(e => e.Password, opt => opt.MapFrom(x => x.Password));
        }
    }
}

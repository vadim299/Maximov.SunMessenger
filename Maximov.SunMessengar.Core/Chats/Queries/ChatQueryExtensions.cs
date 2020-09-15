using Maximov.SunMessenger.Core.Chats.Objects.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Maximov.SunMessenger.Core.Chats.Queries
{
    public static class ChatQueryExtensions
    {
        public static IQueryable<T> WithUser<T>(this IQueryable<T> chats, Guid userId) where T : Chat
        => from chat in chats
           where chat.UserLinks.Any(ul => ul.UserId == userId)
           select chat;
    }
}

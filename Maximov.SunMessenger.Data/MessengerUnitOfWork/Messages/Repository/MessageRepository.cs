using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Maximov.SunMessenger.Core;
using Maximov.SunMessenger.Core.Interfaces;
using Maximov.SunMessenger.Core.Interfaces.Repositories;
using Maximov.SunMessenger.Core.Messages.Objects;
using Maximov.SunMessenger.Data.MessengerUnitOfWork.DirectChats.Specifications;
using Maximov.SunMessenger.Data.MessengerUnitOfWork.GroupChats.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Maximov.SunMessenger.Data.Repositories
{
    public class MessageRepository:IMessageRepository
    {
        private readonly SunMessengerContext db;

        public MessageRepository(SunMessengerContext db)
        {
            this.db = db;
        }

        public void Create(Message message)
        {
            db.Messages.Add(message);
        }

        public void Delete(Guid id)
        {
            Message message = db.Messages.Find(id);
            if (message != null)
                db.Messages.Remove(message);
        }

        public Message FindById(Guid id)
        {
            return db.Messages.Include(m=>m.ChatLinks).First(m=>m.Id==id);
        }

        public IEnumerable<Message> GetAll()
        {
            return db.Messages.Include(m => m.ChatLinks);
        }

        public IEnumerable<Message> GetBySpecification(ISpecification<Message> specification)
        {
            return specification.Apply(db.Messages.Include(m => m.ChatLinks)).ToArray();
        }

        public IEnumerable<Message> GetLastMessages(Guid userId, Guid[] chatIds)
        {
            var directChats = new GetDirectChatWithUsersSpecification(userId)
                .Apply(db.DirectChats.Where(chat=>chatIds.Contains(chat.Id)));

            var directMessages = from message in db.Messages
                                 from chatLink in message.ChatLinks
                                 join chat in directChats on chatLink.ChatId equals chat.Id
                                 group message by chatLink.ChatId into gr
                                 select new { chatId = gr.Key, lastDate = gr.Max(mes => mes.ActiveFrom) } into sel
                                 from message in db.Messages
                                 from chatLink in message.ChatLinks
                                 where sel.chatId==chatLink.ChatId && message.ActiveFrom == sel.lastDate
                                 select message;

            var s = directMessages.ToArray();

            var groupChats = new GetGroupChatsWithUsersSpecification(userId)
                .Apply(db.GroupChats.Where(chat => chatIds.Contains(chat.Id)));

            var groupMessages = from message in db.Messages
                                from chatLink in message.ChatLinks
                                join chat in groupChats on chatLink.ChatId equals chat.Id
                                from userHistory in chat.UserHistory
                                where userHistory.UserId == userId
                                && message.ActiveFrom >= userHistory.DateFrom
                                && message.ActiveFrom <= (userHistory.DateTo ?? DateTime.MaxValue)
                                group message by chatLink.ChatId into gr
                                select new { chatId = gr.Key, lastDate = gr.Max(mes => mes.ActiveFrom) } into sel
                                from message in db.Messages
                                from chatLink in message.ChatLinks
                                where sel.chatId == chatLink.ChatId && message.ActiveFrom == sel.lastDate
                                select message;

            return directMessages.Union(groupMessages).Include(m=>m.ChatLinks).ToArray();
        }

        public IEnumerable<Message> GetMessages(Guid userId, Guid chatId)
        {
            var directChatMessages = from message in db.Messages
                                     from chatLink in message.ChatLinks
                                     join chat in db.DirectChats on chatLink.ChatId equals chat.Id
                                     where chat.Id == chatId
                                     && chat.UserLinks.Any(ul => ul.UserId == userId)
                                     select message;
            var groupChatMessages = from message in db.Messages
                                    from chatLink in message.ChatLinks
                                    join chat in db.GroupChats on chatLink.ChatId equals chat.Id
                                    from userHistory in chat.UserHistory
                                    where chat.Id == chatId && userHistory.UserId == userId
                                    && message.ActiveFrom >= userHistory.DateFrom
                                    && message.ActiveFrom <= (userHistory.DateTo ?? DateTime.MaxValue)
                                    select message;
            return directChatMessages.Union(groupChatMessages).Include(m=>m.ChatLinks).ToArray();
        }

        public void Update(Message message)
        {
            db.Messages.Update(message);
        }
    }
}

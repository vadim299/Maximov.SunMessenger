using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Maximov.SunMessenger.Core;
using Maximov.SunMessenger.Core.Interfaces;
using Maximov.SunMessenger.Core.Messages.Objects;

namespace Maximov.SunMessenger.Data.Repositories
{
    public class MessageRepository:IWriteRepository<Message>
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
            return db.Messages.Find(id);
        }

        public IQueryable<Message> GetAll()
        {
            return db.Messages;
        }

        public void Update(Message message)
        {
            db.Messages.Update(message);
        }
    }
}

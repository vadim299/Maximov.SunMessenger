using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Maximov.SunMessenger.Core;
using Maximov.SunMessenger.Core.Chats.Objects.Entities;
using Maximov.SunMessenger.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Maximov.SunMessenger.Data.Repositories
{
    public class DirectChatRepository:IWriteRepository<DirectChat>
    {
        private readonly SunMessengerContext db;

        public DirectChatRepository(SunMessengerContext db)
        {
            this.db = db;
        }

        public void Create(DirectChat entity)
        {
            db.DirectChats.Add(entity);
        }

        public void Delete(Guid id)
        {
            DirectChat directChat = db.DirectChats.Find(id);
            if (directChat != null)
                db.DirectChats.Remove(directChat);
        }

        public DirectChat FindById(Guid id)
        {
            return db.DirectChats.Include(c => c.MessageLinks).Include(c => c.UserLinks).FirstOrDefault(c => c.Id == id);
        }

        public IQueryable<DirectChat> GetAll()
        {
            return db.DirectChats.Include(c => c.MessageLinks).Include(c => c.UserLinks);
        }

        public void Update(DirectChat entity)
        {
            db.DirectChats.Update(entity);
        }
    }
}

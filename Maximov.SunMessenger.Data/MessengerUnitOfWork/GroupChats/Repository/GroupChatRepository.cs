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
    public class GroupChatRepository:IWriteRepository<GroupChat>
    {
        private readonly SunMessengerContext db;

        public GroupChatRepository(SunMessengerContext db)
        {
            this.db = db;
        }

        public void Create(GroupChat entity)
        {
            db.GroupChats.Add(entity);
        }

        public void Delete(Guid id)
        {
            GroupChat groupChat = db.GroupChats.Find(id);
            if (groupChat != null)
                db.GroupChats.Remove(groupChat);
        }

        public GroupChat FindById(Guid id)
        {
            return db.GroupChats.Include(e => e.UserLinks).First(c=>c.Id==id);
        }

        public IQueryable<GroupChat> GetAll()
        {
            return db.GroupChats.Include(e=>e.UserLinks);
        }

        public IEnumerable<GroupChat> GetBySpecification(ISpecification<GroupChat> specification)
        {
            return specification.Apply(db.GroupChats.Include(e => e.UserLinks)).ToArray();
        }

        public void Update(GroupChat entity)
        {
            db.GroupChats.Update(entity);
        }

        IEnumerable<GroupChat> IReadRepository<GroupChat>.GetAll()
        {
            throw new NotImplementedException();
        }
    }
}

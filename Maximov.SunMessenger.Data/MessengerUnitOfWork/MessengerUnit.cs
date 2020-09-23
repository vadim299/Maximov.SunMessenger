using System;
using System.Collections.Generic;
using System.Text;
using Maximov.SunMessenger.Core;
using Maximov.SunMessenger.Core.Chats.Objects.Entities;
using Maximov.SunMessenger.Core.Interfaces;
using Maximov.SunMessenger.Core.Interfaces.Repositories;
using Maximov.SunMessenger.Core.Messages.Objects;
using Maximov.SunMessenger.Core.Users.Objects;
using Maximov.SunMessenger.Data.Repositories;

namespace Maximov.SunMessenger.Data
{
    public class MessengerUnit : IMessengerUnit
    {
        private readonly SunMessengerContext db;

        private IWriteRepository<DirectChat> directChats;
        private IWriteRepository<GroupChat> groupChats;
        private IMessageRepository messages;
        private IWriteRepository<User> users;

        public MessengerUnit(SunMessengerContext db)
        {
            this.db = db;
        }

        public IWriteRepository<DirectChat> DirectChats
        {
            get
            {
                if (directChats == null)
                    directChats = new DirectChatRepository(db);
                return directChats;
            }
        }

        public IWriteRepository<GroupChat> GroupChats
        {
            get
            {
                if (groupChats == null)
                    groupChats = new GroupChatRepository(db);
                return groupChats;
            }
        }

        public IMessageRepository Messages
        {
            get
            {
                if (messages == null)
                    messages = new MessageRepository(db);
                return messages;
            }
        }

        public IWriteRepository<User> Users
        {
            get
            {
                if (users == null)
                    users = new UserRepository(db);
                return users;
            }
        }

        public void SaveChanges()
        {
            db.SaveChanges();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Proxies;
using Maximov.SunMessenger.Core;
using Maximov.SunMessenger.Core.Messages.Objects;
using Maximov.SunMessenger.Core.Users.Objects;
using Maximov.SunMessenger.Core.Chats.Objects.ValueObjects;
using Maximov.SunMessenger.Core.Chats.Objects.Entities;
using Maximov.SunMessenger.Data.EntityTypeConfigurrations;
using Maximov.SunMessenger.Data.EntityTypeConfigurations;
using Microsoft.Extensions.Options;

namespace Maximov.SunMessenger.Data
{
    public class SunMessengerContext:DbContext
    {
        public DbSet<DirectChat> DirectChats { get; set; }
        public DbSet<GroupChat> GroupChats { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<User> Users { get; set; }

        public SunMessengerContext(DbContextOptions<SunMessengerContext> options) : base(options) {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ChatConfiguration());
            modelBuilder.ApplyConfiguration(new UserHistoryItemConfiguration());
            modelBuilder.ApplyConfiguration(new DirectChatConfiguration());
            modelBuilder.ApplyConfiguration(new GroupChatConfiguration());
            modelBuilder.ApplyConfiguration(new MessageConfiguration());
            modelBuilder.ApplyConfiguration(new MessageLinkConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new UserLinkConfiguration());
           
            base.OnModelCreating(modelBuilder);
        }
    }
}

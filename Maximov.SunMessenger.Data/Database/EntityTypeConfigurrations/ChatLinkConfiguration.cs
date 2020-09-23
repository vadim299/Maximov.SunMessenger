using Maximov.SunMessenger.Core.Chats.Objects.Entities;
using Maximov.SunMessenger.Core.Messages.Objects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Maximov.SunMessenger.Data.Database.EntityTypeConfigurrations
{
    class ChatLinkConfiguration : IEntityTypeConfiguration<ChatLink>
    {
        public void Configure(EntityTypeBuilder<ChatLink> builder)
        {
            builder.HasKey(e => e.Id);

            builder.HasOne<Chat>()
                .WithMany()
                .HasForeignKey(e => e.ChatId);
        }
    }
}

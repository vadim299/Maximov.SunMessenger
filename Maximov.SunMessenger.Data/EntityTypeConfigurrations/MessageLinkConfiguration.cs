using Maximov.SunMessenger.Core.Chats.Objects.Entities;
using Maximov.SunMessenger.Core.Chats.Objects.ValueObjects;
using Maximov.SunMessenger.Core.Messages.Objects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Maximov.SunMessenger.Data.EntityTypeConfigurrations
{
    public class MessageLinkConfiguration : IEntityTypeConfiguration<MessageLink>
    {
        public void Configure(EntityTypeBuilder<MessageLink> builder)
        {
            builder.HasKey(e => e.Id);

            builder.HasOne<Message>()
                .WithMany()
                .HasForeignKey(e => e.MessageId);
        }
    }
}

using Maximov.SunMessenger.Core.Chats.Objects.Entities;
using Maximov.SunMessenger.Core.Users.Objects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Maximov.SunMessenger.Data.EntityTypeConfigurrations
{
    class GroupChatConfiguration : IEntityTypeConfiguration<GroupChat>
    {
        public void Configure(EntityTypeBuilder<GroupChat> builder)
        {
            builder.Property(e => e.Name)
                .HasMaxLength(30)
                .IsRequired();

            builder.HasOne<User>()
                .WithMany()
                .HasForeignKey(e => e.CreatorId);

            builder.HasMany<UserHistoryItem>()
                .WithOne();
        }
    }
}

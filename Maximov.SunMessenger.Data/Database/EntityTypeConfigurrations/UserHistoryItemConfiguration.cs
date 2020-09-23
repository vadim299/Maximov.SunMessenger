using System;
using System.Collections.Generic;
using System.Text;
using Maximov.SunMessenger.Core.Chats.Objects.Entities;
using Maximov.SunMessenger.Core.Users.Objects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Maximov.SunMessenger.Data.EntityTypeConfigurrations
{
    class UserHistoryItemConfiguration:IEntityTypeConfiguration<UserHistoryItem>
    {
        public void Configure(EntityTypeBuilder<UserHistoryItem> builder)
        {
            builder.HasKey(e => e.Id);

            builder.HasOne<User>()
                .WithMany()
                .HasForeignKey(e => e.UserId);
        }
    }
}

using Maximov.SunMessenger.Core.Chats.Objects.Entities;
using Maximov.SunMessenger.Core.Chats.Objects.ValueObjects;
using Maximov.SunMessenger.Core.Users.Objects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Maximov.SunMessenger.Data.EntityTypeConfigurrations
{
    class UserLinkConfiguration : IEntityTypeConfiguration<UserLink>
    {
        public void Configure(EntityTypeBuilder<UserLink> builder)
        {
            builder.HasKey(e => e.Id);

            builder.HasOne<User>()
                .WithMany()
                .HasForeignKey(e => e.UserId);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Maximov.SunMessenger.Core.Chats.Objects.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Maximov.SunMessenger.Data.EntityTypeConfigurrations
{
    class ChatConfiguration : IEntityTypeConfiguration<Chat>
    {
        public void Configure(EntityTypeBuilder<Chat> builder)
        {
            builder.HasKey(e => e.Id);

            builder.HasMany(e => e.UserLinks)
                .WithOne();
        }
    }
}

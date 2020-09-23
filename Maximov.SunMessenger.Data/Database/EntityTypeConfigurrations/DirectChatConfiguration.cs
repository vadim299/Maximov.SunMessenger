using Maximov.SunMessenger.Core.Chats.Objects.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Maximov.SunMessenger.Data.EntityTypeConfigurations
{
    class DirectChatConfiguration : IEntityTypeConfiguration<DirectChat>
    {
        public void Configure(EntityTypeBuilder<DirectChat> builder)
        {
        }
    }
}

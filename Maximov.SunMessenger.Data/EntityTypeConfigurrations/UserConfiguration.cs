using Maximov.SunMessenger.Core.Users.Objects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Maximov.SunMessenger.Data.EntityTypeConfigurations
{
    class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);
            builder.Property(u => u.Login)
                .HasMaxLength(30)
                .IsRequired();

            builder.Property(u => u.Name)
                .HasMaxLength(30)
                .IsRequired();

            builder.HasIndex(u => u.Login)
                .IsUnique();

        }
    }
}

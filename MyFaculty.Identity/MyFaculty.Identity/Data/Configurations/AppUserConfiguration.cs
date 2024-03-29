﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyFaculty.Identity.Models;

namespace MyFaculty.Identity.Data.Configurations
{
    public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.HasKey(user => user.Id);
            builder.Property(user => user.AvatarPath).IsRequired(false);
            builder.Property(user => user.BirthDate).IsRequired(false);
            builder.Property(user => user.CityId).IsRequired(false);
            builder.Property(user => user.Website).IsRequired(false);
            builder.Property(user => user.VKLink).IsRequired(false);
            builder.Property(user => user.TelegramLink).IsRequired(false);
        }
    }
}

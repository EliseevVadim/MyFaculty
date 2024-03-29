﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyFaculty.Domain.Entities;

namespace MyFaculty.Persistence.EntityConfigurations
{
    public class GroupConfiguration : IEntityTypeConfiguration<Group>
    {
        public void Configure(EntityTypeBuilder<Group> builder)
        {
            builder.HasKey(group => group.Id);
            builder.Property(group => group.Id).ValueGeneratedOnAdd();
            builder.HasMany(group => group.Pairs)
                .WithOne(pair => pair.Group)
                .HasForeignKey(pair => pair.GroupId);
            builder.HasOne(group => group.Course)
                .WithMany(course => course.Groups)
                .HasForeignKey(group => group.CourseId);
            builder.HasMany(group => group.Students)
                .WithOne(user => user.Group)
                .HasForeignKey(user => user.GroupId);
        }
    }
}

﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyFaculty.Domain.Entities;

namespace MyFaculty.Persistence.EntityConfigurations
{
    public class FacultyConfiguration : IEntityTypeConfiguration<Faculty>
    {
        public void Configure(EntityTypeBuilder<Faculty> builder)
        {
            builder.HasKey(faculty => faculty.Id);
            builder.Property(faculty => faculty.Id).ValueGeneratedOnAdd();
            builder.Property(faculty => faculty.FacultyName).IsRequired();
            builder.Property(faculty => faculty.OfficialWebsite).IsRequired(false);
            builder.HasMany(faculty => faculty.Floors)
                .WithOne(floor => floor.Faculty)
                .HasForeignKey(floor => floor.FacultyId);
            builder.HasMany(faculty => faculty.Courses)
                .WithOne(course => course.Faculty)
                .HasForeignKey(course => course.FacultyId);
            builder.HasMany(faculty => faculty.Students)
                .WithOne(user => user.Faculty)
                .HasForeignKey(user => user.FacultyId);
        }
    }
}

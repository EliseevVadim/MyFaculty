﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyFaculty.Domain.Entities;

namespace MyFaculty.Persistence.EntityConfigurations
{
    public class DisciplineConfiguration : IEntityTypeConfiguration<Discipline>
    {
        public void Configure(EntityTypeBuilder<Discipline> builder)
        {
            builder.HasKey(discipline => discipline.Id);
            builder.Property(discipline => discipline.Id).ValueGeneratedOnAdd();
            builder.HasMany(discipline => discipline.Pairs)
                .WithOne(pair => pair.Discipline)
                .HasForeignKey(pair => pair.DisciplineId);
            builder.HasMany(discipline => discipline.TeacherDisciplines)
                .WithOne(teacherDiscipline => teacherDiscipline.Discipline)
                .HasForeignKey(teacherDiscipline => teacherDiscipline.DisciplineId);
        }
    }
}

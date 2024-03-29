﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyFaculty.Domain.Entities;

namespace MyFaculty.Persistence.EntityConfigurations
{
    public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.ToTable("users");
            builder.HasKey(user => user.Id);
            builder.Property(user => user.Email).IsRequired();
            builder.HasIndex(user => user.Email).IsUnique();
            builder.Property(user => user.AvatarPath).IsRequired(false);
            builder.Property(user => user.BirthDate).IsRequired(false);
            builder.Property(user => user.CityId).IsRequired(false);
            builder.HasOne(user => user.City)
                .WithMany(city => city.Users)
                .HasForeignKey(user => user.CityId);
            builder.Property(user => user.FacultyId).IsRequired(false);
            builder.HasOne(user => user.Faculty)
                .WithMany(faculty => faculty.Students)
                .HasForeignKey(user => user.FacultyId);
            builder.Property(user => user.CourseId).IsRequired(false);
            builder.HasOne(user => user.Course)
                .WithMany(course => course.Students)
                .HasForeignKey(user => user.CourseId);
            builder.Property(user => user.GroupId).IsRequired(false);
            builder.HasOne(user => user.Group)
                .WithMany(group => group.Students)
                .HasForeignKey(user => user.GroupId);
            builder.HasMany(user => user.OwnedStudyClubs)
                .WithOne(club => club.Owner)
                .HasForeignKey(club => club.OwnerId);
            builder.HasMany(user => user.OwnedInformationPublics)
                .WithOne(infoPublic => infoPublic.Owner)
                .HasForeignKey(infoPublic => infoPublic.OwnerId);
            builder.HasMany(user => user.Comments)
                .WithOne(comment => comment.Author)
                .HasForeignKey(comment => comment.AuthorId);
            builder.HasMany(user => user.TaskSubmissions)
                .WithOne(submission => submission.Author)
                .HasForeignKey(submission => submission.AuthorId);
            builder.HasMany(user => user.SubmissionReviews)
                .WithOne(review => review.Reviewer)
                .HasForeignKey(review => review.ReviewerId);
            builder.HasMany(user => user.Notifications)
                .WithOne(notification => notification.NotifiedUser)
                .HasForeignKey(notification => notification.UserId);
            builder.HasMany(user => user.AppliedBanActions)
                .WithOne(banReport => banReport.AffectedUser)
                .HasForeignKey(banReport => banReport.AffectedUserId);
            builder.HasMany(user => user.PerformedBanActions)
                .WithOne(banReport => banReport.Administrator)
                .HasForeignKey(banReport => banReport.AdministratorId);
            builder.HasMany(user => user.InformationsPublicBanReports)
                .WithOne(banReport => banReport.Administrator)
                .HasForeignKey(banReport => banReport.AdministratorId);
            builder.Property(user => user.IsTeacher).HasDefaultValue(false);
            builder.Property(user => user.Website).IsRequired(false);
            builder.Property(user => user.VKLink).IsRequired(false);
            builder.Property(user => user.TelegramLink).IsRequired(false);
            builder.Property(user => user.IsBanned).HasDefaultValue(false);
        }
    }
}

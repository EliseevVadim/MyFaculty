﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyFaculty.Domain.Entities;

namespace MyFaculty.Persistence.EntityConfigurations
{
    public class TaskSubmissionReviewConfiguration : IEntityTypeConfiguration<TaskSubmissionReview>
    {
        public void Configure(EntityTypeBuilder<TaskSubmissionReview> builder)
        {
            builder.HasKey(review => review.Id);
            builder.Property(review => review.Id).ValueGeneratedOnAdd();
            builder.Property(review => review.TextContent).IsRequired(false);
            builder.Property(review => review.Attachments).IsRequired(false);
            builder.Property(review => review.SubmissionReviewAttachmentsUid).IsRequired();
            builder.Property(review => review.Rate).IsRequired();
            builder.HasOne(review => review.Reviewer)
                .WithMany(user => user.SubmissionReviews)
                .HasForeignKey(review => review.ReviewerId);
        }
    }
}

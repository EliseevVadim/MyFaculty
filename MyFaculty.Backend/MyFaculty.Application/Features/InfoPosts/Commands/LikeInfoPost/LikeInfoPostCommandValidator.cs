﻿using FluentValidation;

namespace MyFaculty.Application.Features.InfoPosts.Commands.LikeInfoPost
{
    public class LikeInfoPostCommandValidator : AbstractValidator<LikeInfoPostCommand>
    {
        public LikeInfoPostCommandValidator()
        {
            RuleFor(command => command.LikedPostId).NotEmpty();
            RuleFor(command => command.LikedUserId).NotEmpty();
        }
    }
}

﻿using FluentValidation;

namespace MyFaculty.Application.Features.Pairs.Commands.DeletePair
{
    public class DeletePairCommandValidator : AbstractValidator<DeletePairCommand>
    {
        public DeletePairCommandValidator()
        {
            RuleFor(command => command.Id).NotEmpty();
        }
    }
}

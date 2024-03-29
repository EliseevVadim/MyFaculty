﻿using FluentValidation;

namespace MyFaculty.Application.Features.Faculties.Commands.DeleteFaculty
{
    public class DeleteFacultyCommandValidator : AbstractValidator<DeleteFacultyCommand>
    {
        public DeleteFacultyCommandValidator()
        {
            RuleFor(command => command.Id).NotEmpty();
        }
    }
}

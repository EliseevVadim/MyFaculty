﻿using FluentValidation;

namespace MyFaculty.Application.Features.Cities.Commands.DeleteCity
{
    public class DeleteCityCommandValidator : AbstractValidator<DeleteCityCommand>
    {
        public DeleteCityCommandValidator()
        {
            RuleFor(command => command.Id).NotEmpty();
        }
    }
}

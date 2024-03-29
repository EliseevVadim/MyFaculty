﻿using FluentValidation;

namespace MyFaculty.Application.Features.Cities.Commands.CreateCity
{
    public class CreateCityCommandValidator : AbstractValidator<CreateCityCommand>
    {
        public CreateCityCommandValidator()
        {
            RuleFor(command => command.CityName).NotEmpty();
            RuleFor(command => command.RegionId).NotEmpty();
        }
    }
}

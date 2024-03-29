﻿using FluentValidation;

namespace MyFaculty.Application.Features.Cities.Commands.UpdateCity
{
    public class UpdateCityCommandValidator : AbstractValidator<UpdateCityCommand>
    {
        public UpdateCityCommandValidator()
        {
            RuleFor(command => command.Id).NotEmpty();
            RuleFor(command => command.CityName).NotEmpty();
            RuleFor(command => command.RegionId).NotEmpty();
        }
    }
}

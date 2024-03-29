﻿using MediatR;
using MyFaculty.Application.ViewModels;

namespace MyFaculty.Application.Features.Cities.Commands.CreateCity
{
    public class CreateCityCommand : IRequest<CityViewModel>
    {
        public string CityName { get; set; }
        public int RegionId { get; set; }
    }
}

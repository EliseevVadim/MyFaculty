﻿using MediatR;
using MyFaculty.Application.ViewModels;

namespace MyFaculty.Application.Features.Cities.Commands.UpdateCity
{
    public class UpdateCityCommand : IRequest<CityViewModel>
    {
        public int Id { get; set; }
        public string CityName { get; set; }
        public int RegionId { get; set; }
    }
}

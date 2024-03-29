﻿using AutoMapper;
using MyFaculty.Application.Common.Mappings;
using MyFaculty.Application.Dto;
using MyFaculty.Domain.Entities;
using System;
using System.Collections.Generic;

namespace MyFaculty.Application.ViewModels
{
    public class CountryViewModel : IMapWith<Country>
    {
        public int Id { get; set; }
        public string CountryName { get; set; }
        public List<RegionLookupDto> Regions { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Country, CountryViewModel>()
                .ForMember(vm => vm.Id, options => options.MapFrom(country => country.Id))
                .ForMember(vm => vm.CountryName, options => options.MapFrom(country => country.CountryName))
                .ForMember(vm => vm.Regions, options => options.MapFrom(country => country.Regions))
                .ForMember(vm => vm.Created, options => options.MapFrom(country => country.Created))
                .ForMember(vm => vm.Updated, options => options.MapFrom(country => country.Updated));
        }
    }
}

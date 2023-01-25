﻿using AutoMapper;
using MyFaculty.Application.Common.Mappings;
using MyFaculty.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFaculty.Application.Dto
{
    public class InformationPublicLookupDto : IMapWith<InformationPublic>
    {
        public int Id { get; set; }
        public string PublicName { get; set; }
        public string ImagePath { get; set; }
        public int MembersCount { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<InformationPublic, InformationPublicLookupDto>()
                .ForMember(dto => dto.Id, options => options.MapFrom(infoPublic => infoPublic.Id))
                .ForMember(dto => dto.PublicName, options => options.MapFrom(infoPublic => infoPublic.PublicName))
                .ForMember(dto => dto.ImagePath, options => options.MapFrom(infoPublic => infoPublic.ImagePath))
                .ForMember(dto => dto.MembersCount, options => options.MapFrom(infoPublic => infoPublic.Members.Count));
        }
    }
}
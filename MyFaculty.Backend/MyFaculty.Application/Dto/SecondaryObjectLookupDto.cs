﻿using AutoMapper;
using MyFaculty.Application.Common.Mappings;
using MyFaculty.Domain.Entities;

namespace MyFaculty.Application.Dto
{
    public class SecondaryObjectLookupDto : IMapWith<SecondaryObject>
    {
        public int Id { get; set; }
        public string ObjectName { get; set; }
        public string PositionInfo { get; set; }
        public int SecondaryObjectTypeId { get; set; }
        public int FloorId { get; set; }
        public int FacultyId { get; set; }
        public string FacultyName { get; set; }
        public SecondaryObjectTypeLookupDto SecondaryObjectType { get; set; }
        public string FloorName { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<SecondaryObject, SecondaryObjectLookupDto>()
                .ForMember(dto => dto.Id, options => options.MapFrom(secondaryObject => secondaryObject.Id))
                .ForMember(dto => dto.ObjectName, options => options.MapFrom(secondaryObject => secondaryObject.ObjectName))
                .ForMember(dto => dto.PositionInfo, options => options.MapFrom(secondaryObject => secondaryObject.PositionInfo))
                .ForMember(dto => dto.SecondaryObjectTypeId, options => options.MapFrom(secondaryObject => secondaryObject.SecondaryObjectTypeId))
                .ForMember(dto => dto.FacultyId, options => options.MapFrom(secondaryObject => secondaryObject.Floor.FacultyId))
                .ForMember(dto => dto.FacultyName, options => options.MapFrom(secondaryObject => secondaryObject.Floor.Faculty.FacultyName))
                .ForMember(dto => dto.FloorId, options => options.MapFrom(secondaryObject => secondaryObject.FloorId))
                .ForMember(dto => dto.SecondaryObjectType, options => options.MapFrom(secondaryObject => secondaryObject.SecondaryObjectType))
                .ForMember(dto => dto.FloorName, options => options.MapFrom(secondaryObject => secondaryObject.Floor.Name));
        }
    }
}

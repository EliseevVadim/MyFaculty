﻿using AutoMapper;
using MyFaculty.Application.Common.Mappings;
using MyFaculty.Domain.Entities;

namespace MyFaculty.Application.Dto
{
    public class AuditoriumLookupDto : IMapWith<Auditorium>
    {
        public int Id { get; set; }
        public string AuditoriumName { get; set; }
        public string PositionInfo { get; set; }
        public int FloorId { get; set; }
        public string FloorName { get; set; }
        public int FacultyId { get; set; }
        public string FacultyName { get; set; }
        public int TeacherId { get; set; }
        public string TeachersFIO { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Auditorium, AuditoriumLookupDto>()
                .ForMember(dto => dto.Id, options => options.MapFrom(auditorium => auditorium.Id))
                .ForMember(dto => dto.AuditoriumName, options => options.MapFrom(auditorium => auditorium.AuditoriumName))
                .ForMember(dto => dto.PositionInfo, options => options.MapFrom(auditorium => auditorium.PositionInfo))
                .ForMember(dto => dto.FloorId, options => options.MapFrom(auditorium => auditorium.FloorId))
                .ForMember(dto => dto.FloorName, options => options.MapFrom(auditorium => auditorium.Floor.Name))
                .ForMember(dto => dto.FacultyId, options => options.MapFrom(auditorium => auditorium.Floor.FacultyId))
                .ForMember(dto => dto.FacultyName, options => options.MapFrom(auditorium => auditorium.Floor.Faculty.FacultyName))
                .ForMember(dto => dto.TeacherId, options => options.MapFrom(auditorium => auditorium.TeacherId))
                .ForMember(dto => dto.TeachersFIO, options => options.MapFrom(auditorium => auditorium.Teacher.FIO));
        }
    }
}

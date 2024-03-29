﻿using AutoMapper;
using MyFaculty.Application.Common.Mappings;
using MyFaculty.Domain.Entities;

namespace MyFaculty.Application.Dto
{
    public class PairLookupDto : IMapWith<Pair>
    {
        public int Id { get; set; }
        public string PairName { get; set; }
        public int TeacherId { get; set; }
        public string TeachersFIO { get; set; }
        public int AuditoriumId { get; set; }
        public AuditoriumLookupDto Auditorium { get; set; }
        public int GroupId { get; set; }
        public GroupLookupDto Group { get; set; }
        public int DisciplineId { get; set; }
        public string DisciplineName { get; set; }
        public int DayOfWeekId { get; set; }
        public string DayOfWeek { get; set; }
        public int PairRepeatingId { get; set; }
        public string PairRepeatingName { get; set; }
        public int PairInfoId { get; set; }
        public PairInfoLookupDto PairInfo { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Pair, PairLookupDto>()
                .ForMember(dto => dto.Id, options => options.MapFrom(pair => pair.Id))
                .ForMember(dto => dto.PairName, options => options.MapFrom(pair => pair.PairName))
                .ForMember(dto => dto.TeacherId, options => options.MapFrom(pair => pair.TeacherId))
                .ForMember(dto => dto.TeachersFIO, options => options.MapFrom(pair => pair.Teacher.FIO))
                .ForMember(dto => dto.AuditoriumId, options => options.MapFrom(pair => pair.AuditoriumId))
                .ForMember(dto => dto.Auditorium, options => options.MapFrom(pair => pair.Auditorium))
                .ForMember(dto => dto.GroupId, options => options.MapFrom(pair => pair.GroupId))
                .ForMember(dto => dto.Group, options => options.MapFrom(pair => pair.Group))
                .ForMember(dto => dto.DisciplineId, options => options.MapFrom(pair => pair.DisciplineId))
                .ForMember(dto => dto.DisciplineName, options => options.MapFrom(pair => pair.Discipline.DisciplineName))
                .ForMember(dto => dto.DayOfWeekId, options => options.MapFrom(pair => pair.DayOfWeekId))
                .ForMember(dto => dto.DayOfWeek, options => options.MapFrom(pair => pair.DayOfWeek.DaysName))
                .ForMember(dto => dto.PairRepeatingId, options => options.MapFrom(pair => pair.PairRepeatingId))
                .ForMember(dto => dto.PairRepeatingName, options => options.MapFrom(pair => pair.PairRepeating.RepeatingName))
                .ForMember(dto => dto.PairInfoId, options => options.MapFrom(pair => pair.PairInfoId))
                .ForMember(dto => dto.PairInfo, options => options.MapFrom(pair => pair.PairInfo));
        }
    }
}

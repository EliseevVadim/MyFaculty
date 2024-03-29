﻿using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MyFaculty.Application.Common.Interfaces;
using MyFaculty.Application.Dto;
using MyFaculty.Application.ViewModels;
using System.Threading;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.TeachersDisciplines.Queries.GetTeachersDisciplines
{
    public class GetTeachersDisciplinesQueryHandler : IRequestHandler<GetTeachersDisciplinesQuery, TeachersDisciplinesListViewModel>
    {
        private readonly IMFDbContext _context;
        private readonly IMapper _mapper;

        public GetTeachersDisciplinesQueryHandler(IMFDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<TeachersDisciplinesListViewModel> Handle(GetTeachersDisciplinesQuery request, CancellationToken cancellationToken)
        {
            var teachersDisciplines = await _context.TeacherDisciplines
                .ProjectTo<TeacherDisciplineLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
            return new TeachersDisciplinesListViewModel()
            {
                TeacherDisciplines = teachersDisciplines
            };
        }
    }
}

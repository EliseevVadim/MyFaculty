using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MyFaculty.Application.Common.Interfaces;
using MyFaculty.Application.Dto;
using MyFaculty.Application.ViewModels;

namespace MyFaculty.Application.Features.Courses.Queries.GetCourses
{
    public class GetCoursesQueryHandler : IRequestHandler<GetCoursesQuery, CoursesListViewModel>
    {
        private IMFDbContext _context;
        private IMapper _mapper;

        public GetCoursesQueryHandler(IMFDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CoursesListViewModel> Handle(GetCoursesQuery request, CancellationToken cancellationToken)
        {
            var courses = await _context.Courses
                .ProjectTo<CourseLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
            return new CoursesListViewModel()
            {
                Courses = courses
            };
        }
    }
}

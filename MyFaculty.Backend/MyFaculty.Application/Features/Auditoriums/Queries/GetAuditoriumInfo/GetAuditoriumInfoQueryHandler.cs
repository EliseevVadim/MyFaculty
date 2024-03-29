﻿using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MyFaculty.Application.Common.Exceptions;
using MyFaculty.Application.Common.Interfaces;
using MyFaculty.Application.ViewModels;
using MyFaculty.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.Auditoriums.Queries.GetAuditoriumInfo
{
    public class GetAuditoriumInfoQueryHandler : IRequestHandler<GetAuditoriumInfoQuery, AuditoriumViewModel>
    {
        private readonly IMFDbContext _context;
        private readonly IMapper _mapper;

        public GetAuditoriumInfoQueryHandler(IMFDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<AuditoriumViewModel> Handle(GetAuditoriumInfoQuery request, CancellationToken cancellationToken)
        {
            Auditorium auditorium = await _context
                .Auditoriums
                .Include(auditorium => auditorium.Floor)
                    .ThenInclude(floor => floor.Faculty)
                .Include(auditorium => auditorium.Teacher)
                .Include(auditorium => auditorium.Pairs)
                    .ThenInclude(pair => pair.PairInfo)
                .Include(auditorium => auditorium.Pairs)
                    .ThenInclude(pair => pair.Group)
                    .ThenInclude(group => group.Course)
                .Include(auditorium => auditorium.Pairs)
                    .ThenInclude(pair => pair.DayOfWeek)
                .Include(auditorium => auditorium.Pairs)
                    .ThenInclude(pair => pair.Discipline)
                .Include(auditorium => auditorium.Pairs)
                    .ThenInclude(pair => pair.PairRepeating)
                .FirstOrDefaultAsync(auditorium => auditorium.Id == request.Id, cancellationToken);
            if (auditorium == null)
                throw new EntityNotFoundException(nameof(Auditorium), request.Id);
            return _mapper.Map<AuditoriumViewModel>(auditorium);
        }
    }
}

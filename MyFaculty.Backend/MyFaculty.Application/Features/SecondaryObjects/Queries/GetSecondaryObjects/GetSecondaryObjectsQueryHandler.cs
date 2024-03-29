﻿using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MyFaculty.Application.Common.Interfaces;
using MyFaculty.Application.Dto;
using MyFaculty.Application.ViewModels;
using System.Threading;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.SecondaryObjects.Queries.GetSecondaryObjects
{
    public class GetSecondaryObjectsQueryHandler : IRequestHandler<GetSecondaryObjectsQuery, SecondaryObjectsListViewModel>
    {
        private readonly IMFDbContext _context;
        private readonly IMapper _mapper;

        public GetSecondaryObjectsQueryHandler(IMFDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<SecondaryObjectsListViewModel> Handle(GetSecondaryObjectsQuery request, CancellationToken cancellationToken)
        {
            var secondaryObjects = await _context.SecondaryObjects
                .ProjectTo<SecondaryObjectLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
            return new SecondaryObjectsListViewModel()
            {
                SecondaryObjects = secondaryObjects
            };
        }
    }
}

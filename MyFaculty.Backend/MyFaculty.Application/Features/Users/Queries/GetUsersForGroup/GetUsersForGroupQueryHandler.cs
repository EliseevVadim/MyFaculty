﻿using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MyFaculty.Application.Common.Interfaces;
using MyFaculty.Application.Dto;
using MyFaculty.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.Users.Queries.GetUsersForGroup
{
    public class GetUsersForGroupQueryHandler : IRequestHandler<GetUsersForGroupQuery, UsersListViewModel>
    {
        private IMFDbContext _context;
        private IMapper _mapper;

        public GetUsersForGroupQueryHandler(IMFDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<UsersListViewModel> Handle(GetUsersForGroupQuery request, CancellationToken cancellationToken)
        {
            var users = await _context.Users
                .Where(user => user.GroupId == request.GroupId)
                .ProjectTo<UserLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
            return new UsersListViewModel()
            {
                Users = users
            };
        }
    }
}
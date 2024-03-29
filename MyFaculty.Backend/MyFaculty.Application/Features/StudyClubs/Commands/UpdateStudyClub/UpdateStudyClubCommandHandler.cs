﻿using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MyFaculty.Application.Common.Exceptions;
using MyFaculty.Application.Common.Interfaces;
using MyFaculty.Application.ViewModels;
using MyFaculty.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.StudyClubs.Commands.UpdateStudyClub
{
    public class UpdateStudyClubCommandHandler : IRequestHandler<UpdateStudyClubCommand, StudyClubViewModel>
    {
        private readonly IMFDbContext _context;
        private readonly IMapper _mapper;

        public UpdateStudyClubCommandHandler(IMFDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<StudyClubViewModel> Handle(UpdateStudyClubCommand request, CancellationToken cancellationToken)
        {
            StudyClub club = await _context.StudyClubs
                .Include(club => club.Moderators)
                .FirstOrDefaultAsync(club => club.Id == request.Id, cancellationToken);
            if (club == null)
                throw new EntityNotFoundException(nameof(StudyClub), request.Id);
            AppUser owner = await _context.Users.FirstOrDefaultAsync(user => user.Id == request.OwnerId);
            if (owner == null)
                throw new EntityNotFoundException(nameof(AppUser), request.OwnerId);
            if (club.OwnerId != request.IssuerId)
                throw new UnauthorizedActionException("Данное действие Вам запрещено.");
            if (request.OwnerId != club.OwnerId && !club.Moderators.Contains(owner))
                club.Moderators.Add(owner);
            club.ClubName = request.StudyClubName;
            club.Description = request.Description;
            club.ImagePath = String.IsNullOrEmpty(request.ImagePath) ? club.ImagePath : request.ImagePath;
            club.OwnerId = request.OwnerId;
            club.Updated = DateTime.Now;
            await _context.SaveChangesAsync(cancellationToken);
            return _mapper.Map<StudyClubViewModel>(club);
        }
    }
}

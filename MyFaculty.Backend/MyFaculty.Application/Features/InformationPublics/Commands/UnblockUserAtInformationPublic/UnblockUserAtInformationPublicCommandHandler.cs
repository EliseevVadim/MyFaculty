﻿using MediatR;
using Microsoft.EntityFrameworkCore;
using MyFaculty.Application.Common.Exceptions;
using MyFaculty.Application.Common.Interfaces;
using MyFaculty.Domain.Entities;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.InformationPublics.Commands.UnblockUserAtInformationPublic
{
    public class UnblockUserAtInformationPublicCommandHandler : IRequestHandler<UnblockUserAtInformationPublicCommand>
    {
        private readonly IMFDbContext _context;

        public UnblockUserAtInformationPublicCommandHandler(IMFDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UnblockUserAtInformationPublicCommand request, CancellationToken cancellationToken)
        {
            AppUser unblockingUser = await _context.Users.FindAsync(new object[] { request.UserId }, cancellationToken);
            if (unblockingUser == null)
                throw new EntityNotFoundException(nameof(AppUser), request.UserId);
            InformationPublic infoPublic = await _context.InformationPublics
                .Include(infoPublic => infoPublic.Moderators)
                .Include(infoPublic => infoPublic.BlockedUsers)
                .FirstOrDefaultAsync(club => club.Id == request.PublicId, cancellationToken);
            if (infoPublic == null || infoPublic.IsBanned)
                throw new EntityNotFoundException(nameof(InformationPublic), request.PublicId);
            if (!infoPublic.BlockedUsers.Contains(unblockingUser))
                throw new DestructiveActionException("Пользователь не заблокирован.");
            if (!infoPublic.Moderators.Any(user => user.Id == request.IssuerId))
                throw new UnauthorizedActionException("Данное действие Вам запрещено.");
            infoPublic.BlockedUsers.Remove(unblockingUser);
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}

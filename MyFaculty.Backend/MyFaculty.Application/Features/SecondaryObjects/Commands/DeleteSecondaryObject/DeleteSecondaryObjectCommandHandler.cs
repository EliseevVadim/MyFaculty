﻿using MediatR;
using MyFaculty.Application.Common.Exceptions;
using MyFaculty.Application.Common.Interfaces;
using MyFaculty.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.SecondaryObjects.Commands.DeleteSecondaryObject
{
    public class DeleteSecondaryObjectCommandHandler : IRequestHandler<DeleteSecondaryObjectCommand>
    {
        private readonly IMFDbContext _context;

        public DeleteSecondaryObjectCommandHandler(IMFDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteSecondaryObjectCommand request, CancellationToken cancellationToken)
        {
            SecondaryObject secondaryObject = await _context.SecondaryObjects.FindAsync(new object[] { request.Id }, cancellationToken);
            if (secondaryObject == null)
                throw new EntityNotFoundException(nameof(SecondaryObject), request.Id);
            _context.SecondaryObjects.Remove(secondaryObject);
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}

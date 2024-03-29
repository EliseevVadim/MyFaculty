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

namespace MyFaculty.Application.Features.SecondaryObjectTypes.Commands.UpdateSecondaryObjectType
{
    public class UpdateSecondaryObjectTypeCommandHandler : IRequestHandler<UpdateSecondaryObjectTypeCommand, SecondaryObjectTypeViewModel>
    {
        private readonly IMFDbContext _context;
        private readonly IMapper _mapper;

        public UpdateSecondaryObjectTypeCommandHandler(IMFDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<SecondaryObjectTypeViewModel> Handle(UpdateSecondaryObjectTypeCommand request, CancellationToken cancellationToken)
        {
            SecondaryObjectType type = await _context.SecondaryObjectTypes.FirstOrDefaultAsync(type => type.Id == request.Id, cancellationToken);
            if (type == null)
                throw new EntityNotFoundException(nameof(SecondaryObjectType), request.Id);
            type.ObjectTypeName = request.ObjectTypeName;
            type.TypePath = request.TypePath;
            type.Updated = DateTime.Now;
            await _context.SaveChangesAsync(cancellationToken);
            return _mapper.Map<SecondaryObjectTypeViewModel>(type);
        }
    }
}

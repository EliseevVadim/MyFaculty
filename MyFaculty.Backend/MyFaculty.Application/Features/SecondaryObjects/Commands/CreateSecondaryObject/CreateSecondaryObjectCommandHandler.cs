﻿using AutoMapper;
using MediatR;
using MyFaculty.Application.Common.Interfaces;
using MyFaculty.Application.ViewModels;
using MyFaculty.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.SecondaryObjects.Commands.CreateSecondaryObject
{
    public class CreateSecondaryObjectCommandHandler : IRequestHandler<CreateSecondaryObjectCommand, SecondaryObjectViewModel>
    {
        private readonly IMFDbContext _context;
        private readonly IMapper _mapper;

        public CreateSecondaryObjectCommandHandler(IMFDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<SecondaryObjectViewModel> Handle(CreateSecondaryObjectCommand request, CancellationToken cancellationToken)
        {
            SecondaryObject secondaryObject = new SecondaryObject()
            {
                ObjectName = request.ObjectName,
                PositionInfo = request.PositionInfo,
                FloorId = request.FloorId,
                SecondaryObjectTypeId = request.SecondaryObjectTypeId,
                Created = DateTime.Now
            };
            await _context.SecondaryObjects.AddAsync(secondaryObject, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return _mapper.Map<SecondaryObjectViewModel>(secondaryObject);
        }
    }
}

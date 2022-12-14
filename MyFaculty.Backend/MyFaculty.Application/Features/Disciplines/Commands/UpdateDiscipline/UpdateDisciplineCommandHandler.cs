using MyFaculty.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using MyFaculty.Domain.Entities;
using System.Threading;
using Microsoft.EntityFrameworkCore;
using MyFaculty.Application.Common.Exceptions;
using MyFaculty.Application.ViewModels;
using AutoMapper;

namespace MyFaculty.Application.Features.Disciplines.Commands.UpdateDiscipline
{
    public class UpdateDisciplineCommandHandler : IRequestHandler<UpdateDisciplineCommand, DisciplineViewModel>
    {
        private IMFDbContext _context;
        private IMapper _mapper;

        public UpdateDisciplineCommandHandler(IMFDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<DisciplineViewModel> Handle(UpdateDisciplineCommand request, CancellationToken cancellationToken)
        {
            Discipline discipline = await _context.Disciplines.FirstOrDefaultAsync(discipline => discipline.Id == request.Id, cancellationToken);
            if (discipline == null)
                throw new EntityNotFoundException(nameof(Discipline), request.Id);
            discipline.DisciplineName = request.DisciplineName;
            discipline.Updated = DateTime.Now;
            await _context.SaveChangesAsync(cancellationToken);
            return _mapper.Map<DisciplineViewModel>(discipline);
        }
    }
}

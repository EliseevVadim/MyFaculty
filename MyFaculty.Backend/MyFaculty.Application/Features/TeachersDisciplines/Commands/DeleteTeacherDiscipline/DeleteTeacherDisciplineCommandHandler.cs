﻿using MediatR;
using MyFaculty.Application.Common.Exceptions;
using MyFaculty.Application.Common.Interfaces;
using MyFaculty.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.TeachersDisciplines.Commands.DeleteTeacherDiscipline
{
    public class DeleteTeacherDisciplineCommandHandler : IRequestHandler<DeleteTeacherDisciplineCommand>
    {
        private readonly IMFDbContext _context;

        public DeleteTeacherDisciplineCommandHandler(IMFDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteTeacherDisciplineCommand request, CancellationToken cancellationToken)
        {
            TeacherDiscipline teacherDiscipline = await _context.TeacherDisciplines.FindAsync(new object[] { request.Id }, cancellationToken);
            if (teacherDiscipline == null)
                throw new EntityNotFoundException(nameof(TeacherDiscipline), request.Id);
            _context.TeacherDisciplines.Remove(teacherDiscipline);
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}

﻿using AutoMapper;
using MediatR;
using MyFaculty.Application.Common.Exceptions;
using MyFaculty.Application.Common.Interfaces;
using MyFaculty.Application.ViewModels;
using MyFaculty.Domain.Entities;
using MyFaculty.Domain.Enums;
using System.Threading;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.TaskSubmissions.Commands.DeleteTaskSubmission
{
    public class DeleteTaskSubmissionCommandHandler : IRequestHandler<DeleteTaskSubmissionCommand, TaskSubmissionViewModel>
    {
        private readonly IMFDbContext _context;
        private readonly IMapper _mapper;

        public DeleteTaskSubmissionCommandHandler(IMFDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<TaskSubmissionViewModel> Handle(DeleteTaskSubmissionCommand request, CancellationToken cancellationToken)
        {
            TaskSubmission deletingSubmission = await _context.TaskSubmissions.FindAsync(new object[] { request.Id }, cancellationToken);
            if (deletingSubmission == null)
                throw new EntityNotFoundException(nameof(TaskSubmission), request.Id);
            if (deletingSubmission.AuthorId != request.IssuerId)
                throw new UnauthorizedActionException("Данное действие Вам запрещено.");
            if (deletingSubmission.Status != TaskSubmissionStatus.SentForEvaluation)
                throw new DestructiveActionException("Вы не можете удалить это решение, поскольку оно уже оценено.");
            _context.TaskSubmissions.Remove(deletingSubmission);
            await _context.SaveChangesAsync(cancellationToken);
            return _mapper.Map<TaskSubmissionViewModel>(deletingSubmission);
        }
    }
}

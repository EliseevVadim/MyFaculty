﻿using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MyFaculty.Application.Common.Exceptions;
using MyFaculty.Application.Common.Interfaces;
using MyFaculty.Application.ViewModels;
using MyFaculty.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.Comments.Commands.DeleteComment
{
    public class DeleteCommentCommandHandler : IRequestHandler<DeleteCommentCommand, CommentViewModel>
    {
        private IMFDbContext _context;
        private IMapper _mapper;

        public DeleteCommentCommandHandler(IMFDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CommentViewModel> Handle(DeleteCommentCommand request, CancellationToken cancellationToken)
        {
            Comment deletingComment = await _context.Comments
                .Include(comment => comment.Post)
                .FirstOrDefaultAsync(comment => comment.Id == request.Id, cancellationToken);
            if (deletingComment == null)
                throw new EntityNotFoundException(nameof(Comment), request.Id);
            if (deletingComment.AuthorId != request.IssuerId)
                throw new UnauthorizedActionException("Данное действие Вам запрещено.");
            if (deletingComment.Post is InfoPost)
            {
                InfoPost infoPost = await _context.InfoPosts
                    .Include(infoPost => infoPost.OwningInformationPublic)
                        .ThenInclude(infoPublic => infoPublic.BlockedUsers)
                    .FirstOrDefaultAsync(infoPost => infoPost.Id == deletingComment.Post.Id);
                if (infoPost.OwningInformationPublic != null
                    && infoPost
                        .OwningInformationPublic
                        .BlockedUsers
                        .Select(user => user.Id)
                        .Contains(request.IssuerId))
                    throw new DestructiveActionException("Вы не можете удалить комментарий к этой записи, так как были заблокированы " +
                        "в сообществе, содержащем ее");
            }
            _context.Comments.Remove(deletingComment);
            await _context.SaveChangesAsync(cancellationToken);
            return _mapper.Map<CommentViewModel>(deletingComment);
        }
    }
}
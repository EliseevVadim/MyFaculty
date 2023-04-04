﻿using MediatR;
using MyFaculty.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.Newsfeed.Queries.GenerateTasksNewsfeedForUser
{
    public class GenerateTasksNewsfeedForUserQuery : IRequest<ClubTasksListViewModel>
    {
        public int UserId { get; set; }
    }
}

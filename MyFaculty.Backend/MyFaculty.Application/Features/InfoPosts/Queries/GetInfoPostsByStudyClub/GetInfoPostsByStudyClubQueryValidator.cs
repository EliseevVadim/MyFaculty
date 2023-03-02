﻿using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.InfoPosts.Queries.GetInfoPostsByStudyClub
{
    public class GetInfoPostsByStudyClubQueryValidator : AbstractValidator<GetInfoPostsByStudyClubQuery>
    {
        public GetInfoPostsByStudyClubQueryValidator()
        {
            RuleFor(query => query.StudyClubId).NotEmpty();
        }
    }
}
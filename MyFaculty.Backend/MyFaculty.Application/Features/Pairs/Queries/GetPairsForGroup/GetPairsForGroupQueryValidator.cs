using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace MyFaculty.Application.Features.Pairs.Queries.GetPairsForGroup
{
    public class GetPairsForGroupQueryValidator : AbstractValidator<GetPairsForGroupQuery>
    {
        public GetPairsForGroupQueryValidator()
        {
            RuleFor(query => query.GroupId).NotEmpty();
        }
    }
}

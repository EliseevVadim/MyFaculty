using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace MyFaculty.Application.Features.ScienceRanks.Commands.DeleteScienceRank
{
    public class DeleteScienceRankCommand : IRequest
    {
        public int Id { get; set; }
    }
}

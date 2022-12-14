using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using MyFaculty.Application.ViewModels;
using MyFaculty.Domain.Entities;

namespace MyFaculty.Application.Features.PairInfos.Commands.CreatePairInfo
{
    public class CreatePairInfoCommand : IRequest<PairInfoViewModel>
    {
        public int PairNumber { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
    }
}

﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.InformationPublics.Commands.DemoteInformationPublicModerator
{
    public class DemoteInformationPublicModeratorCommand : IRequest
    {
        public int IssuerId { get; set; }
        public int InformationPublicId { get; set; }
        public int ModeratorId { get; set; }
    }
}

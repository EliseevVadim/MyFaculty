﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.InformationPublics.Commands.UnbanInformationPublic
{
    public class UnbanInformationPublicCommand : IRequest
    {
        public int UnbannedPublicId { get; set; }
        public int AdministratorId { get; set; }
        public string Reason { get; set; }
    }
}

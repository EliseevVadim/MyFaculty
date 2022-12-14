using MediatR;
using MyFaculty.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.Faculties.Commands.UpdateFaculty
{
    public class UpdateFacultyCommand : IRequest<FacultyViewModel>
    {
        public int Id { get; set; }
        public string FacultyName { get; set; }
        public string OfficialWebsite { get; set; }
    }
}

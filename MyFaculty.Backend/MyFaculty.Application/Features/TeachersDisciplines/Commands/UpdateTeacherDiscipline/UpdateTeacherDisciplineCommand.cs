using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using MyFaculty.Application.ViewModels;
using MyFaculty.Domain.Entities;

namespace MyFaculty.Application.Features.TeachersDisciplines.Commands.UpdateTeacherDiscipline
{
    public class UpdateTeacherDisciplineCommand : IRequest<TeacherDisciplineViewModel>
    {
        public int Id { get; set; }
        public int TeacherId { get; set; }
        public int DisciplineId { get; set; }
    }
}

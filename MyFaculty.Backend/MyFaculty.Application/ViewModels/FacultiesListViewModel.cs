﻿using MyFaculty.Application.Dto;
using System.Collections.Generic;

namespace MyFaculty.Application.ViewModels
{
    public class FacultiesListViewModel
    {
        public IList<FacultyLookupDto> Faculties { get; set; }
    }
}

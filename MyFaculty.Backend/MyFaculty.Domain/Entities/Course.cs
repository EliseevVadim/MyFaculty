﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyFaculty.Domain.Entities
{
    public class Course
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string CourseName { get; set; }
        public int CourseNumber { get; set; }
        public int? FacultyId { get; set; }
        public Faculty Faculty { get; set; }
        public List<Group> Groups { get; set; } = new();
        public List<AppUser> Students { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
    }
}

﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyFaculty.Domain.Entities
{
    public class Teacher
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string FIO { get; set; }
        public string PhotoPath { get; set; }
        public string Email { get; set; }
        public Guid VerifiactionToken { get; set; }
        public DateTime BirthDate { get; set; }
        public int ScienceRankId { get; set; }
        public ScienceRank ScienceRank { get; set; }
        public List<Auditorium> Auditoriums { get; set; } = new();
        public List<Pair> Pairs { get; set; } = new();
        public List<TeacherDiscipline> TeacherDisciplines { get; set; } = new();
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
    }
}

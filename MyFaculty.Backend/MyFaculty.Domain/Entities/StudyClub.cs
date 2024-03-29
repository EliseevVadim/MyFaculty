﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyFaculty.Domain.Entities
{
    public class StudyClub
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string ClubName { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public int OwnerId { get; set; }
        public AppUser Owner { get; set; }
        public List<AppUser> Members { get; set; } = new List<AppUser>();
        public List<AppUser> Moderators { get; set; } = new List<AppUser>();
        public List<ClubTask> ClubTasks { get; set; }
        public List<InfoPost> InfoPosts { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
    }
}

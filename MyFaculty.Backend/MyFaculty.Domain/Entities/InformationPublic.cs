﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFaculty.Domain.Entities
{
    public class InformationPublic
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string PublicName { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public int OwnerId { get; set; }
        public AppUser Owner { get; set; }
        public List<AppUser> Members { get; set; } = new List<AppUser>();
        public List<AppUser> BlockedUsers { get; set; } = new List<AppUser>();
        public List<InfoPost> InfoPosts { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
    }
}
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFaculty.Domain.Entities
{
    public class AppUser
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string AvatarPath { get; set; }
        public DateTime? BirthDate { get; set; }
        public int? CityId { get; set; }
        public string Website { get; set; }
        public string VKLink { get; set; }
        public string TelegramLink { get; set; }
        public City City { get; set; }
    }
}
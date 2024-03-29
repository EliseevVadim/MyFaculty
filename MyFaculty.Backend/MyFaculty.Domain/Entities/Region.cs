﻿using System;
using System.Collections.Generic;

namespace MyFaculty.Domain.Entities
{
    public class Region
    {
        public int Id { get; set; }
        public string RegionName { get; set; }
        public int CountryId { get; set; }
        public Country Country { get; set; }
        public List<City> Cities { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
    }
}

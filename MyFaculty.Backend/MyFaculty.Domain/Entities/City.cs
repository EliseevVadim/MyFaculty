using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFaculty.Domain.Entities
{
    public class City
    {
        public int Id { get; set; }
        public string CityName { get; set; }
        public int RegionId { get; set; }
        public Region Region { get; set; }
        public List<AppUser> Users { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
    }
}

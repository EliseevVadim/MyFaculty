using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFaculty.Domain.Entities
{
    public class Auditorium
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string AuditoriumName { get; set; }
        public string PositionInfo { get; set; }
        public int FloorId { get; set; }
        public Floor Floor { get; set; }
        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; }
        public List<Pair> Pairs { get; set; } = new();
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
    }
}

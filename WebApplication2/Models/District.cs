using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication2.Models
{
    public class District
    {
        [Key]
        public int idDistrict { get; set; }

        public string name { get; set; }

        [ForeignKey("Region")]
        public int idRegion { get; set; }

        public virtual Region Region { get; set; }

        public virtual List<Locality> Localities { get; set; } = new List<Locality>();
    }
}

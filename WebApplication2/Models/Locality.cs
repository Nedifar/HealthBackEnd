using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication2.Models
{
    public class Locality
    {
        [Key]
        public int idLocality { get; set; }

        public string name { get; set; }

        [ForeignKey("District")]
        public int idDistrict { get; set; }

        public virtual District District { get; set; }

        public virtual List<Street> Streets { get; set; } = new List<Street>();
    }
}

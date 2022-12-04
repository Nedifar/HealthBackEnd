using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication2.Models
{
    public class CampPhoto
    {
        [Key]
        public int idCampPhoto { get; set; }

        public string url { get; set; }

        [ForeignKey("Camp")]
        public int idCamp { get; set; }

        public virtual Camp Camp { get; set; }
    }
}

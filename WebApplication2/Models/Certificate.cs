using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication2.Models
{
    public class Certificate
    {
        [Key]
        public int idCertificate { get; set; }

        public string url { get; set; }

        [ForeignKey("Camp")]
        public int idCamp { get; set; }

        public virtual Camp Camp { get; set; }
    }
}

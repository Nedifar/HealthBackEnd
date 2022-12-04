using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication2.Models
{
    public class Street
    {
        [Key]
        public int idStreet { get; set; }

        public string name { get; set; }

        [ForeignKey("Locality")]
        public int idLocality { get; set; }

        public virtual Locality Locality { get; set; }
    }
}

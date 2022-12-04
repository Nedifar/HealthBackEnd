using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication2.Models
{
    public class RegistrationAddress
    {
        [Key]
        public int idRegistrationAddress { get; set; }

        public string index { get; set; }

        [ForeignKey("Citizenship")]
        public int? idCitizenship { get; set; }

        public virtual Citizenship Citizenship { get; set; }

        public string region { get; set; }

        public string district { get; set; }

        public string locality { get; set; }

        public string street { get; set; }

        public string houseHumber { get; set; }

        public string housing { get; set; }

        public string flat { get; set; }

        public virtual List<Child> Children { get; set; } = new List<Child>();

        public virtual List<Parent> Parents { get; set; } = new List<Parent>();
    }
}

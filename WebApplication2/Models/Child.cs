using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication2.Models
{
    public class Child
    {
        [Key]
        public int idChild { get; set; }

        [ForeignKey("Citizenship")]
        public int? idCitizenship { get; set; }

        public virtual Citizenship Citizenship { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string MiddleName { get; set; }

        public DateTime Birthday { get; set; }

        [ForeignKey("PersonalDocument")]
        public int? idPersonalDocument { get; set; }

        public virtual PersonalDocument PersonalDocument { get; set; }

        [ForeignKey("RegistrationAddress")]
        public int? idRegistrationAddress { get; set; }

        public virtual RegistrationAddress RegistrationAddress { get; set; }

        [ForeignKey("FactAddress")]
        public int? idFactAddress { get; set; }

        public virtual FactAddress FactAddress { get; set; }

        public string Snils { get; set; }

        public string telephoneNumber { get; set; }

        [ForeignKey("Parent")]
        public int idParent { get; set; }

        public virtual Parent Parent { get; set; }

    }
}

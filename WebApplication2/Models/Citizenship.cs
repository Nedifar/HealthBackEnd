using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Models
{
    public class Citizenship
    {
        [Key]
        public int idCitizenship { get; set; }

        public string name { get; set; }

        public string fullName { get; set; }

        public virtual List<Parent> Parents { get; set; } = new List<Parent>();

        public virtual List<Child> Childs { get; set; } = new List<Child>();

        public virtual List<FactAddress> Facts { get; set; } = new List<FactAddress>();
    }
}

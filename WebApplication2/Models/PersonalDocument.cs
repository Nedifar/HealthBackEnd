using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Models
{
    public class PersonalDocument
    {
        [Key]
        public int idPersonalDocument { get; set; }

        public string series { get; set; }

        public string number { get; set; }

        public DateTime dateOfIssue { get; set; }

        public string issuedBy { get; set; }

        public PlacementDocument PlacementDocument { get; set; }

        public TypePersonalDocument TypePersonalDocument { get; set; }

        public DateTime? validityTime { get; set; }

        public virtual List<Parent> Parents { get; set; } = new List<Parent>();

        public virtual List<Child> Childs { get; set; } = new List<Child>();

    }

    public enum PlacementDocument
    {
        Russian,
        Other
    }

    public enum TypePersonalDocument
    {
        BirthCertificate,
        Passport
    }
}

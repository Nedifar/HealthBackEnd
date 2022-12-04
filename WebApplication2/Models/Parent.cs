using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebApplication2.ChatModels;

namespace WebApplication2.Models
{
    public class Parent
    {
        [Key]
        public int idParent { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }
        
        public ParentStatus ParentStatus { get; set; }

        [ForeignKey("Citizenship")]
        public int? idCitizenship { get; set; }

        public virtual Citizenship Citizenship { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string MiddleName { get; set; }

        public DateTime? Birthday { get; set; }

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

        public bool emailIsConfirmed { get; set; } = false;

        public int confirmCode { get; set; }

        public virtual List<Child> Children { get; set; } = new List<Child>();

        public virtual List<Request> Requests { get; set; } = new List<Request>();

        public virtual List<Feedback> Feedbacks { get; set; } = new List<Feedback>();

        public virtual List<Dialog> Dialogs { get; set; } = new List<Dialog>();

        public virtual List<Message> Messages { get; set; } = new List<Message>();
    }

    public enum ParentStatus
    {
        Parent,
        Representative
    }
}

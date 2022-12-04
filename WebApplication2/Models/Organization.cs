using Org.BouncyCastle.Bcpg;
using System.ComponentModel.DataAnnotations;
using WebApplication2.ChatModels;

namespace WebApplication2.Models
{
    public class Organization
    {
        [Key]
        public int IdOrganization { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public virtual List<Camp> Camps { get; set; } = new List<Camp>();

        public virtual List<Dialog> Dialogs { get; set; } = new List<Dialog>();

        public virtual List<Message> Messages { get; set; } = new List<Message>();

        public string INN { get; set; }

        public string BIK { get; set; }

        public string KPP { get; set; }

        public string OGRN { get; set; }

        public string OKPO { get; set; }

        public string CheckNumber { get; set; }

        public string CheckCorres { get; set; }

        public string Director { get; set; } 

    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebApplication2.Models;

namespace WebApplication2.ChatModels
{
    public class Dialog
    {
        [Key]
        public int idDialog { get; set; }

        public string name { get; set; }

        [ForeignKey("Parent")]
        public int idParent { get; set; }

        public virtual Parent Parent { get; set; }

        [ForeignKey("Organization")]
        public int idOrganization { get; set; }

        public virtual Organization Organization { get; set; }

        public virtual List<Message> Messages { get; set; } = new List<Message>();
    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebApplication2.Models;

namespace WebApplication2.ChatModels
{
    public class Message
    {
        [Key]
        public int idMessage { get; set; }

        public string textMessage { get; set; }

        [ForeignKey("Dialog")]
        public int idDialog { get; set; }

        public virtual Dialog Dialog { get; set; }
         
        public DateTime messageTime { get; set; }

        [ForeignKey("Parent")]
        public int? idParent { get; set; }

        public virtual Parent Parent { get; set; }

        [ForeignKey("Organization")]
        public int? idOrganization { get; set; }

        public virtual Organization Organization { get; set; }
    }
}

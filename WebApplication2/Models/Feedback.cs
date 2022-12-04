using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication2.Models
{
    public class Feedback
    {
        [Key]
        public int idFeedback { get; set; }

        [ForeignKey("Parent")]
        public int idParent { get; set; }

        public virtual Parent Parent { get; set; }

        public DateTime datePublished { get; set; }

        public string comment { get; set; }

        public int estimation { get; set; }

        [ForeignKey("Shift")]
        public int idShift { get; set; }

        public virtual Shift Shift { get; set; }
    }
}

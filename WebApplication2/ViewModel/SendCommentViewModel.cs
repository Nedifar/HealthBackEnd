using System.ComponentModel.DataAnnotations.Schema;
using WebApplication2.Models;

namespace WebApplication2.ViewModel
{
    public class SendCommentViewModel
    {
        public DateTime DatePublished { get; set; } = DateTime.Now;

        public string Comment { get; set; }

        public int Estimation { get; set; }

        public int idShift { get; set; }
    }
}

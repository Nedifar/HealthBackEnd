using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication2.Models
{
    public class Shift
    {
        [Key]
        public int idShift { get; set; }

        public string ShiftName { get; set; }

        public DateTime DateBegin { get; set; }

        public DateTime DateEnd { get; set; }

        public SeasonCamp SeasonCamp { get; set; }

        [NotMapped]
        public string Duration
        {
            get
            {
                return (DateEnd.Date - DateBegin.Date).Days + " дней";
            }
        }

        public double Price { get; set; }

        [ForeignKey("Camp")]
        public int idCamp { get; set; }

        public virtual Camp Camp { get; set; }

        public int Capacity { get; set; }

        [NotMapped]
        public int FreeSeats
        {
            get
            {
                return Capacity - Requests.Count;
            }
        }

        public virtual List<Request> Requests { get; set; } = new List<Request>();

        public virtual List<Feedback> Feedbacks { get; set; } = new List<Feedback>();
    }

    public enum SeasonCamp
    {
        Winter,
        Spring,
        Summer,
        Autumn
    }
}

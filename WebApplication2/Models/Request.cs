using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication2.Models
{
    public class Request
    {
        [Key]
        public int idRequest { get; set; }

        [ForeignKey("Child")]
        public int idChild { get; set; }

        public virtual Child Child { get; set; }

        public double AmountToBePaid { get; set; }

        public PaymentType PaymentType { get; set; }

        public bool IsPaid { get; set; }

        [ForeignKey("Shift")]
        public int idShift { get; set; }

        public virtual Shift Shift { get; set; }

        public bool isConfirmed { get; set; }
    }

    public enum PaymentType
    {
        Online,
        InBank
    }
}

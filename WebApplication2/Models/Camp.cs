using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Models
{
    public class Camp
    {
        [Key]
        public int idCamp { get; set; }

        public string campName { get; set; }

        public string Description { get; set; }

        public TypeCamp TypeCamp { get; set; }

        public string Address { get; set; }

        public string WorkingMode { get; set; }

        public string supportTelephone { get; set; }

        public int housingCount { get; set; }

        public virtual List<Shift> Shifts { get; set; } = new List<Shift>();

        public virtual List<CampPhoto> CampPhotos { get; set; } = new List<CampPhoto>();

        public virtual List<Certificate> Certificate { get; set; } = new List<Certificate>();

        public double territoryArea { get; set; }

        public string foodInformation { get; set; }

        public bool haveSportObjects { get; set; }

        public string TermsAndPayment { get; set; }

        public int IdOrganization { get; set; }

        public virtual Organization Organization { get; set; }
    }

    public enum TypeCamp
    {
        StationaryCamp,
        TentMilitaryPatrioticCamp,
        TentTouristCamp
    }
}

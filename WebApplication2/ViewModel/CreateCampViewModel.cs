using System.Text.Json.Serialization;
using WebApplication2.Models;

namespace WebApplication2.ViewModel
{
    public class CreateCampViewModel
    {
        public string CampName { get; set; }

        public string Description { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public TypeCamp TypeCamp { get; set; }

        public string Address { get; set; }

        public string WorkingMode { get; set; }

        public string SupportTelephone { get; set; }

        public int HousingCount { get; set; }

        public double TerritoryArea { get; set; }

        public string FoodInformation { get; set; }

        public bool HaveSportObjects { get; set; }

        public string TermsAndPayment { get; set; }
    }
}

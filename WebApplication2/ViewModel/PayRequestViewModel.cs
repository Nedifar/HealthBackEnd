using System.Text.Json.Serialization;
using WebApplication2.Models;

namespace WebApplication2.ViewModel
{
    public class PayRequestViewModel
    {
        public int idRequest { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public PaymentType PaymentType { get; set; }
    }
}

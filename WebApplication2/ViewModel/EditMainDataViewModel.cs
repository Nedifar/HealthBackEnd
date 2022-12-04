using System.Text.Json.Serialization;
using WebApplication2.Models;

namespace WebApplication2.ViewModel
{
    public class EditMainDataViewModel
    {
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public ParentStatus ParentStatus { get; set; }

        public string Citizenship { get; set; }

        public string LastName { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public DateTime? Birthday { get; set; }

        public string Telephone { get; set; }

        public string Snils { get; set; }

    }
}

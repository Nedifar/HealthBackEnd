using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using WebApplication2.Models;

namespace WebApplication2.ViewModel
{
    public class CreateShiftViewModel
    {
        public string ShiftName { get; set; }

        public DateTime DateBegin { get; set; }

        public DateTime DateEnd { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public SeasonCamp SeasonCamp { get; set; }

        public double Price { get; set; }

        public int Capacity { get; set; }

        public int idCamp { get; set;  }
    }
}

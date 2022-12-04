using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Models
{
    public class Region
    {
        [Key]
        public int idRegion { get; set; }

        public string regionName { get; set; }

        public virtual List<District> Districts { get; set; } = new List<District>();
    }
}

using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Models
{
    public class Admin
    {
        [Key]
        public int idAdmin { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }
    }
}

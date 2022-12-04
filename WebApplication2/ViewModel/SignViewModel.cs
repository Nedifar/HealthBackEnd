using System.ComponentModel.DataAnnotations;

namespace WebApplication2.ViewModel
{
    public class SignViewModel
    {
        [Required]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}

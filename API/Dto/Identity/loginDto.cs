using System.ComponentModel.DataAnnotations;

namespace API.Dto.Identity
{
    public class loginDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }


        public string Password { get; set; }
    }
}

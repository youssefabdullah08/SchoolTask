using System.ComponentModel.DataAnnotations;

namespace DashBoared.DTOS
{
    public class LoginDto
    {
       
        [Required(ErrorMessage = "email is required")]
        [EmailAddress][StringLength(128)] public string? StudentEmail { get; set; }
        [Required(ErrorMessage = "password is required")]
        [StringLength(128)] public string Password { get; set; }
    }
}

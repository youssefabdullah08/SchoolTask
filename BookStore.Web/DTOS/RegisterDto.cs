using System.ComponentModel.DataAnnotations;

namespace DashBoared.DTOS
{
    public class RegisterDto
    {
        [Required(ErrorMessage = "first name is required")]
        [StringLength(128)] public string FristName { get; set; }
        [Required(ErrorMessage = "last name is required")]
        [StringLength(128)] public string LastName { get; set; }
        [Required(ErrorMessage = "email is required")]
        [EmailAddress][StringLength(128)]
        public string? StudentEmail { get; set; }
        [Required(ErrorMessage = "password is required")]
        [StringLength(128)] public string Password { get; set; }
        [Required(ErrorMessage = "confirm password is required")]
        [StringLength(128)] public string ConfirmPassword { get; set; }


    }
}

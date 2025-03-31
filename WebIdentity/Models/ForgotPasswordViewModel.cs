using System.ComponentModel.DataAnnotations;

namespace WebIdentity.Models
{
    public class ForgotPasswordViewModel
    {
        [Required, EmailAddress]
        public string Email { get; set; }
    }
}

using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace WebIdentity.Entities
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required, MaxLength(80, ErrorMessage = "O nome não pode exceder 80 caracteres")]
        public string? Name { get; set; }

        [EmailAddress]
        [Required, MaxLength(120)]
        public string? Email { get; set; }

        public int Idade { get; set; }

        //public List<string> Roles { get; set; } = new List<string>();
    }
}

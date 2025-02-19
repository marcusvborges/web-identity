using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebIdentity.Entities;
using WebIdentity.Enums;

namespace WebIdentity.Entities
{
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [MaxLength(80, ErrorMessage = "The name cannot exceed 80 characters")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "E-mail is required")]
        [EmailAddress]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Date of birth is required")]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        [Required(ErrorMessage = "Phone number is required")]
        [Phone(ErrorMessage = "Invalid phone number")]
        [RegularExpression(@"^\(\d{2}\) \d{5}-\d{4}$", ErrorMessage = "Invalid format. Use (XX) XXXXX-XXXX")]
        public string? PhoneNumber { get; set; }

        [Required(ErrorMessage = "Admission date is required")]
        [DataType(DataType.Date)]
        public DateTime DateAdmission { get; set; }

        [Required(ErrorMessage = "Sector is required")]
        public int SectorId { get; set; }
        public virtual Sector? Sector {  get; set; }

        [Required(ErrorMessage = "Department is required")]
        public int DepartmentId { get; set; }
        public virtual Department? Department { get; set; }

        [Required(ErrorMessage = "The level is required")]
        public HierarchicalLevel HierarchicalLevel {  get; set; }

    }
}

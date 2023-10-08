using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace WebApplication4.Models
{
    public class UserViewModel
    {
        public long Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [Range(0, 1000, ErrorMessage = "Age must be between {1} and {2}.")]
        public int Age { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [ValidateNever]
        public List<string> Roles { get; set; }
    }
}
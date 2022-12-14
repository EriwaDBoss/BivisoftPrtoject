using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BiviProject.Models
{
    public class ApplicationUser: IdentityUser
    {
        public bool IsAdmin { get; set; }

        //[NotMapped]
        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }

        //[NotMapped]
        [Required(ErrorMessage = "Confirmation Password is required.")]
        [Compare("Password", ErrorMessage = "Password and Confirmation Password must match.")]
        public string ConfirmPassword { get; set; }
        public DateTime DateCreated { get; set; }

    }
}

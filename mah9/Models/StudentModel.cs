using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace mah9.Models
{
    public class StudentModel
    {
        [Required]
        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        //  [Required]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        public string Class { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email Adress")]
        public string Email { get; set; }

        public string PasswordHash { get; set; }
        public string UserId { get; set; }
        public string SecurityStamp { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool twofac { get; set; }
        public bool lockout { get; set; }
        public string lockoutend { get; set; }
        public string Username { get; set; }
        public int AcecssFailed { get; set; }

        public string Role { get; set; }
        public string Password { get; set; }
        public string Roles { get; set; }
        public string Subject{ get; set; }
        public string userroless { get; set; }

        public int SeconID { get; set; }

        public DateTime Date { get; set; }
    }
}
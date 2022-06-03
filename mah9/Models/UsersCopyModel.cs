using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace mah9.Models
{
    public class UsersCopyModel
    {
        [Key]
        public int ID { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string UserName { get; set; }
        public string Class { get; set; }
        public string Password { get; set; }
    }
}
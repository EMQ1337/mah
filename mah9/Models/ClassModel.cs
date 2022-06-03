using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace mah9.Models
{
    public class ClassModel
    {
        [Key]
        public int ClassID { get; set; }
        [Display(Name = "Class Name")]
        public string ClassName { get; set; }
        public string OldClassName { get; set; }
        public DateTime Date { get; set; }

    }
}
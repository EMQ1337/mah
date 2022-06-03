using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace mah9.Models
{
    public class SubjectModel
    {
        [Key]
        public int subjectID { get; set; }
        public string subject { get; set; }
        public string Oldsubject { get; set; }
        public DateTime Date { get; set; }

    }
}
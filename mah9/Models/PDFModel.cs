using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace mah9.Models
{
    public class PDFModel
    {
        [Key]
        public int PDFID { get; set; }
        [Required]
        [Display(Name = "PDF URL")]
        public string PDFurl { get; set; }
        [Required]
        [Display(Name = "PDF Name")]
        public string PDFName { get; set; }
        [Display(Name = "PDF Subject")]
        public string PDFSubject { get; set; }
        [Display(Name = "PDF Class")]
        public string PDFClass { get; set; }

        public DateTime Date { get; set; }
    }
}
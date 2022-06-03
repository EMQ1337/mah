using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace mah9.Models
{
    public class VideoModel
    {
        [Key]
        public int VideoID { get; set; }

        [Required]
        [Display(Name = "Video URL")]
        public string VideoURL { get; set; }

        [Required]
        [Display(Name = "Video Name")]
        public string VideoName { get; set; }

        [Display(Name = "Video Subject")]
        public string VideoSubject { get; set; }

        [Display(Name = "Video Class")]
        public string VideoClass { get; set; }

        public DateTime Date { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApiLibrary.Models
{
    public class ServiceBooks
    {
        [Key]
        public int serviceBookId { get; set; }

        [Required(ErrorMessage = "please enter Book Name")]
        [Display(Name = "Book Name")]
        public string serviceBookName { get; set; }

        [Required(ErrorMessage = "please enter serial number")]
        [Display(Name = "Serial Number")]
        public string serviceSerialNumber { get; set; }

        [Required(ErrorMessage = "please enter Author Name")]
        [Display(Name = "Author Name")]
        public string serviceAuthorName { get; set; }

        [Required(ErrorMessage = "please enter Branch")]
        [Display(Name = "Branch")]
        public string serviceBranch { get; set; }

        [Required(ErrorMessage = "please enter Publications")]
        [Display(Name = "Publications")]
        public string servicePublications { get; set; }

        [Display(Name = "IsAvailable")]
        public bool serviceIsAvailable { get; set; }
    }
}
   
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApiLibrary.Models
{
	public class ServiceIssuedBooks
	{
        [Key]
        public int serviceIssuedId { get; set; }

        public int bookId { get; set; }
        [ForeignKey("bookId")]
        public ServiceBooks serviceBooks { get; set; }

        public int studentId { get; set; }
        [ForeignKey("studentId")]
        public ServiceStudents serviceStudents { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        [Required]
        [Display(Name = "From date")]
        public DateTime serviceFromDate { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MMM/yyyy}")]
        [Required]
        [Display(Name = "To date")]
        public DateTime serviceToDate { get; set; }
    }
}
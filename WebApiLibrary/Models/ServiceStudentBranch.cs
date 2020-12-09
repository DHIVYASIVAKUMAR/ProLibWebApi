using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApiLibrary.Models
{
	public class ServiceStudentBranch
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int serviceStudentBranchId { get; set; }

		[Required(ErrorMessage = "Please enter branch name")]
		[Display(Name = "Branch")]
		public string serviceStudentBranch { get; set; }
	}
}
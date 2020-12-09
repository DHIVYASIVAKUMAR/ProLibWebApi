using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApiLibrary.Models
{
	public class ServiceAuthor
	{

		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int serviceAuthorId { get; set; }

		[Required(ErrorMessage = "please enter Author Name")]
		[Display(Name = "Author Name")]
		public string serviceAuthorName { get; set; }
	}
	public class ServiceBookBranch
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int serviceBookBranchId { get; set; }

		[Required(ErrorMessage = "please enter Branch")]
		[Display(Name = "Branch")]
		public string serviceBranch { get; set; }
	}

	public class ServiceBookPublication
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int serviceBookPublicationId { get; set; }

		[Required(ErrorMessage = "please enter Publications")]
		[Display(Name = "Publications")]
		public string servicePublications { get; set; }
	}
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiLibrary.ViewModels
{
	public class ServiceIssuedBookViewModel
	{
		public int serviceBookId { get; set; }
		public string serviceBookName { get; set; }
		public string serviceAuthorName { get; set; }
		public string serviceBranch { get; set; }
		public string servicePublication { get; set; }
		public string serviceStudentName { get; set; }
		public string serviceStudentEmail { get; set; }
		public DateTime serviceFromDate { get; set; }
		public DateTime serviceToDate { get; set; }
		public int serviceIssuedId { get; set; }
		public string serviceDisplayFromDate { get; set; }
		public string serviceDisplayToDate { get; set; }
	}
}
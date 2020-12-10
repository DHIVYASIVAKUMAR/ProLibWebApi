using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApiLibrary.Models;

namespace WebApiLibrary.ViewModels
{
	public class ServiceBooksViewModel
	{
		public ServiceBooks book { get; set; }
		public List<ServiceAuthor> authors { get; set; }
		public List<ServiceBookBranch> bookBranches { get; set; }
		public List<ServiceBookPublication> bookPublications { get; set; }
	}
}
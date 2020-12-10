using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApiLibrary.Models;

namespace WebApiLibrary.ViewModels
{
	public class ServiceStudentViewModel
	{
		public ServiceStudents students { get; set; }
		public List<ServiceStudentBranch> studentBranches { get; set; }
	}
}
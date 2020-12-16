using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebApiLibrary.Models;

namespace WebApiLibrary.DataContext
{
	public class DatabaseContext:DbContext
	{
		public DatabaseContext() : base("ServiceConnection"){ }
        public DbSet<ServiceBooks> book { get; set; }
        public DbSet<ServiceStudents> student { get; set; }
        public DbSet<ServiceIssuedBooks> issuedBook { get; set; }
        public DbSet<ServiceAuthor> author { get; set; }
        public DbSet<ServiceBookBranch> bookBranch { get; set; }
        public DbSet<ServiceBookPublication> bookPublication { get; set; }
        public DbSet<ServiceStudentBranch> studentBranches { get; set; }
    }
}
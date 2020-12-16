using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApiLibrary.DataContext;
using WebApiLibrary.Models;
using WebApiLibrary.ViewModels;

namespace WebApiLibrary.Controllers
{   
    public class ServiceIssuedBooksController : Controller
    {
        private DatabaseContext context = new DatabaseContext();
        List<ServiceBooks> book = new List<ServiceBooks>();
        List<ServiceStudents> student = new List<ServiceStudents>();
        List<ServiceIssuedBooks> issuedBooks = new List<ServiceIssuedBooks>();

        public ActionResult Index()
        {
            var issuedBookViewModel = (from i in context.issuedBook
                                       join b in context.book on i.bookId equals b.serviceBookId
                                       join s in context.student on i.studentId equals s.serviceStudentId
                                       select new ServiceIssuedBookViewModel
                                       {
                                           serviceBookId = b.serviceBookId,
                                           serviceBookName = b.serviceBookName,
                                           serviceAuthorName = b.serviceAuthorName,
                                           serviceBranch = b.serviceBranch,
                                           servicePublication = b.servicePublications,
                                           serviceStudentName = s.serviceStudentName,
                                           serviceStudentEmail = s.serviceEmail,
                                           serviceFromDate = i.serviceFromDate,
                                           serviceToDate = i.serviceToDate,
                                           serviceIssuedId = i.serviceIssuedId
                                           //book = b,
                                           //student = s,
                                           //issuedBook = i
                                       }).ToList();

            var result = issuedBookViewModel.Select(x => new ServiceIssuedBookViewModel
            {
                serviceBookId = x.serviceBookId,
                serviceBookName = x.serviceBookName,
                serviceAuthorName = x.serviceAuthorName,
                serviceBranch = x.serviceBranch,
                servicePublication = x.servicePublication,
                serviceStudentName = x.serviceStudentName,
                serviceStudentEmail = x.serviceStudentEmail,
                serviceDisplayToDate = x.serviceToDate.ToString("MM/dd/yyyy"),
                serviceDisplayFromDate = x.serviceFromDate.ToString("MM/dd/yyyy"),
                serviceIssuedId = x.serviceIssuedId
            }).ToList();
            return View(result);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var issuedBookViewModel = from i in context.issuedBook
                                      where i.serviceIssuedId == id
                                      join b in context.book on i.bookId equals b.serviceBookId
                                      join s in context.student on i.studentId equals s.serviceStudentId
                                      select new ServiceIssuedBookViewModel
                                      {
                                          serviceBookId = b.serviceBookId,
                                          serviceBookName = b.serviceBookName,
                                          serviceAuthorName = b.serviceAuthorName,
                                          serviceBranch = b.serviceBranch,
                                          servicePublication = b.servicePublications,
                                          serviceStudentName = s.serviceStudentName,
                                          serviceStudentEmail = s.serviceEmail,
                                          serviceFromDate = i.serviceFromDate,
                                          serviceToDate = i.serviceToDate,
                                          serviceIssuedId = i.serviceIssuedId                                          
                                      };
            return View(issuedBookViewModel.FirstOrDefault());
        }

        public ActionResult Create(int id)
        {
            var book = (from books in context.book
                        where books.serviceBookId == id
                        select books).FirstOrDefault();
            ViewBag.bookName = book.serviceBookName;
            ViewBag.authorName = book.serviceAuthorName;
            ViewBag.bookId = book.serviceBookId;
            return View(context.student.ToList());
        }

        [HttpPost]
        public JsonResult BookIssue(ServiceIssuedBooks issuedBooks)
        {
            var Book = context.book.FirstOrDefault(b => b.serviceBookId == issuedBooks.bookId);
            if (issuedBooks != null && Book != null)
            {
                context.issuedBook.Add(issuedBooks);
                context.SaveChanges();
                Book.serviceIsAvailable = false;

                context.Entry(Book).State = EntityState.Modified;
                context.SaveChanges();
                return Json(new { result = "success" });
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var issuedBookViewModel = (from i in context.issuedBook
                                       where i.serviceIssuedId == id
                                       join b in context.book on i.bookId equals b.serviceBookId
                                       join s in context.student on i.studentId equals s.serviceStudentId
                                       select new ServiceIssuedBookViewModel
                                       {
                                           serviceBookId = b.serviceBookId,
                                           serviceBookName = b.serviceBookName,
                                           serviceAuthorName = b.serviceAuthorName,
                                           serviceBranch = b.serviceBranch,
                                           servicePublication = b.servicePublications,
                                           serviceStudentName = s.serviceStudentName,
                                           serviceStudentEmail = s.serviceEmail,
                                           serviceFromDate = i.serviceFromDate,
                                           serviceToDate = i.serviceToDate,
                                           serviceIssuedId = i.serviceIssuedId
                                           //book = b,
                                           //student = s,
                                           //issuedBook = i
                                       }).ToList();
            var result = issuedBookViewModel.Select(x => new ServiceIssuedBookViewModel
            {
                serviceBookId = x.serviceBookId,
                serviceBookName = x.serviceBookName,
                serviceAuthorName = x.serviceAuthorName,
                serviceBranch = x.serviceBranch,
                servicePublication = x.servicePublication,
                serviceStudentName = x.serviceStudentName,
                serviceStudentEmail = x.serviceStudentEmail,
                serviceDisplayToDate = x.serviceToDate.ToString("MM/dd/yyyy"),
                serviceDisplayFromDate = x.serviceFromDate.ToString("MM/dd/yyyy"),
                serviceIssuedId = x.serviceIssuedId                
            }).ToList();
            // return View(result);
            return View(result.FirstOrDefault());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ServiceIssuedBookViewModel issuedBookViewModel)
        {
            if (ModelState.IsValid)
            {
                ServiceIssuedBooks issuedBooks = context.issuedBook.Find(issuedBookViewModel.serviceIssuedId);
                var fromDate = issuedBookViewModel.serviceDisplayFromDate;
                var toDate = issuedBookViewModel.serviceDisplayToDate;
                issuedBooks.serviceFromDate = Convert.ToDateTime(fromDate);
                issuedBooks.serviceToDate = Convert.ToDateTime(toDate);

                context.Entry(issuedBooks).State = EntityState.Modified;
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(issuedBooks);
        }

        [HttpPost]
        public JsonResult Return(int BookId, int IssuedBookId)
        {
            bool result = false;
            var book = context.book.FirstOrDefault(b => b.serviceBookId == BookId);
            var issuedBook = context.issuedBook.FirstOrDefault(i => i.serviceIssuedId == IssuedBookId);
            if (book != null && issuedBook != null)
            {
                book.serviceIsAvailable = true;
                context.issuedBook.Remove(issuedBook);
                context.SaveChanges();
                result = true;
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
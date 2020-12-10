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

//namespace WebApiLibrary.Controllers
//{
//    public class ServiceIssuedBooksController : Controller
//    {
//        private DatabaseContext context = new DatabaseContext();

//        // GET: ServiceIssuedBooks
//        public ActionResult Index()
//        {
//            var issuedBook = context.issuedBook.Include(s => s.serviceBooks).Include(s => s.serviceStudents);
//            return View(issuedBook.ToList());
//        }

//        // GET: ServiceIssuedBooks/Details/5
//        public ActionResult Details(int? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            ServiceIssuedBooks serviceIssuedBooks = context.issuedBook.Find(id);
//            if (serviceIssuedBooks == null)
//            {
//                return HttpNotFound();
//            }
//            return View(serviceIssuedBooks);
//        }

//        // GET: ServiceIssuedBooks/Create
//        public ActionResult Create()
//        {
//            ViewBag.bookId = new SelectList(context.book, "serviceBookId", "serviceBookName");
//            ViewBag.studentId = new SelectList(context.student, "serviceStudentId", "serviceStudentName");
//            return View();
//        }

//        // POST: ServiceIssuedBooks/Create
//        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
//        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult Create([Bind(Include = "serviceIssuedId,bookId,studentId,serviceFromDate,serviceToDate")] ServiceIssuedBooks serviceIssuedBooks)
//        {
//            if (ModelState.IsValid)
//            {
//                context.issuedBook.Add(serviceIssuedBooks);
//                context.SaveChanges();
//                return RedirectToAction("Index");
//            }

//            ViewBag.bookId = new SelectList(context.book, "serviceBookId", "serviceBookName", serviceIssuedBooks.bookId);
//            ViewBag.studentId = new SelectList(context.student, "serviceStudentId", "serviceStudentName", serviceIssuedBooks.studentId);
//            return View(serviceIssuedBooks);
//        }
//        [HttpPost]
//        public JsonResult BookIssue(ServiceIssuedBooks issuedBooks)
//        {
//            var Book = context.book.FirstOrDefault(b => b.bookId == issuedBooks.bookId);
//            if (issuedBooks != null && Book != null)
//            {
//                context.issuedBook.Add(issuedBooks);
//                context.SaveChanges();
//                Book.isAvailable = false;

//                context.Entry(Book).State = EntityState.Modified;
//                context.SaveChanges();
//                return Json(new { result = "success" });
//            }
//            else
//            {
//                return Json(false, JsonRequestBehavior.AllowGet);
//            }
//        }


//        // GET: ServiceIssuedBooks/Edit/5
//        public ActionResult Edit(int? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            ServiceIssuedBooks serviceIssuedBooks = context.issuedBook.Find(id);
//            if (serviceIssuedBooks == null)
//            {
//                return HttpNotFound();
//            }
//            ViewBag.bookId = new SelectList(context.book, "serviceBookId", "serviceBookName", serviceIssuedBooks.bookId);
//            ViewBag.studentId = new SelectList(context.student, "serviceStudentId", "serviceStudentName", serviceIssuedBooks.studentId);
//            return View(serviceIssuedBooks);
//        }

//        // POST: ServiceIssuedBooks/Edit/5
//        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
//        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult Edit([Bind(Include = "serviceIssuedId,bookId,studentId,serviceFromDate,serviceToDate")] ServiceIssuedBooks serviceIssuedBooks)
//        {
//            if (ModelState.IsValid)
//            {
//                context.Entry(serviceIssuedBooks).State = EntityState.Modified;
//                context.SaveChanges();
//                return RedirectToAction("Index");
//            }
//            ViewBag.bookId = new SelectList(context.book, "serviceBookId", "serviceBookName", serviceIssuedBooks.bookId);
//            ViewBag.studentId = new SelectList(context.student, "serviceStudentId", "serviceStudentName", serviceIssuedBooks.studentId);
//            return View(serviceIssuedBooks);
//        }

//        //// GET: ServiceIssuedBooks/Delete/5
//        //public ActionResult Delete(int? id)
//        //{
//        //    if (id == null)
//        //    {
//        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//        //    }
//        //    ServiceIssuedBooks serviceIssuedBooks = context.issuedBook.Find(id);
//        //    if (serviceIssuedBooks == null)
//        //    {
//        //        return HttpNotFound();
//        //    }
//        //    return View(serviceIssuedBooks);
//        //}

//        //// POST: ServiceIssuedBooks/Delete/5
//        //[HttpPost, ActionName("Delete")]
//        //[ValidateAntiForgeryToken]
//        //public ActionResult DeleteConfirmed(int id)
//        //{
//        //    ServiceIssuedBooks serviceIssuedBooks = context.issuedBook.Find(id);
//        //    context.issuedBook.Remove(serviceIssuedBooks);
//        //    context.SaveChanges();
//        //    return RedirectToAction("Index");
//        //}
//        [HttpPost]
//        public JsonResult Return(int BookId, int IssuedBookId)
//        {
//            bool result = false;
//            var book = context.book.FirstOrDefault(b => b.bookId == BookId);
//            var issuedBook = context.issuedBook.FirstOrDefault(i => i.issuedId == IssuedBookId);
//            if (book != null && issuedBook != null)
//            {
//                book.isAvailable = true;
//                context.issuedBook.Remove(issuedBook);
//                context.SaveChanges();
//                result = true;
//            }
//            return Json(result, JsonRequestBehavior.AllowGet);
//        }
//        protected override void Dispose(bool disposing)
//        {
//            if (disposing)
//            {
//                context.Dispose();
//            }
//            base.Dispose(disposing);
//        }
//    }
//}
namespace WebApiLibrary.Controllers
{
    //[Authorize(Roles = "Admin")]
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
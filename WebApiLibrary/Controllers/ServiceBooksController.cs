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
    public class ServiceBooksController : Controller
    {
        private DatabaseContext context = new DatabaseContext();

        public ActionResult Index()
        {
            return View(context.book.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServiceBooks books = context.book.Find(id);
            if (books == null)
            {
                return HttpNotFound();
            }
            return View(books);
        }

        public ActionResult Create()
        {
            ServiceBooksViewModel booksViewModel = new ServiceBooksViewModel();

            booksViewModel.authors = context.author.ToList();
            booksViewModel.bookBranches = context.bookBranch.ToList();
            booksViewModel.bookPublications = context.bookPublication.ToList();
            return View(booksViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ServiceBooksViewModel books)
        {
            if (ModelState.IsValid)
            {
                context.book.Add(books.book);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(books);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServiceBooks books = context.book.Find(id);
            if (books == null)
            {
                return HttpNotFound();
            }
            return View(books);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "serviceBookId,serviceBookName,serviceSerialNumber,serviceAuthorName,serviceBranch,servicePublications,serviceIsAvailable")] ServiceBooks books)
        {
            if (ModelState.IsValid)
            {
                context.Entry(books).State = EntityState.Modified;
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(books);
        }

        [HttpPost]
        public JsonResult Delete(int id)
        {
            bool result = false;

            var Book = context.book.FirstOrDefault(b => b.serviceBookId == id);
            Book.serviceIsAvailable = false;
            if (Book != null)
            {
                context.book.Remove(Book);
                context.SaveChanges();
                result = true;
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AddAuthor(string name)
        {
            var author = new ServiceAuthor();
            author.serviceAuthorName = name;
            context.author.Add(author);
            context.SaveChanges();
            return Json(new { result = "success" });
        }

        [HttpPost]
        public JsonResult AddBranch(string name)
        {
            var bookBranch = new ServiceBookBranch();
            bookBranch.serviceBranch = name;
            context.bookBranch.Add(bookBranch);
            context.SaveChanges();
            return Json(new { result = "success" });
        }

        [HttpPost]
        public JsonResult AddPublication(string name)
        {
            var publication = new ServiceBookPublication();
            publication.servicePublications = name;
            context.bookPublication.Add(publication);
            context.SaveChanges();
            return Json(new { result = "success" });
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
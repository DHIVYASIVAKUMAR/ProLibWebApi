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

namespace WebApiLibrary.Controllers
{
    public class ServiceIssuedBooksController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        // GET: ServiceIssuedBooks
        public ActionResult Index()
        {
            var issuedBook = db.issuedBook.Include(s => s.serviceBooks).Include(s => s.serviceStudents);
            return View(issuedBook.ToList());
        }

        // GET: ServiceIssuedBooks/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServiceIssuedBooks serviceIssuedBooks = db.issuedBook.Find(id);
            if (serviceIssuedBooks == null)
            {
                return HttpNotFound();
            }
            return View(serviceIssuedBooks);
        }

        // GET: ServiceIssuedBooks/Create
        public ActionResult Create()
        {
            ViewBag.bookId = new SelectList(db.book, "serviceBookId", "serviceBookName");
            ViewBag.studentId = new SelectList(db.student, "serviceStudentId", "serviceStudentName");
            return View();
        }

        // POST: ServiceIssuedBooks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "serviceIssuedId,bookId,studentId,serviceFromDate,serviceToDate")] ServiceIssuedBooks serviceIssuedBooks)
        {
            if (ModelState.IsValid)
            {
                db.issuedBook.Add(serviceIssuedBooks);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.bookId = new SelectList(db.book, "serviceBookId", "serviceBookName", serviceIssuedBooks.bookId);
            ViewBag.studentId = new SelectList(db.student, "serviceStudentId", "serviceStudentName", serviceIssuedBooks.studentId);
            return View(serviceIssuedBooks);
        }

        // GET: ServiceIssuedBooks/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServiceIssuedBooks serviceIssuedBooks = db.issuedBook.Find(id);
            if (serviceIssuedBooks == null)
            {
                return HttpNotFound();
            }
            ViewBag.bookId = new SelectList(db.book, "serviceBookId", "serviceBookName", serviceIssuedBooks.bookId);
            ViewBag.studentId = new SelectList(db.student, "serviceStudentId", "serviceStudentName", serviceIssuedBooks.studentId);
            return View(serviceIssuedBooks);
        }

        // POST: ServiceIssuedBooks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "serviceIssuedId,bookId,studentId,serviceFromDate,serviceToDate")] ServiceIssuedBooks serviceIssuedBooks)
        {
            if (ModelState.IsValid)
            {
                db.Entry(serviceIssuedBooks).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.bookId = new SelectList(db.book, "serviceBookId", "serviceBookName", serviceIssuedBooks.bookId);
            ViewBag.studentId = new SelectList(db.student, "serviceStudentId", "serviceStudentName", serviceIssuedBooks.studentId);
            return View(serviceIssuedBooks);
        }

        // GET: ServiceIssuedBooks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServiceIssuedBooks serviceIssuedBooks = db.issuedBook.Find(id);
            if (serviceIssuedBooks == null)
            {
                return HttpNotFound();
            }
            return View(serviceIssuedBooks);
        }

        // POST: ServiceIssuedBooks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ServiceIssuedBooks serviceIssuedBooks = db.issuedBook.Find(id);
            db.issuedBook.Remove(serviceIssuedBooks);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

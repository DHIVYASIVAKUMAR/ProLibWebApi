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
    public class ServiceBooksController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        // GET: ServiceBooks
        public ActionResult Index()
        {
            return View(db.book.ToList());
        }

        // GET: ServiceBooks/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServiceBooks serviceBooks = db.book.Find(id);
            if (serviceBooks == null)
            {
                return HttpNotFound();
            }
            return View(serviceBooks);
        }

        // GET: ServiceBooks/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ServiceBooks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "serviceBookId,serviceBookName,serviceSerialNumber,serviceAuthorName,serviceBranch,servicePublications,serviceIsAvailable")] ServiceBooks serviceBooks)
        {
            if (ModelState.IsValid)
            {
                db.book.Add(serviceBooks);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(serviceBooks);
        }

        // GET: ServiceBooks/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServiceBooks serviceBooks = db.book.Find(id);
            if (serviceBooks == null)
            {
                return HttpNotFound();
            }
            return View(serviceBooks);
        }

        // POST: ServiceBooks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "serviceBookId,serviceBookName,serviceSerialNumber,serviceAuthorName,serviceBranch,servicePublications,serviceIsAvailable")] ServiceBooks serviceBooks)
        {
            if (ModelState.IsValid)
            {
                db.Entry(serviceBooks).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(serviceBooks);
        }

        // GET: ServiceBooks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServiceBooks serviceBooks = db.book.Find(id);
            if (serviceBooks == null)
            {
                return HttpNotFound();
            }
            return View(serviceBooks);
        }

        // POST: ServiceBooks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ServiceBooks serviceBooks = db.book.Find(id);
            db.book.Remove(serviceBooks);
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

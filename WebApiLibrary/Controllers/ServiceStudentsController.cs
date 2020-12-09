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
    public class ServiceStudentsController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        // GET: ServiceStudents
        public ActionResult Index()
        {
            return View(db.student.ToList());
        }

        // GET: ServiceStudents/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServiceStudents serviceStudents = db.student.Find(id);
            if (serviceStudents == null)
            {
                return HttpNotFound();
            }
            return View(serviceStudents);
        }

        // GET: ServiceStudents/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ServiceStudents/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "serviceStudentId,serviceStudentName,serviceStudentBranch,serviceGender,servicePhoneNumber,serviceAddress,serviceCity,serviceEmail,servicePassword")] ServiceStudents serviceStudents)
        {
            if (ModelState.IsValid)
            {
                db.student.Add(serviceStudents);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(serviceStudents);
        }

        // GET: ServiceStudents/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServiceStudents serviceStudents = db.student.Find(id);
            if (serviceStudents == null)
            {
                return HttpNotFound();
            }
            return View(serviceStudents);
        }

        // POST: ServiceStudents/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "serviceStudentId,serviceStudentName,serviceStudentBranch,serviceGender,servicePhoneNumber,serviceAddress,serviceCity,serviceEmail,servicePassword")] ServiceStudents serviceStudents)
        {
            if (ModelState.IsValid)
            {
                db.Entry(serviceStudents).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(serviceStudents);
        }

        // GET: ServiceStudents/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServiceStudents serviceStudents = db.student.Find(id);
            if (serviceStudents == null)
            {
                return HttpNotFound();
            }
            return View(serviceStudents);
        }

        // POST: ServiceStudents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ServiceStudents serviceStudents = db.student.Find(id);
            db.student.Remove(serviceStudents);
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

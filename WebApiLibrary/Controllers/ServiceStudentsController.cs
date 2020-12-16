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
    public class ServiceStudentsController : Controller
    {
        private DatabaseContext context = new DatabaseContext();
        public ActionResult Index()
        {
            return View(context.student.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServiceStudents students = context.student.Find(id);
            if (students == null)
            {
                return HttpNotFound();
            }
            return View(students);
        }

        public ActionResult Create()
        {
            ServiceStudentViewModel studentViewModel = new ServiceStudentViewModel();
            studentViewModel.studentBranches = context.studentBranches.ToList();
            return View(studentViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ServiceStudentViewModel studentObj)
        {
            if (ModelState.IsValid)
            {
                context.student.Add(studentObj.students);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(studentObj);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServiceStudents students = context.student.Find(id);
            if (students == null)
            {
                return HttpNotFound();
            }
            return View(students);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "serviceStudentId,serviceStudentName,serviceStudentBranch,serviceGender,servicePhoneNumber,serviceAddress,serviceCity,serviceEmail,servicePassword")] ServiceStudents students)
		{
            if (ModelState.IsValid)
            {
                context.Entry(students).State = EntityState.Modified;
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(students);
        }

        [HttpPost]
        public JsonResult Delete(int id)
        {
            bool result = false;
            var student = context.student.FirstOrDefault(s => s.serviceStudentId == id);
            if (student != null)
            {
                context.student.Remove(student);
                context.SaveChanges();
                result = true;
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AddBranch(string name)
        {
            var branchs = new ServiceStudentBranch();
            branchs.serviceStudentBranch = name;
            context.studentBranches.Add(branchs);
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
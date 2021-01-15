using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BITCollege_GiangHuynh.Models;

namespace BITCollege_GiangHuynh.Controllers
{
    public class RegistrationsController : Controller
    {
        private BITCollege_GiangHuynhContext db = new BITCollege_GiangHuynhContext();

        // GET: Registrations
        public ActionResult Index()
        {
            var registrations = db.Registrations.Include(r => r.Course).Include(r => r.Student);
            return View(registrations.ToList());
        }

        // GET: Registrations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Registration registration = db.Registrations.Find(id);
            if (registration == null)
            {
                return HttpNotFound();
            }
            return View(registration);
        }

        // GET: Registrations/Create
        public ActionResult Create()
        {
            // ViewBag.CourseId = new SelectList(db.Courses, "CourseId", "CourseNumber");
            ViewBag.CourseId = new SelectList(db.Courses, "CourseId", "Title");
            //ViewBag.StudentId = new SelectList(db.Students, "StudentId", "FirstName");
            ViewBag.StudentId = new SelectList(db.Students, "StudentId", "FullName");
            return View();
        }

        // POST: Registrations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RegistrationId,RegistrationNumber,StudentId,CourseId,RegistrationDate,Grade,Notes")] Registration registration)
        {
            if (ModelState.IsValid)
            {
                //Giang the appropriate setNext method using the argument supplied
                registration.setNextRegistrationNumber();
                NextRegistrationNumber.getInstance().NextAvailableNumber = registration.RegistrationNumber;
                db.Registrations.Add(registration);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            // ViewBag.CourseId = new SelectList(db.Courses, "CourseId", "CourseNumber", registration.CourseId);
            ViewBag.CourseId = new SelectList(db.Courses, "CourseId", "Title", registration.CourseId);
            // ViewBag.StudentId = new SelectList(db.Students, "StudentId", "FirstName", registration.StudentId);
            ViewBag.StudentId = new SelectList(db.Students, "StudentId", "FullName", registration.StudentId);
            return View(registration);
        }

        // GET: Registrations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Registration registration = db.Registrations.Find(id);
            if (registration == null)
            {
                return HttpNotFound();
            }
            // ViewBag.CourseId = new SelectList(db.Courses, "CourseId", "CourseNumber", registration.CourseId);
            ViewBag.CourseId = new SelectList(db.Courses, "CourseId", "Title", registration.CourseId);
            ViewBag.StudentId = new SelectList(db.Students, "StudentId", "FullName", registration.StudentId);
            return View(registration);
        }

        // POST: Registrations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RegistrationId,RegistrationNumber,StudentId,CourseId,RegistrationDate,Grade,Notes")] Registration registration)
        {
            if (ModelState.IsValid)
            {
                db.Entry(registration).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            // ViewBag.CourseId = new SelectList(db.Courses, "CourseId", "CourseNumber", registration.CourseId);
            ViewBag.CourseId = new SelectList(db.Courses, "CourseId", "Title", registration.CourseId);
            // ViewBag.StudentId = new SelectList(db.Students, "StudentId", "FirstName", registration.StudentId);
            ViewBag.StudentId = new SelectList(db.Students, "StudentId", "FullName", registration.StudentId);
            return View(registration);
        }

        // GET: Registrations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Registration registration = db.Registrations.Find(id);
            if (registration == null)
            {
                return HttpNotFound();
            }
            return View(registration);
        }

        // POST: Registrations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Registration registration = db.Registrations.Find(id);
            db.Registrations.Remove(registration);
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

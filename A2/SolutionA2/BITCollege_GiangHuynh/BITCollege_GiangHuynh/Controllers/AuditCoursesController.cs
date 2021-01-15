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
    public class AuditCoursesController : Controller
    {
        private BITCollege_GiangHuynhContext db = new BITCollege_GiangHuynhContext();

        // GET: AuditCourses
        // {Giang}Change Courses to AuditCourses
        // From var courses = db.Courses.Include(g => g.Program);
        public ActionResult Index()
        {
            var courses = db.AuditCourses.Include(a => a.Program);
            return View(courses.ToList());
        }

        // GET: AuditCourses/Details/5
        // {Giang} Cast to AuditCourse from Course
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AuditCourse auditCourse = (AuditCourse)db.Courses.Find(id);
            if (auditCourse == null)
            {
                return HttpNotFound();
            }
            return View(auditCourse);
        }

        // GET: AuditCourses/Create
        public ActionResult Create()
        {
            // ViewBag.ProgramId = new SelectList(db.Programs, "ProgramId", "ProgramAcronym");
            ViewBag.ProgramId = new SelectList(db.Programs, "ProgramId", "Description");
            return View();
        }

        // POST: AuditCourses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CourseId,ProgramId,CourseNumber,Title,CreditHours,TuitionAmount,Notes")] AuditCourse auditCourse)
        {
            if (ModelState.IsValid)
            {
                //Giang the appropriate setNext method using the argument supplied
               // NextAuditCourse.getInstance().NextAvailableNumber = NextAuditCourse.getInstance().NextAvailableNumber + 1;
                auditCourse.setNextCourseNumber();
                db.Courses.Add(auditCourse);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            // ViewBag.ProgramId = new SelectList(db.Programs, "ProgramId", "ProgramAcronym", auditCourse.ProgramId);
            ViewBag.ProgramId = new SelectList(db.Programs, "ProgramId", "Description", auditCourse.ProgramId);
            return View(auditCourse);
        }

        // GET: AuditCourses/Edit/5
        // {Giang} Cast to AuditCourse from Course
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AuditCourse auditCourse = (AuditCourse)db.Courses.Find(id);
            if (auditCourse == null)
            {
                return HttpNotFound();
            }
            // ViewBag.ProgramId = new SelectList(db.Programs, "ProgramId", "ProgramAcronym", auditCourse.ProgramId);
            ViewBag.ProgramId = new SelectList(db.Programs, "ProgramId", "Description", auditCourse.ProgramId);
            return View(auditCourse);
        }

        // POST: AuditCourses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CourseId,ProgramId,CourseNumber,Title,CreditHours,TuitionAmount,Notes")] AuditCourse auditCourse)
        {
            if (ModelState.IsValid)
            {
                db.Entry(auditCourse).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            // ViewBag.ProgramId = new SelectList(db.Programs, "ProgramId", "ProgramAcronym", auditCourse.ProgramId);
            ViewBag.ProgramId = new SelectList(db.Programs, "ProgramId", "Description", auditCourse.ProgramId);
            return View(auditCourse);
        }

        // GET: AuditCourses/Delete/5
        // {Giang} Cast to AuditCourse from Course
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AuditCourse auditCourse = (AuditCourse)db.Courses.Find(id);
            if (auditCourse == null)
            {
                return HttpNotFound();
            }
            return View(auditCourse);
        }

        // POST: AuditCourses/Delete/5
        // {Giang} Cast to AuditCourse from Course
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AuditCourse auditCourse = (AuditCourse)db.Courses.Find(id);
            db.Courses.Remove(auditCourse);
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

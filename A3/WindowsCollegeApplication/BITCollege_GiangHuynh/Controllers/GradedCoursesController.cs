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
    public class GradedCoursesController : Controller
    {
        private BITCollege_GiangHuynhContext db = new BITCollege_GiangHuynhContext();

        // GET: GradedCourses
        // {Giang}Change Courses to GradedCourses
        // From var courses = db.Courses.Include(g => g.Program);
        public ActionResult Index()
        {
            var courses = db.GradedCourses.Include(g => g.Program);
            return View(courses.ToList());
        }

        // GET: GradedCourses/Details/5
        // {Giang} Cast to GradedCourse from Course
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GradedCourse gradedCourse = (GradedCourse)db.Courses.Find(id);
            if (gradedCourse == null)
            {
                return HttpNotFound();
            }
            return View(gradedCourse);
        }

        // GET: GradedCourses/Create
        public ActionResult Create()
        {
            // ViewBag.ProgramId = new SelectList(db.Programs, "ProgramId", "ProgramAcronym");
            ViewBag.ProgramId = new SelectList(db.Programs, "ProgramId", "Description");
            return View();
        }

        // POST: GradedCourses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CourseId,ProgramId,CourseNumber,Title,CreditHours,TuitionAmount,Notes,AssignmentWeight,MidtermWeight,FinalWeight")] GradedCourse gradedCourse)
        {
            if (ModelState.IsValid)
            {
                //Giang the appropriate setNext method using the argument supplied
                NextGradedCourse.getInstance().NextAvailableNumber = NextGradedCourse.getInstance().NextAvailableNumber + 1;
                gradedCourse.setNextCourseNumber();
                db.Courses.Add(gradedCourse);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            // ViewBag.ProgramId = new SelectList(db.Programs, "ProgramId", "ProgramAcronym", gradedCourse.ProgramId);
            ViewBag.ProgramId = new SelectList(db.Programs, "ProgramId", "Description", gradedCourse.ProgramId);
            return View(gradedCourse);
        }

        // GET: GradedCourses/Edit/5
        // {Giang} Cast to GradedCourse from Course
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GradedCourse gradedCourse = (GradedCourse)db.Courses.Find(id);
            if (gradedCourse == null)
            {
                return HttpNotFound();
            }
            // ViewBag.ProgramId = new SelectList(db.Programs, "ProgramId", "ProgramAcronym", gradedCourse.ProgramId);
            ViewBag.ProgramId = new SelectList(db.Programs, "ProgramId", "Description", gradedCourse.ProgramId);
            return View(gradedCourse);
        }

        // POST: GradedCourses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CourseId,ProgramId,CourseNumber,Title,CreditHours,TuitionAmount,Notes,AssignmentWeight,MidtermWeight,FinalWeight")] GradedCourse gradedCourse)
        {
            if (ModelState.IsValid)
            {
                db.Entry(gradedCourse).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            // ViewBag.ProgramId = new SelectList(db.Programs, "ProgramId", "ProgramAcronym", gradedCourse.ProgramId);
            ViewBag.ProgramId = new SelectList(db.Programs, "ProgramId", "Description", gradedCourse.ProgramId);
            return View(gradedCourse);
        }

        // GET: GradedCourses/Delete/5
        // {Giang} Cast to GradedCourse from Course
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GradedCourse gradedCourse = (GradedCourse)db.Courses.Find(id);
            if (gradedCourse == null)
            {
                return HttpNotFound();
            }
            return View(gradedCourse);
        }

        // POST: GradedCourses/Delete/5
        // {Giang} Cast to GradedCourse from Course
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            GradedCourse gradedCourse = (GradedCourse)db.Courses.Find(id);
            db.Courses.Remove(gradedCourse);
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

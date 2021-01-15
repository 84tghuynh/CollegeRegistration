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
    public class MasteryCoursesController : Controller
    {
        private BITCollege_GiangHuynhContext db = new BITCollege_GiangHuynhContext();

        // GET: MasteryCourses
        // {Giang}Change Courses to MasteryCourses
        // From var courses = db.Courses.Include(g => g.Program);
        public ActionResult Index()
        {
            var courses = db.MasteryCourses.Include(m => m.Program);
            return View(courses.ToList());
        }

        // GET: MasteryCourses/Details/5
        // {Giang} Cast to MasteryCourse from Course
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MasteryCourse masteryCourse = (MasteryCourse)db.Courses.Find(id);
            if (masteryCourse == null)
            {
                return HttpNotFound();
            }
            return View(masteryCourse);
        }

        // GET: MasteryCourses/Create
        public ActionResult Create()
        {
            // ViewBag.ProgramId = new SelectList(db.Programs, "ProgramId", "ProgramAcronym");
            ViewBag.ProgramId = new SelectList(db.Programs, "ProgramId", "Description");
            return View();
        }

        // POST: MasteryCourses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CourseId,ProgramId,CourseNumber,Title,CreditHours,TuitionAmount,Notes,MaximumAttempts")] MasteryCourse masteryCourse)
        {
            if (ModelState.IsValid)
            {
                //Giang the appropriate setNext method using the argument supplied
                NextMasteryCourse.getInstance().NextAvailableNumber = NextMasteryCourse.getInstance().NextAvailableNumber + 1;
                masteryCourse.setNextCourseNumber();
                db.Courses.Add(masteryCourse);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            // ViewBag.ProgramId = new SelectList(db.Programs, "ProgramId", "ProgramAcronym", masteryCourse.ProgramId);
            ViewBag.ProgramId = new SelectList(db.Programs, "ProgramId", "Description", masteryCourse.ProgramId);
            return View(masteryCourse);
        }

        // GET: MasteryCourses/Edit/5
        // {Giang} Cast to MasteryCourse from Course
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MasteryCourse masteryCourse = (MasteryCourse)db.Courses.Find(id);
            if (masteryCourse == null)
            {
                return HttpNotFound();
            }
            // ViewBag.ProgramId = new SelectList(db.Programs, "ProgramId", "ProgramAcronym", masteryCourse.ProgramId);
            ViewBag.ProgramId = new SelectList(db.Programs, "ProgramId", "Description", masteryCourse.ProgramId);
            return View(masteryCourse);
        }

        // POST: MasteryCourses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CourseId,ProgramId,CourseNumber,Title,CreditHours,TuitionAmount,Notes,MaximumAttempts")] MasteryCourse masteryCourse)
        {
            if (ModelState.IsValid)
            {
                db.Entry(masteryCourse).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            //ViewBag.ProgramId = new SelectList(db.Programs, "ProgramId", "ProgramAcronym", masteryCourse.ProgramId);
            ViewBag.ProgramId = new SelectList(db.Programs, "ProgramId", "Description", masteryCourse.ProgramId);
            return View(masteryCourse);
        }

        // GET: MasteryCourses/Delete/5
        // {Giang} Cast to MasteryCourse from Course
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MasteryCourse masteryCourse = (MasteryCourse)db.Courses.Find(id);
            if (masteryCourse == null)
            {
                return HttpNotFound();
            }
            return View(masteryCourse);
        }

        // POST: MasteryCourses/Delete/5
        // {Giang} Cast to MasteryCourse from Course
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MasteryCourse masteryCourse = (MasteryCourse)db.Courses.Find(id);
            db.Courses.Remove(masteryCourse);
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

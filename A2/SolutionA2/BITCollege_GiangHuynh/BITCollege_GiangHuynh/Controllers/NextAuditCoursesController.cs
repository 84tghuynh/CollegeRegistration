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
    public class NextAuditCoursesController : Controller
    {
        private BITCollege_GiangHuynhContext db = new BITCollege_GiangHuynhContext();

        // GET: NextAuditCourses
        public ActionResult Index()
        {
           // return View(db.NextAuditCourses.SingleOrDefault());
            return View(NextAuditCourse.getInstance());
        }

        // GET: NextAuditCourses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NextAuditCourse nextAuditCourse = db.NextAuditCourses.Find(id);
            if (nextAuditCourse == null)
            {
                return HttpNotFound();
            }
            return View(nextAuditCourse);
        }

        // GET: NextAuditCourses/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: NextAuditCourses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "NextAuditCourseId,NextAvailableNumber")] NextAuditCourse nextAuditCourse)
        {
            if (ModelState.IsValid)
            {
                db.NextAuditCourses.Add(nextAuditCourse);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(nextAuditCourse);
        }

        // GET: NextAuditCourses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NextAuditCourse nextAuditCourse = db.NextAuditCourses.Find(id);
            if (nextAuditCourse == null)
            {
                return HttpNotFound();
            }
            return View(nextAuditCourse);
        }

        // POST: NextAuditCourses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "NextAuditCourseId,NextAvailableNumber")] NextAuditCourse nextAuditCourse)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nextAuditCourse).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(nextAuditCourse);
        }

        // GET: NextAuditCourses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NextAuditCourse nextAuditCourse = db.NextAuditCourses.Find(id);
            if (nextAuditCourse == null)
            {
                return HttpNotFound();
            }
            return View(nextAuditCourse);
        }

        // POST: NextAuditCourses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            NextAuditCourse nextAuditCourse = db.NextAuditCourses.Find(id);
            db.NextAuditCourses.Remove(nextAuditCourse);
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

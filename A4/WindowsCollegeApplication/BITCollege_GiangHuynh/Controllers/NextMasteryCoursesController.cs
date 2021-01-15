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
    public class NextMasteryCoursesController : Controller
    {
        private BITCollege_GiangHuynhContext db = new BITCollege_GiangHuynhContext();

        // GET: NextMasteryCourses
        public ActionResult Index()
        {
            //return View(db.NextMasteryCourses.SingleOrDefault());
            return View(NextMasteryCourse.getInstance());
        }

        // GET: NextMasteryCourses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NextMasteryCourse nextMasteryCourse = db.NextMasteryCourses.Find(id);
            if (nextMasteryCourse == null)
            {
                return HttpNotFound();
            }
            return View(nextMasteryCourse);
        }

        // GET: NextMasteryCourses/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: NextMasteryCourses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "NextMasteryCourseId,NextAvailableNumber")] NextMasteryCourse nextMasteryCourse)
        {
            if (ModelState.IsValid)
            {
                db.NextMasteryCourses.Add(nextMasteryCourse);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(nextMasteryCourse);
        }

        // GET: NextMasteryCourses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NextMasteryCourse nextMasteryCourse = db.NextMasteryCourses.Find(id);
            if (nextMasteryCourse == null)
            {
                return HttpNotFound();
            }
            return View(nextMasteryCourse);
        }

        // POST: NextMasteryCourses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "NextMasteryCourseId,NextAvailableNumber")] NextMasteryCourse nextMasteryCourse)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nextMasteryCourse).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(nextMasteryCourse);
        }

        // GET: NextMasteryCourses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NextMasteryCourse nextMasteryCourse = db.NextMasteryCourses.Find(id);
            if (nextMasteryCourse == null)
            {
                return HttpNotFound();
            }
            return View(nextMasteryCourse);
        }

        // POST: NextMasteryCourses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            NextMasteryCourse nextMasteryCourse = db.NextMasteryCourses.Find(id);
            db.NextMasteryCourses.Remove(nextMasteryCourse);
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

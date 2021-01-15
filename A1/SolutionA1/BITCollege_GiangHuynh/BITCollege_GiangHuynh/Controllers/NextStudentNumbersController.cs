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
    public class NextStudentNumbersController : Controller
    {
        private BITCollege_GiangHuynhContext db = new BITCollege_GiangHuynhContext();

        // GET: NextStudentNumbers
        public ActionResult Index()
        {
            //return View(db.NextStudentNumbers.ToList());
            return View(NextStudentNumber.getInstance());
            //return View(db.NextStudentNumbers.SingleOrDefault());
        }

        // GET: NextStudentNumbers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NextStudentNumber nextStudentNumber = db.NextStudentNumbers.Find(id);
            if (nextStudentNumber == null)
            {
                return HttpNotFound();
            }
            return View(nextStudentNumber);
        }

        // GET: NextStudentNumbers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: NextStudentNumbers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "NextStudentNumberId,NextAvailableNumber")] NextStudentNumber nextStudentNumber)
        {
            if (ModelState.IsValid)
            {
                db.NextStudentNumbers.Add(nextStudentNumber);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(nextStudentNumber);
        }

        // GET: NextStudentNumbers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NextStudentNumber nextStudentNumber = db.NextStudentNumbers.Find(id);
            if (nextStudentNumber == null)
            {
                return HttpNotFound();
            }
            return View(nextStudentNumber);
        }

        // POST: NextStudentNumbers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "NextStudentNumberId,NextAvailableNumber")] NextStudentNumber nextStudentNumber)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nextStudentNumber).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(nextStudentNumber);
        }

        // GET: NextStudentNumbers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NextStudentNumber nextStudentNumber = db.NextStudentNumbers.Find(id);
            if (nextStudentNumber == null)
            {
                return HttpNotFound();
            }
            return View(nextStudentNumber);
        }

        // POST: NextStudentNumbers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            NextStudentNumber nextStudentNumber = db.NextStudentNumbers.Find(id);
            db.NextStudentNumbers.Remove(nextStudentNumber);
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

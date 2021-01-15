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
    public class NextRegistrationNumbersController : Controller
    {
        private BITCollege_GiangHuynhContext db = new BITCollege_GiangHuynhContext();

        // GET: NextRegistrationNumbers
        public ActionResult Index()
        {
            return View(NextRegistrationNumber.getInstance());
        }

        // GET: NextRegistrationNumbers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NextRegistrationNumber nextRegistrationNumber = db.NextRegistrationNumbers.Find(id);
            if (nextRegistrationNumber == null)
            {
                return HttpNotFound();
            }
            return View(nextRegistrationNumber);
        }

        // GET: NextRegistrationNumbers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: NextRegistrationNumbers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "NextRegistrationNumberId,NextAvailableNumber")] NextRegistrationNumber nextRegistrationNumber)
        {
            if (ModelState.IsValid)
            {
                db.NextRegistrationNumbers.Add(nextRegistrationNumber);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(nextRegistrationNumber);
        }

        // GET: NextRegistrationNumbers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NextRegistrationNumber nextRegistrationNumber = db.NextRegistrationNumbers.Find(id);
            if (nextRegistrationNumber == null)
            {
                return HttpNotFound();
            }
            return View(nextRegistrationNumber);
        }

        // POST: NextRegistrationNumbers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "NextRegistrationNumberId,NextAvailableNumber")] NextRegistrationNumber nextRegistrationNumber)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nextRegistrationNumber).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(nextRegistrationNumber);
        }

        // GET: NextRegistrationNumbers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NextRegistrationNumber nextRegistrationNumber = db.NextRegistrationNumbers.Find(id);
            if (nextRegistrationNumber == null)
            {
                return HttpNotFound();
            }
            return View(nextRegistrationNumber);
        }

        // POST: NextRegistrationNumbers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            NextRegistrationNumber nextRegistrationNumber = db.NextRegistrationNumbers.Find(id);
            db.NextRegistrationNumbers.Remove(nextRegistrationNumber);
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

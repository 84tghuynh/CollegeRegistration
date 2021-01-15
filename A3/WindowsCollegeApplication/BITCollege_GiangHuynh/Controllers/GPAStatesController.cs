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
    public class GPAStatesController : Controller
    {
        private BITCollege_GiangHuynhContext db = new BITCollege_GiangHuynhContext();

        // GET: GPAStates
        public ActionResult Index()
        {
            return View(db.GPAStates.ToList());
        }

        // GET: GPAStates/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GPAState gPAState = db.GPAStates.Find(id);
            if (gPAState == null)
            {
                return HttpNotFound();
            }
            return View(gPAState);
        }

        // GET: GPAStates/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: GPAStates/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "GPAStateId,LowerLimit,UpperLimit,TuitionRateFactor")] GPAState gPAState)
        {
            if (ModelState.IsValid)
            {
                db.GPAStates.Add(gPAState);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(gPAState);
        }

        // GET: GPAStates/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GPAState gPAState = db.GPAStates.Find(id);
            if (gPAState == null)
            {
                return HttpNotFound();
            }
            return View(gPAState);
        }

        // POST: GPAStates/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "GPAStateId,LowerLimit,UpperLimit,TuitionRateFactor")] GPAState gPAState)
        {
            if (ModelState.IsValid)
            {
                db.Entry(gPAState).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(gPAState);
        }

        // GET: GPAStates/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GPAState gPAState = db.GPAStates.Find(id);
            if (gPAState == null)
            {
                return HttpNotFound();
            }
            return View(gPAState);
        }

        // POST: GPAStates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            GPAState gPAState = db.GPAStates.Find(id);
            db.GPAStates.Remove(gPAState);
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

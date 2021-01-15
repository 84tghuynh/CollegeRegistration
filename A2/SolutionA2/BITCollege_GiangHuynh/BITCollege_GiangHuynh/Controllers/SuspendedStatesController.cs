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
    public class SuspendedStatesController : Controller
    {
        private BITCollege_GiangHuynhContext db = new BITCollege_GiangHuynhContext();

        // GET: SuspendedStates
        // {Giang}Change GPAStates to SuspendedStates
        // From View(db.GPAStates.ToList());
        public ActionResult Index()
        {
            //  return View(db.SuspendedStates.ToList());
            return View(SuspendedState.getInstance());
        }

        // GET: SuspendedStates/Details/5
        // {Giang} Cast to SuspendedState from GPAState
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SuspendedState suspendedState = (SuspendedState)db.GPAStates.Find(id);
            if (suspendedState == null)
            {
                return HttpNotFound();
            }
            return View(suspendedState);
        }

        // GET: SuspendedStates/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SuspendedStates/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "GPAStateId,LowerLimit,UpperLimit,TuitionRateFactor")] SuspendedState suspendedState)
        {
            if (ModelState.IsValid)
            {
                db.GPAStates.Add(suspendedState);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(suspendedState);
        }

        // GET: SuspendedStates/Edit/5
        // {Giang} Cast to SuspendedState from GPAState
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SuspendedState suspendedState = (SuspendedState)db.GPAStates.Find(id);
            if (suspendedState == null)
            {
                return HttpNotFound();
            }
            return View(suspendedState);
        }

        // POST: SuspendedStates/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "GPAStateId,LowerLimit,UpperLimit,TuitionRateFactor")] SuspendedState suspendedState)
        {
            if (ModelState.IsValid)
            {
                db.Entry(suspendedState).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(suspendedState);
        }

        // GET: SuspendedStates/Delete/5
        // {Giang} Cast to SuspendedState from GPAState
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SuspendedState suspendedState = (SuspendedState)db.GPAStates.Find(id);
            if (suspendedState == null)
            {
                return HttpNotFound();
            }
            return View(suspendedState);
        }

        // POST: SuspendedStates/Delete/5
        // {Giang} Cast to SuspendedState from GPAState
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SuspendedState suspendedState = (SuspendedState)db.GPAStates.Find(id);
            db.GPAStates.Remove(suspendedState);
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

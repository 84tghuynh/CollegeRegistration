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
    public class ProbationStatesController : Controller
    {
        private BITCollege_GiangHuynhContext db = new BITCollege_GiangHuynhContext();

        // GET: ProbationStates
        // {Giang}Change GPAStates to ProbationStates
        // From View(db.GPAStates.ToList());
        public ActionResult Index()
        {
            //return View(db.ProbationStates.ToList());
            return View(ProbationState.getInstance());
        }

        // GET: ProbationStates/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProbationState probationState = (ProbationState)db.GPAStates.Find(id);
            if (probationState == null)
            {
                return HttpNotFound();
            }
            return View(probationState);
        }

        // GET: ProbationStates/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProbationStates/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "GPAStateId,LowerLimit,UpperLimit,TuitionRateFactor")] ProbationState probationState)
        {
            if (ModelState.IsValid)
            {
                db.GPAStates.Add(probationState);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(probationState);
        }

        // GET: ProbationStates/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProbationState probationState = (ProbationState)db.GPAStates.Find(id);
            if (probationState == null)
            {
                return HttpNotFound();
            }
            return View(probationState);
        }

        // POST: ProbationStates/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "GPAStateId,LowerLimit,UpperLimit,TuitionRateFactor")] ProbationState probationState)
        {
            if (ModelState.IsValid)
            {
                db.Entry(probationState).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(probationState);
        }

        // GET: ProbationStates/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProbationState probationState = (ProbationState)db.GPAStates.Find(id);
            if (probationState == null)
            {
                return HttpNotFound();
            }
            return View(probationState);
        }

        // POST: ProbationStates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProbationState probationState = (ProbationState)db.GPAStates.Find(id);
            db.GPAStates.Remove(probationState);
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

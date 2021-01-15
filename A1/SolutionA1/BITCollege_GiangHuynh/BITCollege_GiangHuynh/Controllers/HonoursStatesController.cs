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
    public class HonoursStatesController : Controller
    {
        private BITCollege_GiangHuynhContext db = new BITCollege_GiangHuynhContext();

        // GET: HonoursStates
        // {Giang}Change GPAStates to HonoursStates
        // From View(db.GPAStates.ToList());
        public ActionResult Index()
        {
            // return View(db.HonoursStates.ToList());
            return View(HonoursState.getInstance());
        }

        // GET: HonoursStates/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HonoursState honoursState = (HonoursState)db.GPAStates.Find(id);
            if (honoursState == null)
            {
                return HttpNotFound();
            }
            return View(honoursState);
        }

        // GET: HonoursStates/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: HonoursStates/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "GPAStateId,LowerLimit,UpperLimit,TuitionRateFactor")] HonoursState honoursState)
        {
            if (ModelState.IsValid)
            {
                db.GPAStates.Add(honoursState);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(honoursState);
        }

        // GET: HonoursStates/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HonoursState honoursState = (HonoursState)db.GPAStates.Find(id);
            if (honoursState == null)
            {
                return HttpNotFound();
            }
            return View(honoursState);
        }

        // POST: HonoursStates/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "GPAStateId,LowerLimit,UpperLimit,TuitionRateFactor")] HonoursState honoursState)
        {
            if (ModelState.IsValid)
            {
                db.Entry(honoursState).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(honoursState);
        }

        // GET: HonoursStates/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HonoursState honoursState = (HonoursState)db.GPAStates.Find(id);
            if (honoursState == null)
            {
                return HttpNotFound();
            }
            return View(honoursState);
        }

        // POST: HonoursStates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            HonoursState honoursState = (HonoursState)db.GPAStates.Find(id);
            db.GPAStates.Remove(honoursState);
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

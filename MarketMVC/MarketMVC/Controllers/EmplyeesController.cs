using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MarketMVC.Data;
using MarketMVC.Helpers;
using MarketMVC.Models;

namespace MarketMVC.Controllers
{
    public class EmplyeesController : Controller
    {
        private MarketMVCContext db = new MarketMVCContext();

        // GET: Emplyees
        public ActionResult Index()
        {
            var emplyees = db.Emplyees.Include(e => e.DocumentType);
            return View(emplyees.ToList());
        }

        // GET: Emplyees/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Emplyee emplyee = db.Emplyees.Find(id);
            if (emplyee == null)
            {
                return HttpNotFound();
            }
            return View(emplyee);
        }

        // GET: Emplyees/Create
        public ActionResult Create()
        {

            ViewBag.DocumentTypeID = new SelectList(CombosHelper.GetDocumentTypes(), "DocumentTypeID", "Description");
            return View();
        }

        // POST: Emplyees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EmplyeeID,FirstName,LastName,Salary,BonusPercent,DateOfBirth,StartDate,Email,URL,DocumentTypeID")] Emplyee emplyee)
        {
            if (ModelState.IsValid)
            {
                db.Emplyees.Add(emplyee);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DocumentTypeID = new SelectList(CombosHelper.GetDocumentTypes(), "DocumentTypeID", "Description", emplyee.DocumentTypeID);
            return View(emplyee);
        }

        // GET: Emplyees/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Emplyee emplyee = db.Emplyees.Find(id);
            if (emplyee == null)
            {
                return HttpNotFound();
            }
            ViewBag.DocumentTypeID = new SelectList(db.DocumentTypes, "DocumentTypeID", "Description", emplyee.DocumentTypeID);
            return View(emplyee);
        }

        // POST: Emplyees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EmplyeeID,FirstName,LastName,Salary,BonusPercent,DateOfBirth,StartDate,Email,URL,DocumentTypeID")] Emplyee emplyee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(emplyee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DocumentTypeID = new SelectList(db.DocumentTypes, "DocumentTypeID", "Description", emplyee.DocumentTypeID);
            return View(emplyee);
        }

        // GET: Emplyees/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Emplyee emplyee = db.Emplyees.Find(id);
            if (emplyee == null)
            {
                return HttpNotFound();
            }
            return View(emplyee);
        }

        // POST: Emplyees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Emplyee emplyee = db.Emplyees.Find(id);
            db.Emplyees.Remove(emplyee);
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

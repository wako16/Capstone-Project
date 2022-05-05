using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Cap.Models;

namespace Cap.Controllers
{
    public class LineItemsController : Controller
    {
        private CAPEntities db = new CAPEntities();

        // GET: LineItems
        public ActionResult Index(int? id)
        {
            var lineItems = db.Reservations.Find(id).LineItems;
            return PartialView(lineItems.ToList());
        }

        // GET: LineItems/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LineItem lineItem = db.LineItems.Find(id);
            if (lineItem == null)
            {
                return HttpNotFound();
            }
            return View(lineItem);
        }

        // GET: LineItems/Create
        public ActionResult Create(int? id)
        {
            var reservation = db.Reservations.Find(id);
            var LineItem = new LineItem
            {
                ReservationId = id.Value,
                Reservation = reservation
            };

            ViewBag.ParkId = new SelectList(db.Parks, "Id", "Name");
            ViewBag.SportId = new SelectList(db.Sports, "Id", "Name");
            return View(LineItem);
        }

        // POST: LineItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ReservationId,ParkId,SportId,UnitPrice")] LineItem lineItem)
        {
            if (ModelState.IsValid)
            {
                db.LineItems.Add(lineItem);
                db.SaveChanges();
                return RedirectToAction("Create" , "LineItems" , new { id = lineItem.ReservationId });
            }

            var reservation = db.Reservations.Find(lineItem.ReservationId);
            lineItem.Reservation = reservation;
            ViewBag.ParkId = new SelectList(db.Parks, "Id", "Name", lineItem.ParkId);
            ViewBag.SportId = new SelectList(db.Sports, "Id", "Name", lineItem.SportId);
            return View(lineItem);
        }

        // GET: LineItems/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LineItem lineItem = db.LineItems.Find(id);
            if (lineItem == null)
            {
                return HttpNotFound();
            }
            ViewBag.ParkId = new SelectList(db.Parks, "Id", "Name", lineItem.ParkId);
            ViewBag.ReservationId = new SelectList(db.Reservations, "Id", "CustomerId", lineItem.ReservationId);
            ViewBag.SportId = new SelectList(db.Sports, "Id", "Name", lineItem.SportId);
            return View(lineItem);
        }

        // POST: LineItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ReservationId,ParkId,SportId,UnitPrice")] LineItem lineItem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lineItem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ParkId = new SelectList(db.Parks, "Id", "Name", lineItem.ParkId);
            ViewBag.ReservationId = new SelectList(db.Reservations, "Id", "CustomerId", lineItem.ReservationId);
            ViewBag.SportId = new SelectList(db.Sports, "Id", "Name", lineItem.SportId);
            return View(lineItem);
        }

        // GET: LineItems/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LineItem lineItem = db.LineItems.Find(id);
            if (lineItem == null)
            {
                return HttpNotFound();
            }
            return View(lineItem);
        }

        // POST: LineItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LineItem lineItem = db.LineItems.Find(id);
            db.LineItems.Remove(lineItem);
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

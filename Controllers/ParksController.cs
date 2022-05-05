using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Cap.Models;
using Cap.Models.ViewModels;
using Microsoft.AspNet.Identity;

namespace Cap.Controllers
{
    public class ParksController : Controller
    {
        private CAPEntities db = new CAPEntities();

        // GET: Parks
        public ActionResult Index()
        {
            return View(db.Parks.ToList());
        }

        // GET: Parks/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Park park = db.Parks.Find(id);
            if (park == null)
            {
                return HttpNotFound();
            }
            return View(park);
        }

        // GET: Parks/Create
        public ActionResult Create()
        {
            ViewBag.allSports = new MultiSelectList(db.Sports.ToList(), "Id", "Name");
            return View();
        }

        // POST: Parks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( parkCreateViewModel parkViewModel)
        {
            if (ModelState.IsValid)
            {
                var park = new Park()
                {
                    Name = parkViewModel.Name,
                    ParkAddress = parkViewModel.ParkAddress,
                    Sports = new List <Sport>()

                };
                if (parkViewModel.SportIds != null)
                {
                    foreach (var SportId in parkViewModel.SportIds)
                    {
                        var Sport = db.Sports.Find(SportId);
                        park.Sports.Add(Sport);

                    }
                }

                db.Parks.Add(park);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.allSports = new MultiSelectList(db.Sports.ToList(), "Id", "Name");
            return View(parkViewModel);
        }
        public ActionResult favorite(int? id)
        {
            var park = db.Parks.Find(id);
            return View(park);
        }
        [HttpPost, ActionName("favorite")]
        public ActionResult favoriteConfirmed(int? id)
        {
            var userId = User.Identity.GetUserId();
            var customer = db.Customers.Find(userId);
            var park = db.Parks.Find(id);
            park.Customers.Add(customer);

            db.Entry(park).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");

        }

        // GET: Parks/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Park park = db.Parks.Find(id);
            if (park == null)
            {
                return HttpNotFound();
            }
            return View(park);
        }

        // POST: Parks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] Park park)
        {
            if (ModelState.IsValid)
            {
                db.Entry(park).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(park);
        }

        // GET: Parks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Park park = db.Parks.Find(id);
            if (park == null)
            {
                return HttpNotFound();
            }
            return View(park);
        }

        // POST: Parks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Park park = db.Parks.Find(id);
            park.Sports.Clear();
            park.LineItems.Clear();
            park.ParkProducts.Clear();
            park.Reviews.Clear();
                      db.Parks.Remove(park);
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

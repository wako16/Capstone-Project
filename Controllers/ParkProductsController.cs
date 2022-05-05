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
    public class ParkProductsController : Controller
    {
        private CAPEntities db = new CAPEntities();

        // GET: ParkProducts
        public ActionResult Index()
        {
            var parkProducts = db.ParkProducts.Include(p => p.Park).Include(p => p.Product);
            return View(parkProducts.ToList());
        }

        // GET: ParkProducts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ParkProduct parkProduct = db.ParkProducts.Find(id);
            if (parkProduct == null)
            {
                return HttpNotFound();
            }
            return View(parkProduct);
        }

        // GET: ParkProducts/Create
        public ActionResult Create()
        {
            ViewBag.ParkId = new SelectList(db.Parks, "Id", "Name");
            ViewBag.ProductId = new SelectList(db.Products, "Id", "Name");
            return View();
        }

        // POST: ParkProducts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ParkId,ProductId,Price")] ParkProduct parkProduct)
        {
            if (ModelState.IsValid)
            {
                db.ParkProducts.Add(parkProduct);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ParkId = new SelectList(db.Parks, "Id", "Name", parkProduct.ParkId);
            ViewBag.ProductId = new SelectList(db.Products, "Id", "Name", parkProduct.ProductId);
            return View(parkProduct);
        }

        // GET: ParkProducts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ParkProduct parkProduct = db.ParkProducts.Find(id);
            if (parkProduct == null)
            {
                return HttpNotFound();
            }
            ViewBag.ParkId = new SelectList(db.Parks, "Id", "Name", parkProduct.ParkId);
            ViewBag.ProductId = new SelectList(db.Products, "Id", "Name", parkProduct.ProductId);
            return View(parkProduct);
        }

        // POST: ParkProducts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ParkId,ProductId,Price")] ParkProduct parkProduct)
        {
            if (ModelState.IsValid)
            {
                db.Entry(parkProduct).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ParkId = new SelectList(db.Parks, "Id", "Name", parkProduct.ParkId);
            ViewBag.ProductId = new SelectList(db.Products, "Id", "Name", parkProduct.ProductId);
            return View(parkProduct);
        }

        // GET: ParkProducts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ParkProduct parkProduct = db.ParkProducts.Find(id);
            if (parkProduct == null)
            {
                return HttpNotFound();
            }
            return View(parkProduct);
        }

        // POST: ParkProducts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ParkProduct parkProduct = db.ParkProducts.Find(id);
            db.ParkProducts.Remove(parkProduct);
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

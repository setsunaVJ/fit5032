using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Foodspotting.Models;
using Microsoft.AspNet.Identity;

namespace Foodspotting.Controllers
{
    public class ReservationsController : Controller
    {
        private foodspotting db = new foodspotting();

        // GET: Reservations
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            var reservation = db.Reservations.Where(s => s.UserID == userId).ToList();
            return View(reservation);
            //var reservations = db.Reservations.Include(r => r.AspNetUser).Include(r => r.Restaurant);
            //return View(reservations.ToList());
        }
        public ActionResult OwnerIndex()
        {
            //var userId = User.Identity.GetUserId();
            //var reservation = db.Reservations.Where(s => s.UserID == userId).ToList();
            //return View(reservation);
            var reservations = db.Reservations.Include(r => r.AspNetUser).Include(r => r.Restaurant);
            return View(reservations.ToList());
        }

        // GET: Reservations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservation reservation = db.Reservations.Find(id);
            if (reservation == null)
            {
                return HttpNotFound();
            }
            return View(reservation);
        }

        // GET: Reservations/Create
        public ActionResult Create()
        {
            
            ViewBag.ReId = new SelectList(db.Restaurants, "ReId", "Name");
            return View();
        }

        // POST: Reservations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserID,ReId,ReserId,Date,Seat,reser_description")] Reservation reservation)
        {
            reservation.UserID = User.Identity.GetUserId();
            ModelState.Clear();
            TryValidateModel(reservation);
            if (ModelState.IsValid)
            {
                db.Reservations.Add(reservation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            
            ViewBag.ReId = new SelectList(db.Restaurants, "ReId", "Name", reservation.ReId);
            return View(reservation);
        }

        // GET: Reservations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservation reservation = db.Reservations.Find(id);
            if (reservation == null)
            {
                return HttpNotFound();
            }
            
            ViewBag.ReId = new SelectList(db.Restaurants, "ReId", "Name", reservation.ReId);
            return View(reservation);
        }

        // POST: Reservations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserID,ReId,ReserId,Date,Seat,reser_description")] Reservation reservation)
        {
            reservation.UserID = User.Identity.GetUserId();
            if (ModelState.IsValid)
            {
                db.Entry(reservation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            
            ViewBag.ReId = new SelectList(db.Restaurants, "ReId", "Name", reservation.ReId);
            return View(reservation);
        }

        // GET: Reservations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservation reservation = db.Reservations.Find(id);
            if (reservation == null)
            {
                return HttpNotFound();
            }
            return View(reservation);
        }

        // POST: Reservations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Reservation reservation = db.Reservations.Find(id);
            db.Reservations.Remove(reservation);
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

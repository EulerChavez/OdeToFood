using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OdeToFood.Models;

namespace OdeToFood.Controllers {

    public class RestaurantController : Controller {

        private OdeToFoodDb db = new OdeToFoodDb();

        // GET: Restaurants
        public ActionResult Index() {

            return View(db.Restaurants.ToList());

        }

        // GET: Restaurants/Create
        public ActionResult Create() {

            return View();

        }

        // POST: Restaurants/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,City,Country")] Restaurant restaurant) {

            if (ModelState.IsValid) {

                db.Restaurants.Add(restaurant);

                db.SaveChanges();

                return RedirectToAction("Index");

            }

            return View(restaurant);

        }

        // GET: Restaurants/Edit/5
        public ActionResult Edit(int? id) {

            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Restaurant restaurant = db.Restaurants.Find(id);

            if (restaurant == null)
                return HttpNotFound();

            return View(restaurant);

        }

        // POST: Restaurants/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,City,Country")] Restaurant restaurant) {

            if (ModelState.IsValid) {

                db.Entry(restaurant).State = EntityState.Modified;

                db.SaveChanges();

                return RedirectToAction("Index");

            }

            return View(restaurant);

        }

        // GET: Restaurants/Delete/5
        public ActionResult Delete(int? id) {

            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);


            Restaurant restaurant = db.Restaurants.Find(id);

            if (restaurant == null)
                return HttpNotFound();

            return View(restaurant);

        }

        // POST: Restaurants/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id) {

            Restaurant restaurant = db.Restaurants.Find(id);

            db.Restaurants.Remove(restaurant);

            db.SaveChanges();

            return RedirectToAction("Index");

        }

        protected override void Dispose(bool disposing) {

            if (disposing) {
                db.Dispose();
            }

            base.Dispose(disposing);

        }

    }

}

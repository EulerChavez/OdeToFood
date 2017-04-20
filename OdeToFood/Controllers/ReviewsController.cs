using OdeToFood.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OdeToFood.Controllers {

    public class ReviewsController : Controller {

        OdeToFoodDb _db = new OdeToFoodDb();

        //[ChildActionOnly]
        //public ActionResult BestReview() {

        //    var review = _reviews.OrderByDescending(r => r.Rating).FirstOrDefault();

        //    return PartialView("_Review", review);

        //}

        // GET: Reviews/restaurantId
        public ActionResult Index([Bind(Prefix = "id")]int restaurantId) {

            var model = _db.Restaurants
                           .Find(restaurantId);

            if (model != null)
                return View(model);

            return HttpNotFound();

        }

        [HttpGet]
        public ActionResult Create(int restaurantId) {

            return View();

        }

        [HttpGet]
        public ActionResult Edit(int id) {

            var model = _db.Reviews.Find(id);

            return View(model);

        }

        [HttpPost]
        public ActionResult Edit([Bind(Exclude = "ReviewerName")]RestaurantReview review) {

            if (ModelState.IsValid) {

                _db.Entry(review).State = System.Data.Entity.EntityState.Modified;

                _db.SaveChanges();

                return RedirectToAction("Index", new { id = review.RestaurantId });

            }

            return View(review);

        }

        [HttpPost]
        public ActionResult Create(RestaurantReview review) {

            if (ModelState.IsValid) {

                _db.Reviews.Add(review);

                _db.SaveChanges();

                return RedirectToAction("Index", new { id = review.RestaurantId });

            }

            return View(review);

        }


        protected override void Dispose(bool disposing) {

            if (disposing) {
                _db.Dispose();
            }

            base.Dispose(disposing);

        }

    }

}

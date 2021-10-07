using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CodingTest.Models.DatabaseModels;

namespace CodingTest.Controllers
{
    public class CarColorsController : Controller
    {
        private CarEntities db = new CarEntities();

        // GET: CarColors
        public ActionResult Index()
        {
            return View(db.CarColors.ToList());
        }

        // GET: CarColors/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CarColor carColor = db.CarColors.Find(id);
            if (carColor == null)
            {
                return HttpNotFound();
            }
            return View(carColor);
        }

        // GET: CarColors/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CarColors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Color")] CarColor carColor)
        {
            if (ModelState.IsValid)
            {
                carColor.ID = Guid.NewGuid();
                db.CarColors.Add(carColor);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(carColor);
        }

        // GET: CarColors/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CarColor carColor = db.CarColors.Find(id);
            if (carColor == null)
            {
                return HttpNotFound();
            }
            return View(carColor);
        }

        // POST: CarColors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Color")] CarColor carColor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(carColor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(carColor);
        }

        // GET: CarColors/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CarColor carColor = db.CarColors.Find(id);
            if (carColor == null)
            {
                return HttpNotFound();
            }
            return View(carColor);
        }

        // POST: CarColors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            CarColor carColor = db.CarColors.Find(id);
            db.CarColors.Remove(carColor);
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

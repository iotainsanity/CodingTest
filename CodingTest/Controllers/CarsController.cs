using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;
using CodingTest.DataLayer.Repositories;
using CodingTest.Models.DatabaseModels;

namespace CodingTest.Controllers
{
    public class CarsController : Controller
    {
        private CarRepository _carRepository = new CarRepository();

        // GET: Cars
        public ActionResult Index()
        {
            var cars = _carRepository.GetAllCars();
            return View(cars.ToList());
        }

        // GET: Cars/Details/5
        public ActionResult Details(Guid id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Car car = _carRepository.GetCar(id);
            if (car == null)
            {
                return HttpNotFound();
            }
            return View(car);
        }

        // GET: Cars/Create
        public ActionResult Create()
        {
            ViewBag.Color = new SelectList(new CarColorRepository().GetAllCarColor(), "ID", "Color");
            return View();
        }

        // POST: Cars/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Color,YearMade")] Car car)
        {
            if (ModelState.IsValid)
            {
                //var carRepository = new CarRepository();
                //carRepository.Insert(car);

                if (car.ID == Guid.Empty)
                    car.ID = Guid.NewGuid();

                string URL = "https://localhost:44388/";
                string urlParameters = "";

                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(URL);

                // Add an Accept header for JSON format.
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = client.PostAsJsonAsync("Api/Cars/Create", car).Result;

                return RedirectToAction("Index");
            }

            ViewBag.Color = new SelectList(new CarColorRepository().GetAllCarColor(), "ID", "Color", car.Color);
            return View(car);
        }

        // GET: Cars/Edit/5
        public ActionResult Edit(Guid id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Car car = _carRepository.GetCar(id);
            if (car == null)
            {
                return HttpNotFound();
            }
            ViewBag.Color = new SelectList(new CarColorRepository().GetAllCarColor(), "ID", "Color", car.Color);
            return View(car);
        }

        // POST: Cars/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Color,YearMade")] Car car)
        {
            if (ModelState.IsValid)
            {
                _carRepository.Update(car);
                return RedirectToAction("Index");
            }
            ViewBag.Color = new SelectList(new CarColorRepository().GetAllCarColor(), "ID", "Color", car.Color);
            return View(car);
        }

        // GET: Cars/Delete/5
        public ActionResult Delete(Guid id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Car car = _carRepository.GetCar(id);
            if (car == null)
            {
                return HttpNotFound();
            }
            return View(car);
        }

        // POST: Cars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            _carRepository.Delete(id);

            return RedirectToAction("Index");
        }
    }
}

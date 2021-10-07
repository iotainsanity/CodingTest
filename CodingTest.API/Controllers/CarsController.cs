using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using CodingTest.API.Models;
using CodingTest.DataLayer.Repositories;
using CodingTest.Models.DatabaseModels;

namespace CodingTest.API.Controllers
{
    public class CarsController : ApiController
    {
        private CarEntities db = new CarEntities();

        [HttpGet]
        public List<Car> GetCarByModelYear(int year)
        {
            return db.Cars.Where(x => x.YearMade == year).ToList();
        }

        // PUT: api/Cars/5
        [HttpPut]
        [ResponseType(typeof(void))]
        public IHttpActionResult Update(APICarModel car)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var c = db.Cars.Where(x => x.ID == car.ID).FirstOrDefault();
            if (c != null)
            {
                c.Name = car.Name;
                c.Color = car.ColorID;
                c.YearMade = car.YearMade;
            }

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CarExists(c.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Cars
        [HttpPost]
        public IHttpActionResult Create(APICarModel car)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Car c = new Car();
            c.ID = car.ID;
            c.Name = car.Name;
            c.Color = car.ColorID;
            c.YearMade = car.YearMade;

            db.Cars.Add(c);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (CarExists(c.ID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }
            return CreatedAtRoute("DefaultApi", new { id = c.ID }, c);
        }



        // DELETE: api/Cars/5
        [ResponseType(typeof(Car))]
        public IHttpActionResult DeleteCar(Guid id)
        {
            Car car = db.Cars.Find(id);
            if (car == null)
            {
                return NotFound();
            }

            db.Cars.Remove(car);
            db.SaveChanges();

            return Ok(car);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CarExists(Guid id)
        {
            return db.Cars.Count(e => e.ID == id) > 0;
        }
    }
}
using CodingTest.Models.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CodingTest.DataLayer.Repositories
{
    public class CarRepository
    {
        private CarEntities db = new CarEntities();

        public bool Insert(Car car)
        {
            car.ID = Guid.NewGuid();
            db.Cars.Add(car);
            db.SaveChanges();

            return true;
        }
        
        public bool Update(Car car)
        {
            db.Entry(car).State = EntityState.Modified;
            db.SaveChanges();

            return true;
        }

        public bool Delete(Guid id)
        {
            Car car = GetCar(id);
            db.Cars.Remove(car);
            db.SaveChanges();

            return true;
        }

        public List<Car> GetAllCars()
        {
            var cars = db.Cars.Include(c => c.CarColor);
            return cars.ToList<Car>();
        }

        public Car GetCar(Guid id)
        {
            var car = db.Cars.Include(c => c.CarColor).Where(x => x.ID == id).First();
            return car;
        }
    }
}
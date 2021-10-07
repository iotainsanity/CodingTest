using CodingTest.Models.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CodingTest.DataLayer.Repositories
{
    public class CarColorRepository
    {
        private CarEntities db = new CarEntities();

        public List<CarColor> GetAllCarColor()
        {
            var carColors = db.CarColors;
            return carColors.ToList<CarColor>();
        }
    }
}
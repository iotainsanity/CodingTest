using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CodingTest.API.Models
{
    public partial class APICarModel 
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public Guid ColorID { get; set; }
        public int YearMade { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebScrapingPOC.Models
{
    public class VehicleSearchResult
    {
        public int TotalCount { get; set; }

        public List<VehicleDetail> Vehicles { get; set; }
            = new List<VehicleDetail>();
    }

    public class VehicleDetail
    {
        public string Name { get; set; }

        public string Price { get; set; }

        public string Mileage { get; set; }

        public string Interior { get; set; }

        public string Exterior { get; set; }

        public string Vin { get; set; }

        public string Stock { get; set; }

        public string Year { get; set; }

        public string Make { get; set; }

        public string Model { get; set; }

        public string BodyStyle { get; set; }

        public string Type { get; set; }
    }
}

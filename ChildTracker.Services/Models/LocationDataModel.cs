using ChildTracker.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace ChildTracker.Services.Controllers
{
    public class LocationDataModel
    {
        public static LocationDataModel FromGeolocation(Geolocation location)
        {
            return new LocationDataModel
            {
                Latitude = location.Latitude,
                Longitude = location.Longitude,
                CreationDate = location.CreatedOn
            };
        }

        [Required]
        public decimal Latitude { get; set; }

        [Required]
        public decimal Longitude { get; set; }

        public DateTime CreationDate { get; set; }
    }
}


namespace ChildTracker.Services.Controllers
{
    using ChildTracker.Data;
    using ChildTracker.Data.Contracts;
    using ChildTracker.Models;

    using Microsoft.AspNet.Identity;

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;

    [Authorize]
    public class LocationsController : ApiController
    {
        protected IChildTrackerData data;

        public LocationsController()
            : this(new ChildTrackerData(new ApplicationDbContext()))
        {
        }

        public LocationsController(IChildTrackerData data)
        {
            this.data = data;
        }

        [HttpGet]
        public IHttpActionResult GetLocation()
        {
            var userId = this.User.Identity.GetUserId();

            var lastLocation = this.data.Locations.All()
                            .Where(l => l.UserID == userId)
                            .OrderByDescending(l => l.CreatedOn)
                            .FirstOrDefault();

            if (lastLocation == null)
            {
                return BadRequest("No locations in database!");
            }

            var locationModel = LocationDataModel.FromGeolocation(lastLocation);
            return Ok(locationModel);
        }

        [HttpGet]
        public IHttpActionResult GetLocation(int id)
        {
            var userId = this.User.Identity.GetUserId();

            var location = this.data.Locations.All()
                .Where(l => l.UserID == userId)
                .FirstOrDefault(l => l.ID == id);

            if (location == null)
            {
                return BadRequest("No location entry with this ID");
            }

            var locationModel = LocationDataModel.FromGeolocation(location);
            return Ok(locationModel);
        }

        [HttpPost]
        public IHttpActionResult AddLocation(LocationDataModel locationModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Incorrect data passed for location!");
            }

            var userId = this.User.Identity.GetUserId();

            var newLocation = new Geolocation
            {
                Latitude = locationModel.Latitude,
                Longitude = locationModel.Longitude,
                UserID = userId,
                CreatedOn = DateTime.Now
            };

            this.data.Locations.Add(newLocation);
            this.data.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = newLocation.ID }, newLocation);
        }
    }
}

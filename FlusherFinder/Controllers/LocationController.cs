using FlusherFinder.Models;
using FlusherFinder.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FlusherFinder.Controllers
{
    [Authorize]
    public class LocationController : ApiController
    {
        private LocationService CreateLocationService()
        {
            var creatorId = Guid.Parse(User.Identity.GetUserId());
            var locationService = new LocationService(creatorId);
            return locationService;
        }

        public IHttpActionResult Get()
        {
            LocationService locationService = CreateLocationService();
            var locations = locationService.GetLocations();
            return Ok(locations);
        }

        public IHttpActionResult Get(int id)
        {
            LocationService locationService = CreateLocationService();
            var location = locationService.GetLocationsById(id);
            return Ok(location);
        }

        public IHttpActionResult Post(LocationCreate location)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateLocationService();

            if (!service.CreateLocation(location))
                return InternalServerError();

            return Ok();
        }

        public IHttpActionResult Put(LocationEdit location)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateLocationService();

            if (!service.UpdateLocation(location))
                return InternalServerError();

            return Ok();
        }

        public IHttpActionResult Delete(int id)
        {
            var service = CreateLocationService();

            if (!service.DeleteLocation(id))
                return InternalServerError();

            return Ok();
        }

    }
}

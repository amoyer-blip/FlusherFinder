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
    public class RatingController : ApiController
    {
        private RatingService CreateRatingService()
        {
            var creatorId = Guid.Parse(User.Identity.GetUserId());
            var ratingService = new RatingService(creatorId);
            return ratingService;
        }

        public IHttpActionResult Get()
        {
            RatingService ratingService = CreateRatingService();
            var ratings = ratingService.GetRatings();
            return Ok(ratings);
        }

        public IHttpActionResult Get(int id)
        {
            RatingService ratingService = CreateRatingService();
            var rating = ratingService.GetRatingById(id);
            return Ok(rating);
        }
        
        public IHttpActionResult Post(RatingCreate rating)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateRatingService();

            if (!service.CreateRating(rating))
                return InternalServerError();

            return Ok();
        }

        public IHttpActionResult Put(RatingEdit rating)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateRatingService();

            if (!service.UpdateRating(rating))
                return InternalServerError();

            return Ok();
        }

        public IHttpActionResult Delete(int id)
        {
            var service = CreateRatingService();

            if (!service.DeleteRating(id))
                return InternalServerError();

            return Ok();
        }
    }
}

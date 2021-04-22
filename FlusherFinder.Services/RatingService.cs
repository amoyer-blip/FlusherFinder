using FlusherFinder.Data;
using FlusherFinder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlusherFinder.Services
{
    public class RatingService
    {
        private readonly Guid _userId;

        public RatingService(Guid creatorId)
        {
            _userId = creatorId;
        }

        public bool CreateRating(RatingCreate model)
        {
            var entity = new Rating()
            {
                LocationId = model.LocationId,
                CleanlinessRating = model.CleanlinessRating,
                AccessibilityRating = model.AccessibilityRating,
                AmenitiesRating = model.AmenitiesRating

            };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Ratings.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<RatingListItem> GetRatings()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query = ctx
                    .Ratings
                    .Where(e => e.CreatorId == _userId)
                    .Select(
                     e =>
                        new RatingListItem
                        {
                            RatingId = e.RatingId,
                            Location = e.Location,
                            CreatedUtc = e.CreatedUtc
                        }
                        );
                return query.ToArray();
            }
        }

        public RatingDetail GetRatingById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Ratings
                        .Single(e => e.RatingId == id && e.CreatorId == _userId);
                return
                    new RatingDetail
                    {
                        RatingId = entity.RatingId,
                        Location = entity.Location,
                        CleanlinessRating = entity.CleanlinessRating,
                        AccessibilityRating = entity.AccessibilityRating,
                        AmenitiesRating = entity.AmenitiesRating
                        
                    };
            }
        }
        public bool UpdateRating(RatingEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Ratings
                        .Single(e => e.RatingId == model.RatingId && e.CreatorId == _userId);

                entity.RatingId = model.RatingId;
                entity.Location = model.Location;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteRating(int ratingId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Ratings
                        .Single(e => e.RatingId == ratingId && e.CreatorId == _userId);

                ctx.Ratings.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}

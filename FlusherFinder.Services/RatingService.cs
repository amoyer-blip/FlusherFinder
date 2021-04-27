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
        private readonly Guid _creatorId;

        public RatingService(Guid creatorId)
        {
            _creatorId = creatorId;
        }

        public bool CreateRating(RatingCreate model)
        {
            var entity = new Rating()
            {
                CreatorId = _creatorId,
                LocationId = model.LocationId,
                CleanlinessRating = model.CleanlinessRating,
                AccessibilityRating = model.AccessibilityRating,
                AmenitiesRating = model.AmenitiesRating,
                CreatedUtc = DateTimeOffset.Now

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
                    .Where(e => e.CreatorId == _creatorId)
                    .Select(
                     e =>
                        new RatingListItem
                        {
                            RatingId = e.RatingId,
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
                        .Single(e => e.RatingId == id && e.CreatorId == _creatorId);
                return
                    new RatingDetail
                    {
                        RatingId = entity.RatingId,
                        LocationId = (entity.LocationId is null) ? null : entity.LocationId,
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
                        .Single(e => e.RatingId == model.RatingId && e.CreatorId == _creatorId);

                entity.RatingId = model.RatingId;
                entity.CleanlinessRating = model.CleanlinessRating;
                entity.AccessibilityRating = model.AccessibilityRating;
                entity.AmenitiesRating = model.AmenitiesRating;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteRating(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Ratings
                        .Single(e => e.RatingId == id && e.CreatorId == _creatorId);

                ctx.Ratings.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}

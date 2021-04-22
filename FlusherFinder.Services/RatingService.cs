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

        public RatingService(Guid userId)
        {
            _userId = userId;
        }

        public bool CreateRating(RatingCreate model)
        {
            var entity = new RatingService()
            {
                OwnerId = _userId,
                Title = model.Title,
                Content = model.Content,
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
                    .Where(e => e.OwnerId == _userId)
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
                        .Single(e => e.RatingId == RatingDetail && e.OwnerId == _userId);
                return
                    new RatingDetail
                    {
                        RatingId = entity.RatingId,
                        Title = entity.Title,
                        Content = entity.Content,
                        CreatedUtc = entity.CreatedUtc,
                        ModifiedUtc = entity.ModifiedUtc
                    };
            }
        }
    };
}

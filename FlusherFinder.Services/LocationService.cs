using FlusherFinder.Data;
using FlusherFinder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlusherFinder.Services
{
    public class LocationService
    {
        private readonly Guid _creatorId;

        public LocationService(Guid creatorId)
        {
            _creatorId = creatorId;
        }

        public bool CreateLocation(LocationCreate model)
        {
            var entity = new Location
            {
                CreatorId = _creatorId,
                LocationName = model.LocationName,
                LocationAddress = model.LocationAddress,
                IsFamilyFriendly = model.IsFamilyFriendly,
                IsTwentyFourHour = model.IsTwentyFourHour,
                CreatedUtc = DateTimeOffset.Now
            };
            using (var ctx = new ApplicationDbContext())
            {
                ctx.Locations.Add(entity);
                return ctx.SaveChanges() > 0;
            }
        }

        public IEnumerable<LocationListItem> GetLocations()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                    .Locations
                    .Where(e => e.CreatorId == _creatorId)
                    .Select(e => new LocationListItem
                    {
                        LocationId = e.LocationId,
                        LocationName = e.LocationName,
                        LocationAddress = e.LocationAddress
                    });

                return query.ToArray();
            }
        }

        public LocationDetails GetLocationsById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Locations
                    .Single(e => e.LocationId == id && e.CreatorId == _creatorId);

                return new Models.LocationDetails
                {
                    LocationId = entity.LocationId,
                    LocationName = entity.LocationName,
                    LocationAddress = entity.LocationAddress,
                    IsFamilyFriendly = entity.IsFamilyFriendly,
                    IsTwentyFourHour = entity.IsTwentyFourHour,
                    Rating = entity.Rating
                };

            }
        }

        public bool UpdateLocation(LocationEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Locations
                    .Single(e => e.LocationId == model.LocationId && e.CreatorId == _creatorId);

                entity.LocationId = model.LocationId;
                entity.LocationName = model.LocationName;
                entity.LocationAddress = model.LocationAddress;
                entity.IsFamilyFriendly = model.IsFamilyFriendly;
                entity.IsTwentyFourHour = model.IsTwentyFourHour;
                entity.ModifiedUtc = DateTimeOffset.UtcNow;

                return ctx.SaveChanges() == 1;

            }
        }

        public bool DeleteLocation(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Locations
                    .Single(e => e.LocationId == id && e.CreatorId == _creatorId);

                ctx.Locations.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}

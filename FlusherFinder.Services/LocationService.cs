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

        //Create
        public bool CreateLocation(LocationCreate model)
        {
            //Our mapping begins here 
            //Map a "LocationCreate" to a Location 
            var entity = new Location
            {
                //LocationCreate - ALL we need to create a new location:
                CreatorId = _creatorId,
                LocationName = model.LocationName,
                LocationAddress = model.LocationAddress,
                IsFamilyFriendly = model.IsFamilyFriendly,
                IsTwentyFourHour = model.IsTwentyFourHour,
                CreatedUtc = DateTimeOffset.Now
            };
            //Add to Locations (the Dbset<Location)
            using (var ctx = new ApplicationDbContext())
            {
                //Add the entity(Location) to the DbSet<Location> in IdentityModels.cs
                ctx.Locations.Add(entity);
                //Save to database 
                return ctx.SaveChanges() > 0;
            }
        }

        //Read
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

                return query.ToArray(); //Does this need to be changed to ToArray?
            }
        }

        //Read 
        public LocationDetails GetLocationsById(int id)
        {
            //LINQ
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

        //Update
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

        //Delete 
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

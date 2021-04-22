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
                LocationId = model.LocationId,
                LocationName = model.LocationName,
                LocationAddress = model.LocationAddress,
                CreatedUtc = model.CreatedUtc
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

                return query.ToList(); //Does this need to be changed to ToArray?
            }
        }

        //Read 
        public LocationDetails GetLocationsById(int locationId)
        {
            //LINQ
            using (var ctx = new ApplicationDbContext())
            {
                var location =
                    ctx
                    .Locations
                    .Single(e => e.CreatorId == _creatorId && e.LocationId == locationId);

                if (location is null)
                {
                    return null; 
                }

                return new Models.LocationDetails
                {
                    LocationId = location.LocationId,
                    LocationName = location.LocationName,
                    LocationAddress = location.LocationAddress, 
                    IsFamilyFriendly = location.IsFamilyFriendly, 
                    IsTwentyFourHour = location.IsTwentyFourHour, 
                    Rating = location.Rating
                };

            }
        }

        //Update
        public bool UpdateLocation(LocationEdit newLocationData)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var oldLocationData =
                    ctx
                    .Locations
                    .Single(e => e.LocationId == newLocationData.LocationId && e.CreatorId == _creatorId);

                oldLocationData.LocationId = newLocationData.LocationId;
                oldLocationData.LocationName = newLocationData.LocationName;
                oldLocationData.LocationAddress = newLocationData.LocationAddress;
                oldLocationData.ModifiedUtc = DateTimeOffset.UtcNow;

                return ctx.SaveChanges() == 1;

            }
        }

        //Delete 
        public bool DeleteLocation(int LocationId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Locations
                    .Single(e => e.LocationId == LocationId && e.CreatorId == _creatorId);

                ctx.Locations.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}

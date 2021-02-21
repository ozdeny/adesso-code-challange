using AdessoRideShare.Core;
using AdessoRideShare.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AdessoRideShare.Data.Repositories
{
    public class RideRepository : Repository<Ride>, IRideRepository
    {
        public RideRepository(AdessoRideShareContext context)
           : base(context)
        { }

        public Task<Ride> SingleOrDefaultWithUsersAsync(Expression<Func<Ride, bool>> predicate)
        {
            return Context.Set<Ride>().Include(r => r.JoinedUsers).SingleOrDefaultAsync(predicate);
        }


        public async Task<IEnumerable<Ride>> GetRidesWithOverlappingPaths(int departureCityId, int arrivalCityId)
        {
            return await Context.Set<Ride>().FromSqlRaw(@" SELECT * FROM Rides WHERE RideId IN 
                   (SELECT RDS.RideId from Rides RDS left join RouteCities RCT on RDS.RideId = RCT.RidesRideId
                    WHERE RCT.RouteCitiesCityId IN({0}, {1})
                     GROUP by RideId having count(*) > 1)", departureCityId, arrivalCityId).ToListAsync();
        }


    }
}

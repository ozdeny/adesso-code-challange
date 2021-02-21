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
            return await Context.Set<Ride>().FromSqlRaw(@"SELECT *
                                                                FROM   rides
                                                                WHERE  rideid IN (SELECT DISTINCT ridesrideid
                                                                                  FROM   (SELECT RCT.ridesrideid,
                                                                                                 routecitiescityid,
                                                                                                 ( Row_number()
                                                                                                     OVER (
                                                                                                       ORDER BY RCT.ridesrideid ASC) ) AS
                                                                                                 EdgeStoreOrder
                                                                                          FROM   (SELECT RidesRideid
                                                                                                  FROM   RouteCities
                                                                                                  WHERE  RouteCitiesCityid IN ( {0}, {1} )
                                                                                                  GROUP  BY RidesRideid
                                                                                                  HAVING Count(*) > 1) SQ1
                                                                                                 JOIN routecities RCT
                                                                                                   ON SQ1.ridesrideid = RCT.ridesrideid
                                                                                          WHERE  RCT.routecitiescityid IN ( {0}, {1} )) SQ2
                                                                                         JOIN (SELECT *,
                                                                                                      ( Row_number()
                                                                                                          OVER (
                                                                                                            ORDER BY edgeindex ASC) ) AS
                                                                                                      EdgeOrder
                                                                                               FROM   (SELECT {0} AS RouteEdge,
                                                                                                              1 AS EdgeIndex
                                                                                                       UNION ALL
                                                                                                       SELECT {1} AS RouteEdge,
                                                                                                              2 AS EdgeIndex)SOQ) OQR
                                                                                           ON SQ2.edgestoreorder = OQR.edgeindex
                                                                                              AND SQ2.routecitiescityid = routeedge)"
                                                                                , departureCityId, arrivalCityId).ToListAsync();
        }


    }
}

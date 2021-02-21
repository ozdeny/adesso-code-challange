using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AdessoRideShare.Core.Repositories
{
    public interface IRideRepository: IRepository<Ride>
    {
        Task<Ride> SingleOrDefaultWithUsersAsync(Expression<Func<Ride, bool>> predicate);

        Task<IEnumerable<Ride>> GetRidesWithOverlappingPaths(int departureCityId, int arrivalCityId);

    }
}

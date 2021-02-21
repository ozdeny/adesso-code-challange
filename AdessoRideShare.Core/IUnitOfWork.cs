using AdessoRideShare.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AdessoRideShare.Core
{
    public interface IUnitOfWork : IDisposable
    {
        IRideRepository Rides { get; }

        IRepository<User> Users { get; }

        IRepository<City> Cities { get; }


        Task<int> CommitAsync();
    }
}

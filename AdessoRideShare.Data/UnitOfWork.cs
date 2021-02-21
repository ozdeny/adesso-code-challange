using AdessoRideShare.Core;
using AdessoRideShare.Core.Repositories;
using AdessoRideShare.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AdessoRideShare.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AdessoRideShareContext _context;

        private RideRepository _rideRepository;

        private IRepository<User> _userRepository;

        private IRepository<City> _cityRepository;


        public IRideRepository Rides => _rideRepository = _rideRepository ?? new RideRepository(_context);

        public IRepository<User> Users => _userRepository = _userRepository ?? new Repository<User>(_context);

        public IRepository<City> Cities => _cityRepository = _cityRepository ?? new Repository<City>(_context);



        public UnitOfWork(AdessoRideShareContext context)
        {
            this._context = context;
        }

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}

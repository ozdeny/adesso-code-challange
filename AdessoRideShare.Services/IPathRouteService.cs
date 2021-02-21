using AdessoRideShare.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdessoRideShare.Services
{
    public interface IPathRouteService
    {

        int[] FindCitiesInRoute(City departureCity, City destinationCity);
    }
}

using System.Collections.Generic;

using Garage3.Models;

namespace Garage3.Utilites
{
    public interface IParkingSpaceCalculations
    {
        List<VehicleTypeCount> vehicleTypeStatistics();        
    }
}
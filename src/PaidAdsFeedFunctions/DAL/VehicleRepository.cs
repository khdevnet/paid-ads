using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;

namespace PaidAdsFeedFunctions.DAL
{
    public class VehicleRepository : IVehicleRepository
    {
        private readonly Container _vehicleDetailsContainer;

        public VehicleRepository(Container container)
        {
            _vehicleDetailsContainer = container;
        }

        public async Task<IEnumerable<VehicleFullDetailsEntity>> GetVehicles()
        {
            return await _vehicleDetailsContainer.ReadQueryResultAsync<VehicleFullDetailsEntity>(v => v.Status == VehicleStatus.OnSale && !v.Unavailable);
        }
    }
}

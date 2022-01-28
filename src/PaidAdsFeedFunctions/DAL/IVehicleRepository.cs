using System.Collections.Generic;
using System.Threading.Tasks;

namespace PaidAdsFeedFunctions.DAL
{
    public interface IVehicleRepository
    {
        Task<IEnumerable<VehicleFullDetailsEntity>> GetVehicles();
    }
}

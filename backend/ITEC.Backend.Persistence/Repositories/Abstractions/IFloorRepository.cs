using ITEC.Backend.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITEC.Backend.Persistence.Repositories.Abstractions
{
    public interface IFloorRepository : IRepository<Floor>
    {
        Task<Floor> GetFloorWithDesksById(int floorId);
    }
}

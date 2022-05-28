using ITEC.Backend.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITEC.Backend.Persistence.Repositories.Abstractions
{
    public interface IOfficeRepository : IRepository<Office>
    {
        Task<List<Office>> GetOffices(bool includeFloors);
    }
}

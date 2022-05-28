

using ITEC.Backend.Domain.Models;

namespace ITEC.Backend.Persistence.Repositories.Abstractions
{
    public interface IDeskReservationRepository : IRepository<DeskReservation>
    {
        Task<List<DeskReservation>> GetFutureReservationsForDesk(int deskId);
    }
}

using ITEC.Backend.Application.Shared;

namespace ITEC.Backend.Application.Queries.GetReservationQuery
{
    public class GetReservationQueryResult : BaseCommandResult
    {
        public int? DeskId { get; set; }
        public bool HasReservation { get; set; }
        public int? NumberOfDays { get; set; }
        public DateTime? StartingDate { get; set; }
    }
}
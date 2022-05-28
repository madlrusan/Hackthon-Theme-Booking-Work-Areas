using ITEC.Backend.Domain.Models;
using MediatR;

namespace ITEC.Backend.Application.Queries.GetOfficeByIdQuery
{
    public class GetOfficeByIdQuery : IRequest<Office>
    {
        public int OfficeId { get; set; }
    }
}

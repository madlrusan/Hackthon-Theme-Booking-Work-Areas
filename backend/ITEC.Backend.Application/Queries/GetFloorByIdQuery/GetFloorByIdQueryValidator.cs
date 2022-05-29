using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITEC.Backend.Application.Queries.GetFloorByIdQuery
{
    public class GetFloorByIdQueryValidator : AbstractValidator<GetFloorByIdQuery>
    {
        public GetFloorByIdQueryValidator()
        {
            RuleFor(x => x.FloorId).NotNull().GreaterThan(0)
                .WithMessage("FloorId is null or smaller than 0!");
        }
    }
}

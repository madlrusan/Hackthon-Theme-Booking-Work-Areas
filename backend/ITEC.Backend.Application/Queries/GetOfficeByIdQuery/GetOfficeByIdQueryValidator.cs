using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITEC.Backend.Application.Queries.GetOfficeByIdQuery
{
    public class GetOfficeByIdQueryValidator : AbstractValidator<GetOfficeByIdQuery>
    {
        public GetOfficeByIdQueryValidator()
        {
            RuleFor(p => p.OfficeId)
                .NotNull().GreaterThan(0)
                .WithMessage("OfficeId cannot be null or smaller than 0!");
        }
    }
}

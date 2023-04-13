using System;
using Application.Core.Utilities.DataTransferObjects_DTO_;
using FluentValidation;

namespace Application.Business.ValidationRules.FluentValidation;

public class RemoveValidation: AbstractValidator<RemoveEntityDTO>
{
    public RemoveValidation()
    {
        RuleFor(i => i.Id)
            .NotNull()
            .NotEqual(default(Guid));
    }
}
using Application.Core.AspectOrientedProgramming.Attributes;
using Application.Core.Utilities.DataTransferObjects_DTO_.App;
using FluentValidation;

namespace Application.Business.ValidationRules.FluentValidation;

public class AppValidation : AbstractValidator<AppRequest>
{
    [IgnoreAOP]
    public class AppRequestValidator : AbstractValidator<AppRequest>
    {
        public AppRequestValidator()
        {
            RuleFor(i => i.Name)
                .NotNull()
                .NotEmpty();
        }
    }
}
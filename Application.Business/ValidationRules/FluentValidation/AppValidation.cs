using Application.Core.AspectOrientedProgramming.Attributes;
using Application.Core.Utilities.DataTransferObjects_DTO_.App;
using Application.Core.Utilities.DataTransferObjects_DTO_.Log;
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
    
    [IgnoreAOP]
    public class LogRequestValidator : AbstractValidator<LogRequest>
    {
        public LogRequestValidator()
        {
            RuleFor(i => i.Name)
                .NotNull()
                .NotEmpty();

            RuleFor(i => i.AppId)
                .NotNull();
            
            RuleFor(i => i.IsItFixed)
                .NotNull(); 
            
            RuleFor(i => i.LogDate)
                .NotNull();
        }
    }
}
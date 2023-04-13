using Application.Core.Entities.Concrete;
using FluentValidation;
using Application.Core.AspectOrientedProgramming.Attributes;

namespace Application.Business.ValidationRules.FluentValidation
{
    [IgnoreAOP]
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            
        }
    }
}

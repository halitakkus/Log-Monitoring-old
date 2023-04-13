using FluentValidation;
using Application.Core.AspectOrientedProgramming.Attributes;
using Application.DataAccess.Entities;

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

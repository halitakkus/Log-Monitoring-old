using FluentValidation;
using Application.Core.AspectOrientedProgramming.Attributes;
using Application.Core.Utilities.DataTransferObjects.User;
using Application.DataAccess.Entities;

namespace Application.Business.ValidationRules.FluentValidation
{
    [IgnoreAOP]
    public class UserValidator : AbstractValidator<UserResponse>
    {
        public UserValidator()
        {
            
        }
    }
}

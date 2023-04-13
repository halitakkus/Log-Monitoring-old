using Application.Core.Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using Application.Packages.AOP.Attributes;

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

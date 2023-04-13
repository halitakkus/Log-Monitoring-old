using FluentValidation;
using System;
using Application.Core.AspectOrientedProgramming.Attributes;
using Application.Core.Utilities.DataTransferObjects.RemoteWork;

namespace Application.Business.ValidationRules.FluentValidation
{
    [IgnoreAOP]
    public class RemoteWorkAddListValidator : AbstractValidator<RemoteWorkAddListRequest>
    {
        public RemoteWorkAddListValidator()
        {
            RuleForEach(x => x.RemoteWorks).SetValidator(new RemoteWorkAddValidator());
        }
    }

    [IgnoreAOP]
    public class RemoteWorkAddValidator : AbstractValidator<RemoteWorkAddRequest>
    {
        public RemoteWorkAddValidator()
        {
            RuleFor(n => n.EmployeeId)
                .Must(StringNotDefault)
                .NotEmpty()
                .NotNull();
            RuleFor(n => n.Dates).Must(DateNotDefault)
                .WithMessage("Tarih boş geçilemez.")
                .NotEmpty()
                .NotNull();
            RuleFor(n => n.Description)
                .MaximumLength(150)
                .WithMessage("Açıklama alanı en fazla 150 karakter olabilir.");
        }
        bool DateNotDefault(DateTime[] date)
        {
            var result = true;
            for (int i = 0; i < date.Length; i++)
            {
                if (date[i] == default)
                {
                    result = false;
                }
            }
            return result;
        }

        bool StringNotDefault(string expression)
        {
            if (expression == default)
            {
                return false;
            }
            return true;
        }
    }

    [IgnoreAOP]
    public class RemoteWorkUpdateValidator : AbstractValidator<RemoteWorkUpdateRequest>
    {
        public RemoteWorkUpdateValidator()
        {
            RuleFor(n => n.EmployeeId)
                .Must(StringNotDefault)
                .NotEmpty()
                .NotNull();
            RuleFor(n => n.Dates).Must(DateNotDefault)
                .WithMessage("Tarih boş geçilemez.")
                .NotEmpty()
                .NotNull();
            RuleFor(n => n.Description)
                .MaximumLength(150)
                .WithMessage("Açıklama alanı en fazla 150 karakter olabilir.");
            RuleFor(n => n.Id)
                .NotEmpty()
                .NotNull();
            RuleFor(n => n.TaskId)
                .NotEmpty()
                .NotNull();
        }
        bool DateNotDefault(DateTime[] date)
        {
            var result = true;
            for (int i = 0; i < date.Length; i++)
            {
                if (date[i] == default)
                {
                    result = false;
                }
            }
            return result;
        }

        bool StringNotDefault(string expression)
        {
            if (expression == default)
            {
                return false;
            }
            return true;
        }
    }
}

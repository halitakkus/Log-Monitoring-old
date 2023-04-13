using System.Collections.Generic;
using Application.Business.ValidationRules.FluentValidation;
using Application.Core.AspectOrientedProgramming.Aspects.Validation;
using Application.Core.Utilities.DataTransferObjects_DTO_;
using Application.Core.Utilities.DataTransferObjects_DTO_.App;
using Application.Core.Utilities.Result;

namespace Application.Business.Abstract;

public interface IAppManager
{
    IDataResult<IEnumerable<AppResponse>> GetList();

    [ValidationAspect<IResult>(typeof(AppValidation.AppRequestValidator), Priority = 1)]
    IResult Insert(AppRequest request);

    IResult Remove(RemoveEntityDTO request);
}
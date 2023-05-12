using System;
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
    IDataResult<AppResponse> GetById(Guid id);
    IResult Remove(RemoveEntityDTO request);
    IResult Insert(AppRequest request);
}
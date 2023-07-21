using System;
using System.Collections.Generic;
using Application.Core.Utilities.DataTransferObjects_DTO_;
using Application.Core.Utilities.DataTransferObjects_DTO_.App;
using Application.Core.Utilities.DataTransferObjects_DTO_.Log;
using Application.Core.Utilities.Result;

namespace Application.Business.Abstract;

public interface IAppManager
{
    IDataResult<IEnumerable<AppResponse>> GetList();
    IDataResult<AppResponse> GetById(Guid id);
    IResult Remove(RemoveEntityDTO request);
    IResult Insert(AppRequest request);
    IResult InsertLog(LogRequest request);
    void CheckLog(dynamic request);
}
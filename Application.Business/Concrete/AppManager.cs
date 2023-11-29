using System;
using System.Collections.Generic;
using Application.Business.Abstract;
using Application.Business.ValidationRules.FluentValidation;
using Application.Core.AspectOrientedProgramming.Aspects.Validation;
using Application.Core.Utilities.DataTransferObjects_DTO_;
using Application.Core.Utilities.DataTransferObjects_DTO_.App;
using Application.Core.Utilities.DataTransferObjects_DTO_.Log;
using Application.Core.Utilities.Result;
using Application.DataAccess.Abstract;
using Application.DataAccess.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Application.Business.Concrete;

public class AppManager : IAppManager
{
    private readonly IAppDal _appDal;
    private readonly ILogDal _logDal;
    private readonly IMapper _mapper;

    public AppManager(IAppDal appDal, IMapper mapper, ILogDal logDal)
    {
        _appDal = appDal;
        _mapper = mapper;
        _logDal = logDal;
    }
    
    public IDataResult<IEnumerable<AppResponse>> GetList()
    {
        var apps = _appDal.GetList();

        var appsResponse = _mapper.Map<IEnumerable<AppResponse>>(apps);

        return new SuccessDataResult<IEnumerable<AppResponse>>(appsResponse);
    }

    public IDataResult<AppResponse> GetById(Guid id)
    {
        var app = _appDal.GetById(id);

        var appResponse = _mapper.Map<AppResponse>(app);

        return new SuccessDataResult<AppResponse>(appResponse);
    }

    [ValidationAspect<IResult>(typeof(AppValidation.AppRequestValidator), Priority = 1)]
    public IResult Remove(RemoveEntityDTO request)
    {
        var entity = _appDal.GetById(request.Id);
      
        var check = _appDal.Remove(entity);

        if (!check) return new ErrorResult();
        
        return new SuccessResult();
    }

    public IResult Insert(AppRequest request)
    {
        var app = _mapper.Map<App>(request);

        var check = _appDal.Add(app);

        if (!check)
        {
            return new ErrorResult();
        }

        return new SuccessResult();
    }

    [ValidationAspect<IResult>(typeof(AppValidation.LogRequestValidator), Priority = 1)]
    public IResult InsertLog([FromHeader]LogRequest request)
    {
        var universalTime  = request.LogDate.Date.ToUniversalTime();
        
        var check = _logDal.Find(i => i.LogDate == universalTime && i.Name == request.Name);

        if (check != null)
            return new SuccessResult();
        
        var entity = _mapper.Map<Log>(request);
        entity.LogDate = entity.LogDate.ToUniversalTime();
        
        var result = _logDal.Add(entity);

        if (!result) return new ErrorResult("Log Eklenemedi.");

        return new SuccessResult();
    }

    public void CheckLog(dynamic request)
    {
        var checkAppLogRequest = new CheckAppLogRequest();

        checkAppLogRequest.AppId = (Guid)request.appId;

        var check = _appDal.CheckLog(checkAppLogRequest);

        if (!check)
            return;

        var logs = _appDal.GetAllFixedLogs(checkAppLogRequest);
        
        foreach (var log in logs)
        {
            log.IsItFixed = true;
            _logDal.UpdateAsync(log);
        }
    }
}
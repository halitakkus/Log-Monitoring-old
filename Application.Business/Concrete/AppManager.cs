using System;
using System.Collections.Generic;
using Application.Business.Abstract;
using Application.Core.Utilities.DataTransferObjects_DTO_;
using Application.Core.Utilities.DataTransferObjects_DTO_.App;
using Application.Core.Utilities.Result;
using Application.DataAccess.Abstract;
using AutoMapper;

namespace Application.Business.Concrete;

public class AppManager : IAppManager
{
    private readonly IAppDal _appDal;
    private readonly IMapper _mapper;

    public AppManager(IAppDal appDal, IMapper mapper)
    {
        _appDal = appDal;
        _mapper = mapper;
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

    public IResult Remove(RemoveEntityDTO request)
    {
        var entity = _appDal.GetById(request.Id);
      
        var check = _appDal.Remove(entity);

        if (!check) return new ErrorResult();
        
        return new SuccessResult();
    }
}
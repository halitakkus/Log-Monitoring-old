using System.Collections.Generic;
using Application.Business.Abstract;
using Application.Core.Utilities.DataTransferObjects_DTO_;
using Application.Core.Utilities.DataTransferObjects_DTO_.App;
using Application.Core.Utilities.Result;
using Application.DataAccess.Abstract;
using Application.DataAccess.Entities;
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

    public IResult Insert(AppRequest request)
    {
        var app = _mapper.Map<App>(request);
        var check = _appDal.Add(app);

        if (!check) return new ErrorResult();
        
        return new SuccessResult();
    }
    
    public IResult Remove(RemoveEntityDTO request)
    {
        var entity = _appDal.GetById(request.Id);
      
        var check = _appDal.Remove(entity);

        if (!check) return new ErrorResult();
        
        return new SuccessResult();
    }
}
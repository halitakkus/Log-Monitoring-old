using Application.Business.Abstract;
using Application.Core.Utilities.Result;
using Application.DataAccess.Abstract;
using AutoMapper;
using System.Collections.Generic;
using Application.Core.AspectOrientedProgramming.Aspects.Caching;
using Application.Core.Utilities.DataTransferObjects.Setting;

namespace Application.Business.Concrete
{
    public class SettingManager : ISettingManager
    {
        private readonly ISettingDal _settingDal;
        private readonly IMapper _mapper;
        public SettingManager(ISettingDal settingDal, IMapper mapper)
        {
            _settingDal = settingDal;
            _mapper = mapper;
        }
        
        [CacheAspect]
        public IDataResult<IEnumerable<SettingResponse>> GetList()
        {
            var data = _settingDal.GetList();
            var mappedResponse = _mapper.Map<IEnumerable<SettingResponse>>(data);
            return new SuccessDataResult<IEnumerable<SettingResponse>>(mappedResponse);
        }
    }
}

using Application.Core.Utilities.Result;
using System.Collections.Generic;
using Application.Core.AspectOrientedProgramming.Aspects.Caching;
using Application.Core.Utilities.DataTransferObjects.Setting;

namespace Application.Business.Abstract
{
    public interface ISettingManager
    {
        /// <summary>
        /// Gets the list of all entity objects.
        /// </summary>
        /// <returns></returns>
        [CacheAspect]
        IDataResult<IEnumerable<SettingResponse>> GetList();
    } 
}

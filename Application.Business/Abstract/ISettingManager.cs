using Application.Core.Utilities.Result;
using Application.Packages.AOP.Aspects.Caching;
using System.Collections.Generic;
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

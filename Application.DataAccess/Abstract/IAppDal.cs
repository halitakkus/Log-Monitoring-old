using System;
using System.Collections.Generic;
using Application.Core.Utilities.DataTransferObjects_DTO_.App;
using Application.DataAccess.Abstract.Repository;
using Application.DataAccess.Entities;

namespace Application.DataAccess.Abstract;

public interface IAppDal: IRepository<App, Guid>
{
    bool CheckLog(CheckAppLogRequest checkAppLogRequest);

    IEnumerable<Log> GetAllFixedLogs(CheckAppLogRequest checkAppLogRequest);
}
using System;
using System.Collections.Generic;
using System.Linq;
using Application.Core.Configuration.Context;
using Application.Core.Utilities.DataTransferObjects_DTO_.App;
using Application.DataAccess.Abstract;
using Application.DataAccess.Concrete.EntityFramework.Context;
using Application.DataAccess.Concrete.EntityFramework.Repository;
using Application.DataAccess.Entities;

namespace Application.DataAccess.Concrete.EntityFramework;

public class AppDal: EfRepositoryBase<App, Guid>, IAppDal
{
    private readonly IApplicationConfigurationContext _configurationContext;
    public AppDal(IApplicationConfigurationContext context) : base(context.ConnectionString) { }
    public bool CheckLog(CheckAppLogRequest checkAppLogRequest)
    {
        using (var context = new ApplicationDbContext(_configurationContext.ConnectionString))
        {
            return context.Logs.Any(
                i => i.AppId == checkAppLogRequest.AppId);
        }
    }
    
    public IEnumerable<Log> GetAllFixedLogs(CheckAppLogRequest checkAppLogRequest)
    {
        using (var context = new ApplicationDbContext(_configurationContext.ConnectionString))
        {
            return context.Logs.Where(
                i => i.AppId == checkAppLogRequest.AppId).ToList();
        }
    }
}
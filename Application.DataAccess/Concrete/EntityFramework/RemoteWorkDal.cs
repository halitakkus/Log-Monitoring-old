using Application.DataAccess.Abstract;
using Application.DataAccess.Concrete.EntityFramework.Context;
using Application.DataAccess.Concrete.EntityFramework.Repository;
using Application.DataAccess.Entities;
using Application.DataAccess.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Application.Core.Configuration.Context;

namespace Application.DataAccess.Concrete.EntityFramework
{
    public class RemoteWorkDal : EfRepositoryBase<RemoteWork, Guid>, IRemoteWorkDal
    {
        private readonly IApplicationConfigurationContext _configurationContext;

        public RemoteWorkDal(IApplicationConfigurationContext context) : base(context.ConnectionString)
        {
            _configurationContext = context;
        }

        public List<RemoteWork> GetListByEmployeeId(List<string> employeeIds)
        {
            using (var context = new ApplicationDbContext(_configurationContext.ConnectionString))
            {
                return context.Set<RemoteWork>()
                    .Include(d => d.Days)
                    .Where(w=>employeeIds.Contains(w.EmployeeId))
                    .ToList();
            }
        }
    }
}

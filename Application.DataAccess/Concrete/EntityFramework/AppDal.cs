using System;
using Application.Core.Configuration.Context;
using Application.DataAccess.Abstract;
using Application.DataAccess.Concrete.EntityFramework.Repository;
using Application.DataAccess.Entities;

namespace Application.DataAccess.Concrete.EntityFramework;

public class AppDal: EfRepositoryBase<App, Guid>, IAppDal
{
    private readonly IApplicationConfigurationContext _configurationContext;
    public AppDal(IApplicationConfigurationContext context) : base(context.ConnectionString) { }
}
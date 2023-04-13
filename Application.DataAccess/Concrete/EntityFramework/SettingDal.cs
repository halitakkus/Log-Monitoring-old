using Application.DataAccess.Abstract;
using Application.DataAccess.Concrete.EntityFramework.Repository;
using Application.DataAccess.Entities;
using System;
using Application.Core.Configuration.Context;

namespace Application.DataAccess.Concrete.EntityFramework
{
   
    public class SettingDal : EfRepositoryBase<Setting, Guid>, ISettingDal
    {
        private readonly IApplicationConfigurationContext _configurationContext;
        public SettingDal(IApplicationConfigurationContext context) : base(context.ConnectionString) { }
    }
}

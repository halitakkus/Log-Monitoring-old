using Application.DataAccess.Abstract.Repository;
using Application.DataAccess.Entities;
using System;
using System.Collections.Generic;

namespace Application.DataAccess.Abstract
{
    public interface IRemoteWorkDal : IRepository<RemoteWork, Guid>
    {
        List<RemoteWork> GetListByEmployeeId(List<string> employeeIds);
    }
}

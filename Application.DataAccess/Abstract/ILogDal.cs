using System;
using System.Collections.Generic;
using Application.Core.Utilities.DataTransferObjects_DTO_.Log;
using Application.DataAccess.Abstract.Repository;
using Application.DataAccess.Entities;

namespace Application.DataAccess.Abstract;

public interface ILogDal: IRepository<Log, Guid>
{
    IEnumerable<StatisticColumnChartResponse> StatisticColumnChart(Guid appId);
}
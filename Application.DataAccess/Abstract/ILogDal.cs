using System;
using Application.Core.Utilities.DataTransferObjects_DTO_.Log;
using Application.DataAccess.Abstract.Repository;
using Application.DataAccess.Entities;

namespace Application.DataAccess.Abstract;

public interface ILogDal: IRepository<Log, Guid>
{
    StatisticColumnChartResponse StatisticColumnChart(Guid appId);
}
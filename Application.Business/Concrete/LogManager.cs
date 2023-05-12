using System;
using Application.Business.Abstract;
using Application.Core.Utilities.DataTransferObjects_DTO_.Log;
using Application.Core.Utilities.Result;
using Application.DataAccess.Abstract;

namespace Application.Business.Concrete;

public class LogManager : ILogManager
{
    private readonly ILogDal _logDal;
    public LogManager(ILogDal logDal)
    {
        _logDal = logDal;
    }
    
    public IDataResult<StatisticColumnChartResponse> StatisticColumnChart(Guid appId)
    {
        var columnChartResponse = _logDal.StatisticColumnChart(appId);

        if (columnChartResponse is null)
        {
            return new ErrorDataResult<StatisticColumnChartResponse>("NotFound");
        }

        return new SuccessDataResult<StatisticColumnChartResponse>(columnChartResponse);
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using Application.Business.Abstract;
using Application.Core.AspectOrientedProgramming.Aspects.Exception;
using Application.Core.Utilities.DataTransferObjects_DTO_.Log;
using Application.Core.Utilities.Result;
using Application.DataAccess.Abstract;
using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Office2010.ExcelAc;

namespace Application.Business.Concrete;

public class LogManager : ILogManager
{
    private readonly ILogDal _logDal;
    public LogManager(ILogDal logDal)
    {
        _logDal = logDal;
    }
    
    [ExceptionAspect]
    public IDataResult<StatisticColumnChartResponse> StatisticColumnChart(Guid appId)
    {
        var columnChartResponse = _logDal.StatisticColumnChart(appId);

        if (columnChartResponse is null)
            return new ErrorDataResult<StatisticColumnChartResponse>("NotFound");

        if(columnChartResponse.Count() <= 1)
            return new SuccessDataResult<StatisticColumnChartResponse>(columnChartResponse.FirstOrDefault());
        
        var columnChartModel = new List<StatisticColumnChartModel>();
        var response = new StatisticColumnChartResponse();
        
        var chart = columnChartResponse.FirstOrDefault();

        response.AppId = chart.AppId;
        response.AppName = chart.AppName;
        
        columnChartResponse.ToList().ForEach(chart =>
        {
            columnChartModel.AddRange(chart.ColumnChartModels);
        });

        response.ColumnChartModels = columnChartModel;

        return new SuccessDataResult<StatisticColumnChartResponse>(response);
    }

    public IDataResult<IEnumerable<LogResponse>> GetLogs(Guid appId)
    {
        var result = _logDal.GetLogs(appId);

        return new SuccessDataResult<IEnumerable<LogResponse>>(result);
    }
}
using System;
using System.Collections.Generic;
using Application.Core.Utilities.DataTransferObjects_DTO_.Log;
using Application.Core.Utilities.Result;

namespace Application.Business.Abstract;

public interface ILogManager {
    IDataResult<StatisticColumnChartResponse> StatisticColumnChart(Guid appId);
    IDataResult<LogResponse> GetLogs(Guid appId);
}
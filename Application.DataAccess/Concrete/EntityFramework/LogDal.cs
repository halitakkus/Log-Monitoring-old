﻿using System;
using System.Collections.Generic;
using System.Linq;
using Application.Core.Configuration.Context;
using Application.Core.Utilities.DataTransferObjects_DTO_.Log;
using Application.DataAccess.Abstract;
using Application.DataAccess.Concrete.EntityFramework.Context;
using Application.DataAccess.Concrete.EntityFramework.Repository;
using Application.DataAccess.Entities;

namespace Application.DataAccess.Concrete.EntityFramework;

public class LogDal : EfRepositoryBase<Log, Guid>, ILogDal
{
    private readonly IApplicationConfigurationContext _configurationContext;

    public LogDal(IApplicationConfigurationContext context) : base(context.ConnectionString)
    {
    }

    public StatisticsTotalLevelInfo StatisticsTotalLevelInfo()
    {
        using (var context = new ApplicationDbContext(_configurationContext.ConnectionString))
        {
            var statisticColumnCharts = context.Logs
                .GroupBy(e => new { e.AppId, e.Name })
                .Select(g => new StatisticsTotalLevelInfo
                {
                    AppId = g.Key.AppId,
                    AppName = g.Key.Name,
                    StatisticTotalLevelModels = g.GroupBy(e => e.Level)
                        .Select(gg => new StatisticTotalLevelModel
                        {
                            TotalLogCount = g.Count(),
                            Statistics = gg.GroupBy(e => e.Level)
                                .OrderBy(ggg => ggg.Key)
                                .Select(ggg => new KeyValuePair<string, int>(ggg.Key.ToString(), ggg.Count()))
                        })
                });

            return statisticColumnCharts.FirstOrDefault();
        }
    }
    
    public StatisticColumnChartResponse StatisticColumnChart(Guid appId)
    {
        using (var context = new ApplicationDbContext(_configurationContext.ConnectionString))
        {
            var statisticColumnCharts = context.Logs
                .Where(i=> i.AppId == appId)
                .GroupBy(e => new { e.AppId, e.Name, Month = new DateTime(e.LogDate.Year, e.LogDate.Month, 1) })
                .Select(g => new StatisticColumnChartResponse
                {
                    AppId = g.Key.AppId,
                    AppName = g.Key.Name,
                    ColumnChartModels = g.GroupBy(e => e.Level)
                        .Select(gg => new StatisticColumnChartModel
                        {
                            DateMon = g.Key.Month,
                            Statistics = gg.GroupBy(e => e.LogDate.Month)
                                .OrderBy(ggg => ggg.Key)
                                .Select(ggg => new KeyValuePair<string, int>(ggg.Key.ToString(), ggg.Count()))
                        })
                });

            return statisticColumnCharts.FirstOrDefault();
        }
    }
}
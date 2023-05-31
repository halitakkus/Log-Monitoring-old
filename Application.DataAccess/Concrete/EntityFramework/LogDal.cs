using System;
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
        _configurationContext = context;
    }

    public IEnumerable<LogResponse> GetLogs(Guid appId)
    {
        using (var context = new ApplicationDbContext(_configurationContext.ConnectionString))
        {
            var logs = context.Logs.Take(7);

            if (appId != default)
            {
                logs = logs.Where(i => i.AppId == appId);
            }

            return logs.Select(i => new LogResponse
            {
                AppId = i.AppId,
                Name = i.Name,
                LogDate = i.LogDate,
                Level = i.Level,
                Content = i.Content,
                UserId = i.UserId,
                IsItFixed = i.IsItFixed,
                ServerIp = i.ServerIp,
                ServerName = i.ServerName
            }).ToList();
        }
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
    
    public IEnumerable<StatisticColumnChartResponse> StatisticColumnChart(Guid appId)
    {
        using (var context = new ApplicationDbContext(_configurationContext.ConnectionString))
        {
            var statisticColumnChart = context.Logs
                .GroupBy(e => new { e.App.Id, e.App.Name, Month = new DateTime(DateTime.Now.Year, e.LogDate.Month, e.LogDate.Day) })
                .Select(g => new StatisticColumnChartResponse
                {
                    AppId = g.Key.Id,
                    AppName = g.Key.Name,
                    ColumnChartModels = g.GroupBy(e => e.LogDate)
                        .Select(gg => new StatisticColumnChartModel
                        { 
                            DateMon = g.Key.Month,
                            Statistics = gg.GroupBy(e => e.Level)
                                .OrderBy(ggg => ggg.Key)
                                .Select(ggg => new KeyValuePair<string, int>(ggg.Key.ToString(), ggg.Count()))
                        })
                });

            if (appId != default)
            {
                statisticColumnChart = statisticColumnChart.Where(i => i.AppId == appId);
            }

            return statisticColumnChart.ToList();
        }
    }
}
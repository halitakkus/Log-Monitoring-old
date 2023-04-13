using Application.Business.Concrete;
using Application.Packages.RabbitMQ.Subscriber;
using Application.WebAPI.MQTT.Subscribers.Concrate;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Application.WebAPI.MQTT.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddSubscribers(this IServiceCollection services)
    {
        services.AddSingleton<RemoteWorkBpmSubscriber>();
    }

    public static void UseSubscribers(this IApplicationBuilder app)
    {
        var rabbitMQSubscriberService = app.ApplicationServices.GetService<IRabbitMQSubscriberService>();

        #region Process Subscriber
        var instanceService = app.ApplicationServices.GetRequiredService<RemoteWorkBpmSubscriber>();
        rabbitMQSubscriberService.Regiser($"bpm_create_process_response_", instanceService.CreateProcess);
        rabbitMQSubscriberService.Regiser($"bpm_complete_task_response_", instanceService.TaskComplete);
        #endregion
    }
}
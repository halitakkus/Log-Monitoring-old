using Application.Business.Abstract;
using Application.WebAPI.MQTT.Subscribers.Abstract;
using Newtonsoft.Json;
using RabbitMQ.Client.Events;
using System.Text;

namespace Application.WebAPI.MQTT.Subscribers.Concrate;

public class RemoteWorkBpmSubscriber : ISubscriber
{
    private readonly IRemoteWorkManager _remoteWorkManager;

    public RemoteWorkBpmSubscriber(IRemoteWorkManager remoteWorkManager)
    {
        _remoteWorkManager = remoteWorkManager;
    }
    public bool CreateProcess(BasicDeliverEventArgs message)
    {
        return true;
    }
    public bool TaskComplete(BasicDeliverEventArgs message)
    {
        return true;
    }
}
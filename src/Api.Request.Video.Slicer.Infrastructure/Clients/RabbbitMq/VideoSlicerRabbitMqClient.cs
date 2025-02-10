using Api.Request.Video.Slicer.Domain;
using Api.Request.Video.Slicer.Domain.ValueObjects;
using Api.Request.Video.Slicer.Infrastucture.Clients.Domain;
using RabbitMQ.Client;


namespace Infrastructure.Clients.RabbbitMq;

public class VideoSlicerRabbitMqClient :  RabbitMQPublisher<VideoRequest>, IVideoSlicerClient
{
    public const string QueueName = "SendVideoRequest";
    public VideoSlicerRabbitMqClient(IConnectionFactory factory) : base(factory, QueueName)
    {

    }

    public async Task SendAsync(VideoRequest videoRequest)
    {
        var dto = new SendVideoRequest
        {
            StorageFile = videoRequest.Video,
            RequestId = videoRequest.Id.ToString(),
        };

        await PublishMessageAsync(videoRequest);
        
    }
}

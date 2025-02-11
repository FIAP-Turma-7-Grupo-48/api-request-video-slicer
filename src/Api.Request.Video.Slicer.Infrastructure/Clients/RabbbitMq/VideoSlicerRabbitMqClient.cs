using Api.Request.Video.Slicer.Domain;
using Api.Request.Video.Slicer.Domain.ValueObjects;
using Api.Request.Video.Slicer.Infrastucture.Clients.Domain;
using RabbitMQ.Client;


namespace Infrastructure.Clients.RabbbitMq;

public class VideoSlicerRabbitMqClient :  RabbitMQPublisher<VideoRequest>, IVideoSlicerClient
{
    public const string QueueName = "RequestVideoSlicer";
    public VideoSlicerRabbitMqClient(IConnectionFactory factory) : base(factory, QueueName)
    {

    }

    public async Task SendAsync(VideoRequest videoRequest)
    {
        var dto = new SendVideoRequest
        {

            RequestId = videoRequest.Id,
            StorageFile = new()
            {
                Key = videoRequest.Id.ToString(),
                FileName = videoRequest.FileName,
                ContentType = "video/mp4"
            }
        };

        await PublishMessageAsync(videoRequest);
        
    }
}

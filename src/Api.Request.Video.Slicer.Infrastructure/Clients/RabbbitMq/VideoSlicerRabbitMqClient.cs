using Api.Request.Video.Slicer.Domain;
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
            fileName = videoRequest.fileName,
            videoUrl = videoRequest.videoUrl,
            id = videoRequest.id.ToString(),
        };

        await PublishMessageAsync(videoRequest);
        
    }
}

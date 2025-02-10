using Api.Request.Video.Slicer.Controller.Application.Interfaces;
using Api.Request.Video.Slicer.UseCase.Dtos;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace api_request_video_slicer.BackgroundServices
{
    public class RabbitMqWorker : BackgroundService
    {
        private readonly IConnectionFactory _connectionFactory;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        private readonly JsonSerializerOptions _options = new()
        {
            PropertyNameCaseInsensitive = true
        };

        public RabbitMqWorker(IServiceScopeFactory serviceScopeFactory, IConnectionFactory factory)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _connectionFactory = factory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using var connection = await _connectionFactory.CreateConnectionAsync();
                using var channel = await connection.CreateChannelAsync();

                await channel.QueueDeclareAsync(queue: "SendToProduction", durable: true, exclusive: false, autoDelete: false,
                    arguments: null);

                Console.WriteLine(" [*] Waiting for messages.");

                var consumer = new AsyncEventingBasicConsumer(channel);
                consumer.ReceivedAsync += (model, ea) =>
                {
                    try
                    {
                        var body = ea.Body.ToArray();
                        var messageJson = Encoding.UTF8.GetString(body);

                        return ProcessMessage(messageJson, stoppingToken);
                    }
                    catch (Exception)
                    {

                        throw;
                    }

                };

                await channel.BasicConsumeAsync("SendToProduction", autoAck: true, consumer: consumer);
            }
        }

        private async Task ProcessMessage(string json, CancellationToken cancellationToken)
        {
            var message = JsonSerializer.Deserialize<UpdateVideoRequestStatus>(json, _options);

            using (IServiceScope scope = _serviceScopeFactory.CreateScope())
            {
                IVideoRequestApplication videoRequestApplication = scope.ServiceProvider.GetRequiredService<IVideoRequestApplication>();

                await videoRequestApplication.UpdateAsync(message);
            }
        }
    }
}

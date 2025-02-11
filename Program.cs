using api.request.video.Extensions;
using api_request_video_slicer.BackgroundServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using RabbitMQ.Client;
using System.Text;
using WebApi.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddUseCase();
builder.Services.AddControllerLayerDI();
builder.Services.AddInfrastructure(builder.Configuration);
AddRabbitMqConnectionFactory(builder.Services);
builder.Services.AddHostedService<RabbitMqWorker>();


builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("JwtKey"))),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Request Video Slicer", Version = "v1" });
});

builder.Services.Configure<FormOptions>(options =>
{
    options.MultipartBodyLengthLimit = long.MaxValue;
});
builder.Services.Configure<KestrelServerOptions>(options =>
{
    options.Limits.MaxRequestBodySize = long.MaxValue;
});

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Request Video Slicer");
    
});

app.UseAuthorization();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();


IServiceCollection AddRabbitMqConnectionFactory(IServiceCollection services)
{
    var hostName = Environment.GetEnvironmentVariable("RabbitMqHostName");
    var port = int.Parse(Environment.GetEnvironmentVariable("RabbitMqPort"));
    var user = Environment.GetEnvironmentVariable("RabbitMqUserName");
    var password = Environment.GetEnvironmentVariable("RabbitMqPassword");

    return
        services
            .AddSingleton<IConnectionFactory>(
                new ConnectionFactory()
                {
                    HostName = hostName,
                    Port = port,
                    UserName = user,
                    Password = password
                }
            );
}

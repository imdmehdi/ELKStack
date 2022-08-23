using Serilog;
using Serilog.Configuration;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
builder.Host.UseSerilog();

var app = builder.Build();

// Configure the HTTP request pipeline.
Log.Logger = CreateSerilogLogger(app.Configuration);
Serilog.ILogger CreateSerilogLogger(IConfiguration configuration)
{
    var seqServerUrl = configuration["Serilog:SeqServerUrl"];
    var logstashUrl = configuration["Serilog:LogstashgUrl"];
    return new LoggerConfiguration()
        .MinimumLevel.Verbose()
        .Enrich.WithProperty("ApplicationContext", app.Environment.ApplicationName)
        .Enrich.FromLogContext()
        .WriteTo.Console()
        .WriteTo.Seq(string.IsNullOrWhiteSpace(seqServerUrl) ? "http://seq" : seqServerUrl)
        .WriteTo.DurableHttpUsingFileSizeRolledBuffers(string.IsNullOrWhiteSpace(logstashUrl) ? "http://logstash:8080" : logstashUrl, string.Format("BufferLogs/text-{0:yyyy-MM-dd}.txt",
            DateTime.Now))
        .ReadFrom.Configuration(configuration)
        .CreateLogger();
}
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

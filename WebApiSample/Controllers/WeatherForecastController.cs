using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace WebApiSample.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            //string Hi=" Imam";
            var obj = Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
            //string jsonString = JsonSerializer.Serialize(obj);
            _logger.LogInformation("hi, Random Value is Mehdi {mes},{toshow}", JsonSerializer.Serialize(obj),"yes");
            _logger.LogInformation("hipol, Random Value is Mehdi {mes},{toshow}", JsonSerializer.Serialize(obj), "no");


            try
            {
               throw new Exception();
            }
            catch(Exception e)
            {
                _logger.LogCritical(e.Message+"- " +e.StackTrace.ToString());

            }
            return obj;
        }
    }
}
using Examination_System.Data;
using Examination_System.Models;
using Microsoft.AspNetCore.Mvc;

namespace Examination_System.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private readonly Context _context;
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
        _context = new Context();
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
    }

    [HttpPost]
    public bool DoWhatever()
    {
        Instructor instructor = new Instructor
        {
            Username = "instructor1",
            Email = "Any Of Any",
            PasswordHash = "hashedpassword",
            Role = UserRole.Instructor,
        };
        _context.Users.Add(instructor);
        _context.SaveChanges();
        return true;
    }
    
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace MyApi.Controllers
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
        private readonly UserManager<MyUser> userManager;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, UserManager<MyUser> userManager)
        {
            _logger = logger;
            this.userManager = userManager;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        [Authorize(Roles = RoleConstants.Athlete)]
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

        [Authorize]
        [HttpPost]
        public async Task RegisterAsAthlete()
        { 
            var user = this.HttpContext.User?.Identity?.Name ?? string.Empty;
            var loggedInUser = await this.userManager.FindByEmailAsync(user);
            
            var result = await this.userManager.AddToRoleAsync(loggedInUser, RoleConstants.Athlete);
        }
    }
}

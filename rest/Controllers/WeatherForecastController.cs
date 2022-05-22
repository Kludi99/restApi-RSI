using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using RiskFirst.Hateoas;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;

namespace rest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
      

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IWeatherForeacastService _weatherForeacastService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IWeatherForeacastService weatherForeacastService)
        {
            _logger = logger;
            _weatherForeacastService = weatherForeacastService;
           // _httpContextAccessor = httpContextAccessor;

        }

        [HttpGet]
        [Produces("application/xml", "application/json")]
        public Dictionary<int, WeatherForecast> Get()
        {
            return _weatherForeacastService.GetAll();

        }
        [HttpGet]
        [Route("/WeatherForecast/{id}")]
        public WeatherForecast GetWeatherForecast(int id)
        {
            //return WeatherForecasts.FirstOrDefault(x => x.Key == id).Value;
            return _weatherForeacastService.GetById(id);
        }
        
        [HttpGet]
        [Route("/WeatherForecast/{id}/comments")]
        public List<string> GetWeatherForecastComments(int id)
        {
            //return WeatherForecasts.FirstOrDefault(x => x.Key == id).Value;
            return _weatherForeacastService.GetComments(id);
        }
        //[HttpGet]
        //public List<string> GetWeatherComments(int weatherId)
        //{
        //    return _weatherForeacastService.GetComments(weatherId);
        //}
        [HttpPost]
        public WeatherForecast CreateWeatherForecast(WeatherForecast weatherForecast)
        {
            var uri = HttpContext.Request.Path;
            var test = HttpContext.Request.GetDisplayUrl();
            //var x = HttpContext.RequestS
           // var x = _httpContextAccessor.HttpContext.Request.PathBase;

            //string url = HttpContext.Request.Url;
           // string url = Current.Request.Url.AbsoluteUri;
            var weather = _weatherForeacastService.CreateWeather(weatherForecast);
            //var paramsObj = new
            //{
            //    Body = weather,
            //    //Query = query,
            //    Header = uri,
            //    Uri = uri

            //};
            HttpContext.Response.Headers.Add("Location", uri.ToString());
           // var resonse = new Response
            return weather;
        }
        [HttpPut]
        public WeatherForecast UpdateWeatherForecast(WeatherForecast weatherForecast)
        {
            return _weatherForeacastService.UpdateWeather(weatherForecast);
        }
        [HttpDelete]
        public void DeleteWeatherForecast(int id)
        {
             _weatherForeacastService.DeleteWeather(id);
        }
        [HttpPost]
        [Route("/params")]
        public IActionResult Params([FromBody] string body, [FromQuery] string query, [FromHeader] string header)
        {
            var uri = ControllerContext.HttpContext.Request.Path;
            var paramsObj = new
            {
                Body = body,
                Query = query,
                Header = header,
                Uri = uri

            };
            return Ok(paramsObj);
        }
        [HttpGet]
        [Route("/hello")]
        public string  Hello()
        {
            return "Hello Rest";

        }

        [HttpGet]
        [Route("/echo")]
        public string Echo()
        {
            return "Witaj echo";
        }
        [HttpGet]
        [Route("/echo/{param}")]
        public string Echo(int param)
        {
            return "Witaj echo " + param;
        }

    }
}

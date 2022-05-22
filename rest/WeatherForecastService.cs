using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace rest
{
    public class WeatherForecastService : IWeatherForeacastService
    {
        public WeatherForecastService()
        {
            WeatherForecasts = CreateList();
        }

        private static readonly string[] Summaries = new[]
      {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };
        private static readonly string[] Comments = new[]
     {
            "Comment", "Comment2", "Comment3", "New comment", "Mild", "Warm", "Balmy", "Hot", "Sweltering"
        };
        private Dictionary<int, WeatherForecast> WeatherForecasts { get; set; }

        public Dictionary<int, WeatherForecast>  GetAll()
        {
            return WeatherForecasts;
        }
        public WeatherForecast GetById (int id)
        {
            return WeatherForecasts.FirstOrDefault(x => x.Key == id).Value;

        }
        public WeatherForecast CreateWeather(WeatherForecast weatherForecast)
        {
            WeatherForecasts.Add(SetId(weatherForecast), weatherForecast);
            return weatherForecast;
        }
        public WeatherForecast UpdateWeather(WeatherForecast weatherForecast)
        {
            var weather = WeatherForecasts.FirstOrDefault(x => x.Key == weatherForecast.Id).Value;
            WeatherForecasts[weather.Id] = weatherForecast;
            return weatherForecast;
        }
        public void DeleteWeather(int id)
        {
            var weather = WeatherForecasts.FirstOrDefault(x => x.Key == id).Value;
            WeatherForecasts.Remove(id);

        }
        public List<string> GetComments(int id)
        {
            var weather = WeatherForecasts.FirstOrDefault(x => x.Key == id).Value;
            return weather.Comments;
        }
        private Dictionary<int, WeatherForecast> CreateList()
        {
            var rng = new Random();
            var list = new Dictionary<int, WeatherForecast>();
            var index = 0;
            foreach (var item in Summaries)
            {

                var foreacast = new WeatherForecast()
                {
                    Id = index,
                    Date = DateTime.Now.AddDays(index),
                    TemperatureC = rng.Next(-20, 55),
                    Summary = Summaries[rng.Next(Summaries.Length)],
                    Comments = Comments.ToList()
                };

                list.Add(index, foreacast);
                index++;
            }
            return list;
        }
        private int SetId(WeatherForecast wf)
        {
            var id = WeatherForecasts.Count();
            wf.Id = id;
            return id;
        }
    }
}

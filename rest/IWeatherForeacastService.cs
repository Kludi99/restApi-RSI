using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace rest
{
    public interface IWeatherForeacastService
    {
        Dictionary<int, WeatherForecast> GetAll();
        WeatherForecast GetById(int id);
        WeatherForecast CreateWeather(WeatherForecast weatherForecast);
        WeatherForecast UpdateWeather(WeatherForecast weatherForecast);
        void DeleteWeather(int id);
        List<string> GetComments(int id);


    }
}

using System.Net.Http.Json;

namespace TravelMate.Services;

public class WeatherService
{
    private readonly HttpClient _httpClient;

    public WeatherService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<WeatherResponse?> GetWeatherAsync(
        double latitude, double longitude)
    {
        string url =
            $"https://api.open-meteo.com/v1/forecast" +
            $"?latitude={latitude.ToString(System.Globalization.CultureInfo.InvariantCulture)}" +
            $"&longitude={longitude.ToString(System.Globalization.CultureInfo.InvariantCulture)}" +
            $"&current=temperature_2m,relative_humidity_2m,wind_speed_10m";

        return await _httpClient.GetFromJsonAsync<WeatherResponse>(url);
    }
}

public class WeatherResponse
{
    public CurrentWeather? Current { get; set; }
}

public class CurrentWeather
{
    public double Temperature_2m { get; set; }
    public int Relative_humidity_2m { get; set; }
    public double Wind_speed_10m { get; set; }
}
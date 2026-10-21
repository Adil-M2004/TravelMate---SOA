using System.Net.Http.Json;

namespace TravelMate.Services;

public class WeatherService
{
    private readonly HttpClient _httpClient;

    // HttpClient is supplied by dependency injection for the weather request.
    public WeatherService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<WeatherResponse?> GetWeatherAsync(
        double latitude, double longitude)
    {
        // Ask Open-Meteo for the current weather at the selected coordinates.
        // InvariantCulture prevents commas from being used as decimal separators.
        string url =
            $"https://api.open-meteo.com/v1/forecast" +
            $"?latitude={latitude.ToString(System.Globalization.CultureInfo.InvariantCulture)}" +
            $"&longitude={longitude.ToString(System.Globalization.CultureInfo.InvariantCulture)}" +
            $"&current=temperature_2m,relative_humidity_2m,wind_speed_10m";

        // Deserialize the JSON response into the model used by the components.
        return await _httpClient.GetFromJsonAsync<WeatherResponse>(url);
    }
}

public class WeatherResponse
{
    // The API groups current measurements inside the "current" property.
    public CurrentWeather? Current { get; set; }
}

public class CurrentWeather
{
    // These names match the current-weather fields returned by Open-Meteo.
    public double Temperature_2m { get; set; }
    public int Relative_humidity_2m { get; set; }
    public double Wind_speed_10m { get; set; }
}
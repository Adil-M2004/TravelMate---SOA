
using System.Net.Http.Json;
using System.Text.Json.Serialization;

namespace TravelMate.Services;

public class ForecastService
{
    private readonly HttpClient _httpClient;

    public ForecastService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<DailyForecast>> GetForecastAsync(
        double latitude, double longitude)
    {
        string url =
            "https://api.open-meteo.com/v1/forecast" +
            $"?latitude={latitude.ToString(System.Globalization.CultureInfo.InvariantCulture)}" +
            $"&longitude={longitude.ToString(System.Globalization.CultureInfo.InvariantCulture)}" +
            "&daily=temperature_2m_max,temperature_2m_min," +
            "precipitation_probability_max,precipitation_sum,weather_code" +
            "&timezone=auto&forecast_days=7";

        var response = await _httpClient
            .GetFromJsonAsync<ForecastResponse>(url);

        if (response?.Daily == null)
            return new List<DailyForecast>();

        var daily = response.Daily;
        var forecasts = new List<DailyForecast>();

        for (int i = 0; i < daily.Time.Count; i++)
        {
            forecasts.Add(new DailyForecast
            {
                Date = DateTime.ParseExact(
                    daily.Time[i], "yyyy-MM-dd",
                    System.Globalization.CultureInfo.InvariantCulture),

                MaxTemperature = daily.MaxTemperature[i],
                MinTemperature = daily.MinTemperature[i],
                RainProbability = daily.RainProbability[i],
                Precipitation = daily.Precipitation[i],
                WeatherCode = daily.WeatherCode[i]
            });
        }

        return forecasts;
    }
}

public class ForecastResponse
{
    [JsonPropertyName("daily")]
    public DailyForecastData? Daily { get; set; }
}

public class DailyForecastData
{
    [JsonPropertyName("time")]
    public List<string> Time { get; set; } = new();

    [JsonPropertyName("temperature_2m_max")]
    public List<double> MaxTemperature { get; set; } = new();

    [JsonPropertyName("temperature_2m_min")]
    public List<double> MinTemperature { get; set; } = new();

    [JsonPropertyName("precipitation_probability_max")]
    public List<int?> RainProbability { get; set; } = new();

    [JsonPropertyName("precipitation_sum")]
    public List<double?> Precipitation { get; set; } = new();

    [JsonPropertyName("weather_code")]
    public List<int> WeatherCode { get; set; } = new();
}

public class DailyForecast
{
    public DateTime Date { get; set; }
    public double MaxTemperature { get; set; }
    public double MinTemperature { get; set; }
    public int? RainProbability { get; set; }
    public double? Precipitation { get; set; }
    public int WeatherCode { get; set; }

    public string Condition => WeatherCode switch
    {
        0 => "Clear sky",
        1 or 2 => "Partly cloudy",
        3 => "Overcast",
        45 or 48 => "Fog",
        51 or 53 or 55 => "Drizzle",
        56 or 57 => "Freezing drizzle",
        61 or 63 or 65 => "Rain",
        66 or 67 => "Freezing rain",
        71 or 73 or 75 or 77 => "Snow",
        80 or 81 or 82 => "Rain showers",
        85 or 86 => "Snow showers",
        95 or 96 or 99 => "Thunderstorm",
        _ => "Unknown"
    };

    public string Icon => WeatherCode switch
    {
        0 => "☀️",
        1 or 2 => "🌤️",
        3 => "☁️",
        45 or 48 => "🌫️",
        51 or 53 or 55 or 56 or 57 => "🌦️",
        61 or 63 or 65 or 66 or 67 => "🌧️",
        71 or 73 or 75 or 77 => "❄️",
        80 or 81 or 82 => "🌦️",
        85 or 86 => "🌨️",
        95 or 96 or 99 => "⛈️",
        _ => "🌍"
    };
}

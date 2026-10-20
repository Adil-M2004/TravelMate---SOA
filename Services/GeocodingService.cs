
using System.Net.Http.Json;
using System.Text.Json.Serialization;

namespace TravelMate.Services;

public class GeocodingService
{
    private readonly HttpClient _httpClient;

    public GeocodingService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<CityResult>> SearchCitiesAsync(string city)
    {
        if (string.IsNullOrWhiteSpace(city) || city.Trim().Length < 2)
            return new List<CityResult>();

        string url =
            "https://geocoding-api.open-meteo.com/v1/search" +
            $"?name={Uri.EscapeDataString(city.Trim())}" +
            "&count=10&language=en&format=json";

        var response = await _httpClient
            .GetFromJsonAsync<GeocodingResponse>(url);

        return response?.Results ?? new List<CityResult>();
    }
}

public class GeocodingResponse
{
    [JsonPropertyName("results")]
    public List<CityResult>? Results { get; set; }
}

public class CityResult
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = "";

    [JsonPropertyName("country")]
    public string Country { get; set; } = "";

    [JsonPropertyName("country_code")]
    public string CountryCode { get; set; } = "";

    [JsonPropertyName("admin1")]
    public string? Region { get; set; }

    [JsonPropertyName("latitude")]
    public double Latitude { get; set; }

    [JsonPropertyName("longitude")]
    public double Longitude { get; set; }
}

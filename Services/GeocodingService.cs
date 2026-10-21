
using System.Net.Http.Json;
using System.Text.Json.Serialization;

namespace TravelMate.Services;

public class GeocodingService
{
    private readonly HttpClient _httpClient;

    // HttpClient is supplied by dependency injection for the city search call.
    public GeocodingService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<CityResult>> SearchCitiesAsync(string city)
    {
        // Avoid sending incomplete searches to the external API.
        if (string.IsNullOrWhiteSpace(city) || city.Trim().Length < 2)
            return new List<CityResult>();

        // Escape the search text so spaces and special characters are safe in a URL.
        string url =
            "https://geocoding-api.open-meteo.com/v1/search" +
            $"?name={Uri.EscapeDataString(city.Trim())}" +
            "&count=10&language=en&format=json";

        var response = await _httpClient
            .GetFromJsonAsync<GeocodingResponse>(url);

        // Use an empty list when the API has no results so callers do not need
        // to check for null before looping through the results.
        return response?.Results ?? new List<CityResult>();
    }
}

public class GeocodingResponse
{
    // The API places matching cities inside a property named "results".
    [JsonPropertyName("results")]
    public List<CityResult>? Results { get; set; }
}

public class CityResult
{
    // These properties describe one location returned by the geocoding API.
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


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



using System.Net.Http.Json;

namespace TravelMate.Services;

public class CurrencyService
{
    private readonly HttpClient _httpClient;

    public CurrencyService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public static decimal CalculateConversion(
    decimal amount, decimal rate)
    {
        if (amount < 0)
            throw new ArgumentOutOfRangeException(
                nameof(amount), "Amount cannot be negative.");

        if (rate <= 0)
            throw new ArgumentOutOfRangeException(
                nameof(rate), "Exchange rate must be positive.");

        return amount * rate;
    }

    public async Task<CurrencyResponse?> ConvertCurrencyAsync(
        string fromCurrency,
        string toCurrency,
        decimal amount)
    {
        string url =
            $"https://api.frankfurter.dev/v2/rate/" +
            $"{fromCurrency.ToUpperInvariant()}/" +
            $"{toCurrency.ToUpperInvariant()}";

        var response = await _httpClient
            .GetFromJsonAsync<CurrencyResponse>(url);

        if (response != null)
        {
            response.ConvertedAmount =
             CalculateConversion(amount, response.Rate);
        }

        return response;
    }
}

public class CurrencyResponse
{
    public string Date { get; set; } = "";
    public string Base { get; set; } = "";
    public string Quote { get; set; } = "";
    public decimal Rate { get; set; }
    public decimal ConvertedAmount { get; set; }
}




using System.Net.Http.Json;

namespace TravelMate.Services;

public class CurrencyService
{
    private readonly HttpClient _httpClient;

    // HttpClient is supplied by dependency injection and is used for the
    // request to the Frankfurter exchange-rate API.
    public CurrencyService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public static decimal CalculateConversion(
    decimal amount, decimal rate)
    {
        // Validate input here so this calculation is safe even when it is
        // called from somewhere other than the currency page.
        if (amount < 0)
            throw new ArgumentOutOfRangeException(
                nameof(amount), "Amount cannot be negative.");

        if (rate <= 0)
            throw new ArgumentOutOfRangeException(
                nameof(rate), "Exchange rate must be positive.");

        // The converted value is the original amount multiplied by the rate.
        return amount * rate;
    }

    public async Task<CurrencyResponse?> ConvertCurrencyAsync(
        string fromCurrency,
        string toCurrency,
        decimal amount)
    {
        // Frankfurter expects the source and target currency codes in the URL.
        string url =
            $"https://api.frankfurter.dev/v2/rate/" +
            $"{fromCurrency.ToUpperInvariant()}/" +
            $"{toCurrency.ToUpperInvariant()}";

        // Convert the JSON response directly into our C# response model.
        var response = await _httpClient
            .GetFromJsonAsync<CurrencyResponse>(url);

        if (response != null)
        {
            // The API returns the rate, while the page also needs the final
            // amount for the amount entered by the user.
            response.ConvertedAmount =
             CalculateConversion(amount, response.Rate);
        }

        return response;
    }
}

public class CurrencyResponse
{
    // These properties match the fields returned by the exchange-rate API.
    public string Date { get; set; } = "";
    public string Base { get; set; } = "";
    public string Quote { get; set; } = "";
    public decimal Rate { get; set; }
    public decimal ConvertedAmount { get; set; }
}



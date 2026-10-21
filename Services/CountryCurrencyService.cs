
namespace TravelMate.Services;

public class CountryCurrencyService
{
    // This small lookup table connects ISO country codes with the currency
    // normally used in those countries. It avoids an extra API call.
    private readonly Dictionary<string, string> _currencies = new()
    {
        { "IE", "EUR" },
        { "GB", "GBP" },
        { "US", "USD" },
        { "JP", "JPY" },
        { "AU", "AUD" },
        { "CA", "CAD" },
        { "CH", "CHF" },
        { "FR", "EUR" },
        { "DE", "EUR" },
        { "ES", "EUR" },
        { "IT", "EUR" },
        { "NL", "EUR" },
        { "AT", "EUR" },
        { "BE", "EUR" },
        { "PT", "EUR" },
        { "FI", "EUR" },
        { "GR", "EUR" },
        { "PK", "PKR" },
        { "IN", "INR" },
        { "AE", "AED" },
        { "SA", "SAR" },
        { "CN", "CNY" },
        { "KR", "KRW" },
        { "SG", "SGD" },
        { "NZ", "NZD" },
        { "SE", "SEK" },
        { "NO", "NOK" },
        { "DK", "DKK" },
        { "PL", "PLN" },
        { "CZ", "CZK" },
        { "RO", "RON" },
        { "HU", "HUF" },
        { "TR", "TRY" },
        { "ZA", "ZAR" },
        { "BR", "BRL" },
        { "MX", "MXN" },
        { "TH", "THB" }
    };

    public string? GetCurrency(string countryCode)
    {
        // Country codes from the geocoding API may use different casing,
        // so normalize them before looking them up.
        return _currencies.TryGetValue(
            countryCode.ToUpperInvariant(),
            out var currency)
            ? currency
            : null;
    }
}

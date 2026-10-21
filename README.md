# TravelMATE

## Project Overview

TravelMATE is a web-based travel planning application developed using Blazor and C# with .NET 10.

The application helps users prepare for international travel by providing destination weather information and currency exchange calculations.

## Technologies Used

- C#
- ASP.NET Core Blazor Web App (.NET 10)
- HTML, CSS and Bootstrap
- HTTPClient for REST API integration
- JSON deserialization
- xUnit for unit testing
- Git and GitHub for version control

## Third-Party APIs

### 1. Open-Meteo

Used to retrieve current weather information, including temperature, humidity and wind speed.

The Open-Meteo Geocoding API is also used to search for destinations worldwide.

Website: https://open-meteo.com/

### 2. Frankfurter

Used to retrieve exchange rates and calculate currency conversions.

Website: https://frankfurter.dev/

## Application Features

- Worldwide destination search
- Current destination weather information
- Currency conversion between supported currencies
- Integrated travel dashboard
- Input validation and API error handling

## Application Architecture

The application separates its UI components from API integration logic.

- `WeatherService.cs` — Retrieves weather information.
- `GeocodingService.cs` — Searches for destinations.
- `CurrencyService.cs` — Retrieves exchange rates and calculates conversions.
- `CountryCurrencyService.cs` — Maps country codes to currency codes.

Services are registered using ASP.NET Core dependency injection.

## Testing

The project includes an xUnit testing project called `TravelMate.Tests`.

Unit tests verify currency conversion calculations and input validation.

## How to Run

1. Open the solution in Visual Studio.
2. Ensure the .NET 10 SDK is installed.
3. Restore NuGet packages.
4. Build the solution.
5. Run the TravelMate application using F5.
6. Open the application in the browser.

An internet connection is required to retrieve information from external APIs.

## Version Control

GitHub is used for source control, with `main` and `development` branches.

## References

- Open-Meteo documentation: https://open-meteo.com/en/docs
- Open-Meteo Geocoding API: https://open-meteo.com/en/docs/geocoding-api
- Frankfurter API documentation: https://frankfurter.dev/
- Microsoft Blazor documentation: https://learn.microsoft.com/aspnet/core/blazor/
- xUnit documentation: https://xunit.net/

## Limitations

- Not all destination currencies are supported by Frankfurter.
- Weather information depends on the availability of the external API.
- The application does not currently support user accounts or persistent trip storage.


using TravelMate.Components;
using TravelMate.Services;

var builder = WebApplication.CreateBuilder(args);

// Register Blazor services so the application can render Razor components
// and keep interactive pages connected to the server.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Register each application service with HttpClient. Blazor will create the
// service and provide an HttpClient whenever a page asks for it.
builder.Services.AddHttpClient<WeatherService>();
builder.Services.AddHttpClient<CurrencyService>();
builder.Services.AddHttpClient<GeocodingService>();
builder.Services.AddSingleton<CountryCurrencyService>();
builder.Services.AddHttpClient<ForecastService>();

var app = builder.Build();

// Configure how incoming requests are handled by the application.
if (!app.Environment.IsDevelopment())
{
    // Show the friendly error page instead of exposing technical details in production.
    app.UseExceptionHandler("/Error", createScopeForErrors: true);

    // Tell browsers to use HTTPS for future requests to this site.
    app.UseHsts();
}

// Send unknown routes to the application's not-found component.
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();

// Protect requests from cross-site request forgery attacks.
app.UseAntiforgery();

// Make files such as CSS, JavaScript, and images available to the browser.
app.MapStaticAssets();

// Map the root Blazor component and enable interactive server rendering.
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();

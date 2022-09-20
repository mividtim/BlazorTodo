using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Fluxor;
using System.Reflection;
using BlazorTodoClient.Services;
using System.Net.Mime;
using BlazorTodoClient;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("app");

// Add Fluxor
builder.Services.AddFluxor(options => 
{
    options.ScanAssemblies(Assembly.GetExecutingAssembly());
    options.UseReduxDevTools();
});

// Add custom application services
builder.Services.AddScoped<StateFacade>();
builder.Services.AddHttpClient<JsonPlaceholderApiService>(client =>
{
    client.DefaultRequestHeaders.Add("Content-Control", $"{MediaTypeNames.Application.Json}; charset=utf-8");
    client.BaseAddress = new Uri("https://localhost:7034/api/");
});

await builder.Build().RunAsync();
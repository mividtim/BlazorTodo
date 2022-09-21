using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Fluxor;
using System.Reflection;
using System.Net.Mime;
using BlazorTodoClient;
using BlazorTodoClient.Features.Todos.Store;
using BlazorTodoClient.ServiceClients;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("app");

// Add Fluxor
builder.Services.AddFluxor(options => 
{
    options.ScanAssemblies(Assembly.GetExecutingAssembly());
    options.UseReduxDevTools();
});

// Add custom application services
builder.Services.AddScoped<TodosStateFacade>();
builder.Services.AddHttpClient<BlazorTodoApiService>(client =>
{
    client.DefaultRequestHeaders.Add("Content-Control", $"{MediaTypeNames.Application.Json}; charset=utf-8");
    client.BaseAddress = new Uri("https://localhost:7034/api/");
});

await builder.Build().RunAsync();
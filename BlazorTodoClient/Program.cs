using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Fluxor;
using System.Reflection;
using System.Net.Mime;
using Blazored.LocalStorage;
using BlazorTodoClient;
using BlazorTodoClient.Features.Authx;
using BlazorTodoClient.Features.Authx.Store;
using BlazorTodoClient.Features.Navigation.Store;
using BlazorTodoClient.Features.Todos.Store;
using BlazorTodoClient.ServiceClients;
using Microsoft.AspNetCore.Components.Authorization;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("app");

// Add authenticated API client
builder.Services.AddSingleton<AuthxMessageHandler>();
builder.Services.AddHttpClient<BlazorTodoApiClient>(client =>
{
    client.DefaultRequestHeaders.Add("Content-Control", $"{MediaTypeNames.Application.Json}; charset=utf-8");
    client.BaseAddress = new Uri(builder.Configuration["ApiBase"]
                                 ?? throw new ApplicationException("ApiBase not defined in configuration"));
}).AddHttpMessageHandler<AuthxMessageHandler>();

// Add authentication
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, AuthxStateProvider>();

// Add Fluxor
builder.Services.AddFluxor(options => 
{
    options.ScanAssemblies(Assembly.GetExecutingAssembly());
    options.UseReduxDevTools();
});

// Add application services
builder.Services.AddScoped<INavigationService, NavigationService>();
builder.Services.AddScoped<IAuthxService, AuthxService>();
builder.Services.AddScoped<ITodosService, TodosService>();
builder.Services.AddScoped<IGoogleService, GoogleService>(_ =>
    new GoogleService(builder.Configuration["Google:ClientId"]
                      ?? throw new ApplicationException("Google:ClientId not specified in configuration")));

await builder.Build().RunAsync();
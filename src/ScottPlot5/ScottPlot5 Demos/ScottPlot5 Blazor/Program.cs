using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

using BlazorObservers.ObserverLibrary.DI;

using Sandbox.Blazor.WebAssembly;
using Sandbox.Blazor.WebAssembly.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

var httpClient = new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) };
builder.Services.AddScoped(sp => httpClient);

var recipes = new RecipesService(httpClient);
builder.Services.AddSingleton<IRecipesService>(recipes);

builder.Services.AddResizeObserverService();
builder.Services.AddSingleton<IResizeService>(new ResizeService());


var app = builder.Build();
await recipes.GetRecipesAsync();

await app.RunAsync();

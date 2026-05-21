using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MuseoAurora.Frontend;
using MuseoAurora.Frontend.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:9000/") });

builder.Services.AddScoped<ArtworkClient>();
builder.Services.AddScoped<ExhibitionClient>();

await builder.Build().RunAsync();

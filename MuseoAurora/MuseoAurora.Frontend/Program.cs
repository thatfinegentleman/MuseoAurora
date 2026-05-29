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
builder.Services.AddScoped<VisitorClient>();
builder.Services.AddScoped<TicketClient>();
builder.Services.AddScoped<ReservationClient>();

await builder.Build().RunAsync();

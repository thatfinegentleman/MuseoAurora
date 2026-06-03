using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MuseoAurora.Client.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddScoped<ExhibitionProxyService>();
builder.Services.AddScoped<ArtworkProxyService>();
builder.Services.AddScoped<GuidedTourProxyService>();
builder.Services.AddScoped<VisitorProxyService>();
builder.Services.AddScoped<TicketTypeProxyService>();
builder.Services.AddScoped<ReservationProxyService>();
builder.Services.AddScoped<TicketProxyService>();

await builder.Build().RunAsync();

using Microsoft.EntityFrameworkCore;
using MuseoAurora.Backend.Context;
using MuseoAurora.Backend.Services;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("AuroraDB");

builder.Services.AddDbContext<MuseoAuroraDbContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddOpenApi();

builder.Services.AddScoped<IArtworkService, ArtworkService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.Run();
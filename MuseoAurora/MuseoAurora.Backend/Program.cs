using Microsoft.EntityFrameworkCore;
using MuseoAurora.Backend.Context;
using MuseoAurora.Backend.Endpoints;
using MuseoAurora.Backend.Services;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("AuroraDB");

builder.Services.AddDbContext<MuseoAuroraDbContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddOpenApi();
builder.Services.AddScoped<IExhibitionService, ExhibitionService>();
builder.Services.AddScoped<IArtworkService, ArtworkService>();
builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazor", policy =>
    {
        policy.SetIsOriginAllowed(origin => true)
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}
app.UseCors("AllowBlazor");
app.MapArtworkEndpoints();
app.MapExhibitionEndpoints();
app.Run("http://localhost:9000");
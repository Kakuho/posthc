using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using Phc.Data;
using Phc.Service.Interface;
using Phc.Service;
using Phc.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<PhcContext>(
    options => options.UseNpgsql(builder.Configuration.GetConnectionString("PhcContext"))
);

builder.Services.AddControllers()
       .AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IBandService, BandService>();
builder.Services.AddScoped<IAlbumService, AlbumService>();
builder.Services.AddScoped<IPlaylistService, PlaylistService>();

// configure the middleware

var app = builder.Build();
//app.UseExceptionMiddleware();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();

using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using Phc.Data;
using Phc.Service.Interface;
using Phc.Service;

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

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();

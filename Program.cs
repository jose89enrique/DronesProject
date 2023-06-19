using Microsoft.OpenApi.Models;
using Drones.DB;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
     c.SwaggerDoc("v1", new OpenApiInfo { Title = "Drones API", Description = "Testing enviroment", Version = "v1" });
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
   c.SwaggerEndpoint("/swagger/v1/swagger.json", "Drones API");
});


app.MapPost("/drones", (RegisterDrone newDrone)=> DroneDB.registerDrone(newDrone));

app.MapPost("/drones/load", (LoadDrone loadM)=> DroneDB.loadMedication(loadM));

app.MapGet("/drones/medication/{serial}", (string serial)=>DroneDB.checkLoadDrone(serial));

app.MapGet("/drones/ready", ()=>DroneDB.checkDronesReady());

app.MapGet("/drones/battery/{serial}", (string serial)=>DroneDB.checkBatteryDrone(serial));

DroneDB.startTime();

app.Run();

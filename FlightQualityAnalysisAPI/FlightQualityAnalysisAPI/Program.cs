using FlightQualityAnalysisAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IFlightService, FlightService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
// Added route endpoints
app.MapGet("/api/GetAllFlightsDetails", (IFlightService service) =>
{
    return Results.Ok(service.GetAllFlightsDetails());
});

app.MapGet("/api/GetInconsistentFlightsDetails", (IFlightService service) =>
{
    return Results.Ok(service.GetInconsistentFlightsDetails());
});
//
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

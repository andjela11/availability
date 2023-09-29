using Application;
using Infrastructure;
using MinimalAPI.Endpoints;
using MinimalAPI.Properties;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddInfrastructureServices()
    .AddApplicationServices();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

app.UseSwagger();
app.UseSwaggerUI();

app.MapGet("/Movie/{id:int}", MovieEndpoints.GetMovieAsync);
app.MapGet("/Movie/", MovieEndpoints.GetAllMoviesAsync);
app.MapPost("/Reservation/", ReservationEndpoints.CreateReservationAsync);
app.MapGet("/Reservation/{id:int}", ReservationEndpoints.GetReservationAsync);

app.Run();

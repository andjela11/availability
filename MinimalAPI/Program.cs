using Application;
using Infrastructure;
using MinimalAPI.Endpoints;
using MinimalAPI.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddInfrastructureServices(builder.Configuration)
    .AddApplicationServices();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

app.UseSwagger();
app.UseSwaggerUI();

app.MapGet("/Movie/{id:int}", MovieEndpoints.GetMovieAsync);
app.MapGet("/Movie/", MovieEndpoints.FilterMoviesAsync);

app.MapPost("/Reservation/", ReservationEndpoints.CreateReservationAsync);
app.MapGet("/Reservation/{id:int}", ReservationEndpoints.GetReservationAsync);
app.MapGet("/Reservation/", ReservationEndpoints.GetAllReservations);
app.MapGet("/Reservations/", ReservationEndpoints.ShowAvailableReservations);

app.MapGet("/Genres", GenreEndpoints.GetAllGenres);
app.MapPost("/Genres", GenreEndpoints.CreateGenre);
app.MapPut("/Genres", GenreEndpoints.UpdateGenre);
app.MapDelete("/Genres", GenreEndpoints.DeleteGenre);

app.Run();

using Application;
using Application.Interfaces;
using Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Mvc;
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

app.MapGet("/{id:int}", MovieEndpoints.GetMovie);

app.Run();
using System.Net;
using System.Text.Json;
using Application.Exceptions;
using FluentValidation;
using MinimalAPI.Models;

namespace MinimalAPI.Middleware;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (ValidationException e)
        {
            var validationErrors = e.Errors.Where(x => x != null)
                .GroupBy(
                    x => x.PropertyName,
                    x => x.ErrorMessage,
                    (propertyName, errorMessages) => new
                    {
                        Key = propertyName, Values = errorMessages.Distinct().ToArray()
                    })
                .ToDictionary(x => x.Key, x => x.Values);

            await GenerateExceptionResponse(context, e, (int)HttpStatusCode.UnprocessableEntity, validationErrors);
        }
        catch (EntityNotFoundException e)
        {
            await GenerateExceptionResponse(context, e, (int)HttpStatusCode.NotFound);
        }
        catch (NoAvailableSeatsException e)
        {
            await GenerateExceptionResponse(context, e, (int)HttpStatusCode.Forbidden);
        }
        catch (Exception e)
        {
            await GenerateExceptionResponse(context, e, (int)HttpStatusCode.InternalServerError);
        }
    }

    private async Task GenerateExceptionResponse(HttpContext context, Exception e, int statusCode, Dictionary<string, string[]>? validationErrors = default)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = statusCode;

        var response = new ErrorDetails(context.Response.StatusCode, e.Message, e.StackTrace?.Substring(0, 100));

        if (validationErrors is not null)
        {
            response.ValidationErrors = validationErrors;
        }

        var json = JsonSerializer.Serialize(response, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

        await context.Response.WriteAsync(json);
    }
}

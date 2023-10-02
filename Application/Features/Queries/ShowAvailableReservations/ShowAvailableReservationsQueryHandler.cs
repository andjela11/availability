using Application.Contracts;
using Application.Exceptions;
using Application.Interfaces;
using MediatR;

namespace Application.Features.Queries.ShowAvailableReservations;

public class ShowAvailableReservationsQueryHandler : IRequestHandler<ShowAvailableReservationsQuery, List<AvailableReservationDto>>
{
    private readonly IReservationHttpClient _reservationHttpClient;
    private readonly IMovieHttpClient _movieHttpClient;

    public ShowAvailableReservationsQueryHandler(
        IReservationHttpClient reservationHttpClient,
        IMovieHttpClient movieHttpClient)
    {
        _reservationHttpClient = reservationHttpClient;
        _movieHttpClient = movieHttpClient;
    }

    public async Task<List<AvailableReservationDto>> Handle(ShowAvailableReservationsQuery request,
        CancellationToken cancellationToken)
    {
        var movies = await _movieHttpClient.FilterMoviesAsync();

        if (movies is null)
        {
            throw new EntityNotFoundException("No movies");
        }

        var availableReservations = new List<AvailableReservationDto>();

        foreach (var movie in movies)
        {
            var reservation = await _reservationHttpClient.GetReservationAsync(movie.Id);
            if (reservation is not null)
            {
                availableReservations.Add(AvailableReservationDto.FromMovieReservation(movie, reservation));
            }
        }

        return availableReservations
            .Skip((request.PageNumber) * request.PageSize)
            .Take(request.PageSize)
            .ToList();
    }
}

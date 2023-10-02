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
        var reservations = await _reservationHttpClient.GetAllReservationsAsync();
        var movies = await _movieHttpClient.GetAllMoviesAsync();

        if (reservations is null)
        {
            throw new EntityNotFoundException("No reservations");
        }

        if (movies is null)
        {
            throw new EntityNotFoundException("No movies");
        }

        var availableReservations = new List<AvailableReservationDto>();

        foreach (var movie in movies)
        {
            var reservationList = reservations.FindAll(x => x.MovieId == movie.Id);
            if (reservationList.Count > 0)
            {
                availableReservations.AddRange(reservationList.Select(reservation => AvailableReservationDto.FromMovieReservation(movie, reservation)));
            }
        }

        return availableReservations
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToList();
    }
}

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

        foreach (var reservation in reservations)
        {
            var movie = movies.FirstOrDefault(x => x.Id == reservation.MovieId);
            if (movie is not null)
            {
                availableReservations.Add(AvailableReservationDto.FromMovieReservation(movie, reservation));
            }
        }

        return availableReservations;
    }
}

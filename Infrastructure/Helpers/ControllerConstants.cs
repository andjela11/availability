namespace Infrastructure.Helpers;

public static class Constants
{
    public static class Controllers
    {
        public const string Movies = "Movies";
        public const string Reservations = "Reservations";
    }

    public static class MoviesRelativeUrls
    {
        public const string Filter = "/filter";
    }
    public static class ReservationsRelativePaths
    {
        public const string GetByMovieId = "/get-by-movieid";
    }
}

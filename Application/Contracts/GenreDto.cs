using Domain;

namespace Application.Contracts;

public record GenreDto(string Name)
{
    public static GenreDto FromGenre(Genre genre)
    {
        return new GenreDto(genre.Name);
    }
}

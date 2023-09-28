namespace Application.Contracts;

public record MovieDto(int Id, string? Title, List<string>? Genre, string? Synopsis, int? Released);
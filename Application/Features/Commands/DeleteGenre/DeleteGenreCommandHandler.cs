using Application.Interfaces;
using MediatR;

namespace Application.Features.Commands.DeleteGenre;

public class DeleteGenreCommandHandler : IRequestHandler<DeleteGenreCommand, Unit>
{
    private readonly IMongoDBService _mongoDbService;

    public DeleteGenreCommandHandler(IMongoDBService mongoDbService)
    {
        _mongoDbService = mongoDbService;
    }
    
    public async Task<Unit> Handle(DeleteGenreCommand request, CancellationToken cancellationToken)
    {
        await _mongoDbService.DeleteGenre(request.GenreId);
        return new Unit();
    }
}

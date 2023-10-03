using Application.Interfaces;
using MediatR;

namespace Application.Features.Commands.DeleteGenre;

public class DeleteGenreCommandHandler : IRequestHandler<DeleteGenreCommand>
{
    private readonly IMongoDBService _mongoDbService;

    public DeleteGenreCommandHandler(IMongoDBService mongoDbService)
    {
        _mongoDbService = mongoDbService;
    }
    
    public async Task Handle(DeleteGenreCommand request, CancellationToken cancellationToken)
    {
        await _mongoDbService.DeleteGenre(request.GenreId);        
    }
}

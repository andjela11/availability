using Application.Interfaces;
using MediatR;

namespace Application.Features.Commands.CreateGenre;

public class CreateGenreCommandHandler : IRequestHandler<CreateGenreCommand, string>
{
    private readonly IMongoDBService _mongoDbService;

    public CreateGenreCommandHandler(IMongoDBService mongoDbService)
    {
        _mongoDbService = mongoDbService;
    }
    
    public async Task<string> Handle(CreateGenreCommand request, CancellationToken cancellationToken)
    {
        var id = await _mongoDbService.CreateGenre(request.genreName);
        return id;
    }
}

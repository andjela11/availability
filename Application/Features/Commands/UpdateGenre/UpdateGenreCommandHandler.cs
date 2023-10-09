using Application.Interfaces;
using MediatR;

namespace Application.Features.Commands.UpdateGenre;

public class UpdateGenreCommandHandler : IRequestHandler<UpdateGenreCommand, Unit>
{
    private readonly IMongoDBService _mongoDbService;

    public UpdateGenreCommandHandler(IMongoDBService mongoDbService)
    {
        _mongoDbService = mongoDbService;
    }

    public async Task<Unit> Handle(UpdateGenreCommand request, CancellationToken cancellationToken)
    {
        await _mongoDbService.UpdateGenre(request.Genre);
        return new Unit();
    }
}

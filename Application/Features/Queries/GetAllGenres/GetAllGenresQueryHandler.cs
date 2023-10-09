using Application.Contracts;
using Application.Interfaces;
using MediatR;

namespace Application.Features.Queries.GetAllGenres;

public class GetAllGenresQueryHandler : IRequestHandler<GetAllGenresQuery, List<GenreDto>>
{
    private readonly IMongoDBService _mongoDbService;

    public GetAllGenresQueryHandler(IMongoDBService service)
    {
        _mongoDbService = service;
    }
    
    public Task<List<GenreDto>> Handle(GetAllGenresQuery request, CancellationToken cancellationToken)
    {
        return _mongoDbService.GetAllGenres();
    }
}

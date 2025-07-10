using System.Linq.Expressions;
using Ambev.DeveloperEvaluation.Domain.Aggregates.UserAggregate.Entities;
using Ambev.DeveloperEvaluation.Domain.Aggregates.UserAggregate.Interfaces;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.UseCases.Users.Queries.GetUsers;


public sealed class GetUsersHandler : IRequestHandler<GetUsersQuery, GetUsersResponse>
{
    private readonly IUserRepository _userRepository;

    public GetUsersHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<GetUsersResponse> Handle(GetUsersQuery query, CancellationToken cancellationToken)
    {
        var filter = CreateFilter(query);
        
        var users = _userRepository.QueryableComplete(query.PageNumber, query.PageSize, filter);
        var data = users.Select(x => x.ToGetUsersResponseData());
        var hits = await _userRepository.CountAsync(filter, cancellationToken);

        return new GetUsersResponse
        {
            Data = data,
            Hits = hits
        };
    }
    
    private static Expression<Func<User, bool>> CreateFilter(GetUsersQuery query)
    {
        return user =>
            (query.Role == null || user.Role == query.Role) &&
            (query.Status == null || user.Status == query.Status) &&
            (query.StartDate == null || user.CreatedAt >= query.StartDate) &&
            (query.EndDate == null || user.CreatedAt <= query.EndDate);
    }
}
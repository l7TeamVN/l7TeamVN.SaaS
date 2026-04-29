using FluentValidation;
using l7TeamVN.SaaS.Application.Messaging;
using l7TeamVN.SaaS.Application.Messaging.Handlers;
using l7TeamVN.SaaS.Modules.Identity.Application.Dtos;
using l7TeamVN.SaaS.Modules.Identity.Domain.Repositories;
using l7TeamVN.SaaS.SharedKernel.Results;

namespace l7TeamVN.SaaS.Modules.Identity.Application.Users;

public record GetUsersQuery : IQuery<IEnumerable<UserDto>>
{
}


public class GetUsersValidator : AbstractValidator<GetUsersQuery>
{
    public GetUsersValidator()
    {

    }
}

public class GetUsersHandler(IUserRepository userRepository) : IQueryHandler<GetUsersQuery, IEnumerable<UserDto>>
{
    public async Task<Result<IEnumerable<UserDto>>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        var users = await userRepository.GetAllAsync(cancellationToken);

        var userDtos = users.Select(u => new UserDto
        {
            UserName = u.UserName,
            Email = u.Email,
            PhoneNumber = u.PhoneNumber,
            FullName = u.FullName
        }).ToList();

        return userDtos;  
    }
}

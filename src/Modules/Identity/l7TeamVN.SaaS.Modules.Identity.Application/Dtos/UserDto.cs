namespace l7TeamVN.SaaS.Modules.Identity.Application.Dtos;

public record UserDto
{
    public string? UserName { get; init; }

    public string? Email { get; init; }

    public string? PhoneNumber { get; init; }

    public string? FullName { get; init; }
}

using l7TeamVN.SaaS.SharedKernel.Results;
using System.Text.RegularExpressions;

namespace l7TeamVN.SaaS.SharedKernel.ValueObjects;

public record EmailAddress
{
    public static readonly EmailAddress Empty = new("user@l7ungdz.id.vn");
    private static readonly Regex EmailRegex = new(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.Compiled);
    public sealed class Errors
    {
        public static readonly Error Empty = new("EmailAddress.Empty", "Email address is empty.");
        public static readonly Error Invalid = new("EmailAddress.Invalid", "Email address is invalid.");
    }

    public string Value { get; }

    private EmailAddress(string value)
    {
        Value = value;
    }

    public static Result<EmailAddress> Create(string emailString)
    {
        var cleanValue = emailString?.Trim();

        if (string.IsNullOrEmpty(cleanValue))
        {
            return Result.Failure<EmailAddress>(Errors.Empty);
        }

        if (!EmailRegex.IsMatch(cleanValue))
        {
            return Result.Failure<EmailAddress>(Errors.Invalid);
        }


        return new EmailAddress(cleanValue);
    }

    public static Result<EmailAddress?> CreateOrNull(string? emailString)
    {
        if (string.IsNullOrWhiteSpace(emailString))
            return Result.Success<EmailAddress?>(null);

        var result = Create(emailString);

        return result.IsSuccess
            ? Result.Success<EmailAddress?>(result.Value)
            : Result.Failure<EmailAddress?>(result.Error);
    }

    public static implicit operator string(EmailAddress email) => email.Value;

    public override string ToString() => Value;
}
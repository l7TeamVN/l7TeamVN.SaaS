using l7TeamVN.SaaS.SharedKernel.Results;
using System.Text.RegularExpressions;

namespace l7TeamVN.SaaS.SharedKernel.ValueObjects;

public record PhoneNumber : IFormattable
{
    private static readonly Regex InternationalPhoneRegex = new(@"^\+[1-9]\d{1,14}$", RegexOptions.Compiled);

    public sealed class Errors
    {
        public static readonly Error Empty = new("PhoneNumber.Empty", "Phone number is empty.");
        public static readonly Error Invalid = new("PhoneNumber.Invalid", "Phone number is invalid.");
    }

    public string Value { get; }

    private PhoneNumber(string value)
    {
        Value = value;
    }

    public static Result<PhoneNumber> Create(string phoneNumberString)
    {
        var cleanValue = phoneNumberString?.Replace(" ", "").Replace("-", "").Replace("(", "").Replace(")", "").Trim();

        if (string.IsNullOrWhiteSpace(cleanValue))
        {
            return Result.Failure<PhoneNumber>(Errors.Empty);
        }

        if (!cleanValue.StartsWith("+"))
        {
            cleanValue = "+" + cleanValue;
        }

        if (!InternationalPhoneRegex.IsMatch(cleanValue))
        {
            return Result.Failure<PhoneNumber>(Errors.Invalid);
        }

        return new PhoneNumber(cleanValue);
    }

    public static Result<PhoneNumber?> CreateOrNull(string? phoneNumberString)
    {
        if (string.IsNullOrWhiteSpace(phoneNumberString))
            return Result.Success<PhoneNumber?>(null);

        var result = Create(phoneNumberString);

        return result.IsSuccess ? Result.Success<PhoneNumber?>(result.Value) : Result.Failure<PhoneNumber?>(result.Error);

    }

    public static implicit operator string?(PhoneNumber? phoneNumber) => phoneNumber?.Value;

    public override string ToString() => Value;

    public string ToString(string? format, IFormatProvider? formatProvider = null)
    {
        if (string.IsNullOrWhiteSpace(format)) format = "G";

        return format.ToUpperInvariant() switch
        {
            "G" => Value, // General format (default) : +84987654321
            "E" => Value, // E.164 format : +84987654321
            "I" => FormatInternationalFallback(Value), // International fallback format : +84 987 654 321
            _ => throw new FormatException($"The format '{format}' is not supported.")
        };
    }

    private static string FormatInternationalFallback(string e164Number)
    {
        if (e164Number.Length <= 4) return e164Number;

        return $"{e164Number[..3]} {e164Number[3..6]} {e164Number[6..9]} {e164Number[9..]}".Trim();
    }
}
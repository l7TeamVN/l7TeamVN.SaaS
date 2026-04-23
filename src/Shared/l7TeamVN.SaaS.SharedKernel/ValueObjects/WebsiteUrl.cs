using l7TeamVN.SaaS.SharedKernel.Results;

namespace l7TeamVN.SaaS.SharedKernel.ValueObjects;

public record WebsiteUrl
{
    public sealed class Errors
    {
        public static readonly Error Empty = new("WebsiteUrl.Empty", "Website URL is empty.");
        public static readonly Error Invalid = new("WebsiteUrl.Invalid", "Website URL is invalid.");
        public static readonly Error UnsupportedScheme = new("WebsiteUrl.UnsupportedScheme", "Only HTTP and HTTPS schemes are supported.");
    }

    public string Value { get; }

    private WebsiteUrl(string value)
    {
        Value = value;
    }
    public static Result<WebsiteUrl> Create(string url)
    {
        var cleanValue = url?.Trim();

        if (string.IsNullOrWhiteSpace(cleanValue))
            return Result.Failure<WebsiteUrl>(Errors.Empty);

        bool isValid = Uri.TryCreate(cleanValue, UriKind.Absolute, out Uri? uriResult);

        if (!isValid || uriResult == null)
        {
            return Result.Failure<WebsiteUrl>(Errors.Invalid);
        }

        if (uriResult.Scheme != Uri.UriSchemeHttp && uriResult.Scheme != Uri.UriSchemeHttps)
        {
            return Result.Failure<WebsiteUrl>(Errors.UnsupportedScheme);
        }


        return new WebsiteUrl(cleanValue);
    }

    public static Result<WebsiteUrl?> CreateOrNull(string? url)
    {
        var cleanUrl = url?.Trim();

        if (string.IsNullOrWhiteSpace(cleanUrl)) return Result.Success<WebsiteUrl?>(null);

        var result = Create(cleanUrl);

        return result.IsSuccess ? Result.Success<WebsiteUrl?>(result.Value) : Result.Failure<WebsiteUrl?>(result.Error);
    }

    public string GetDomain()
    {
        if (Uri.TryCreate(Value, UriKind.Absolute, out Uri? uriResult))
        {
            return uriResult.Host;
        }
        throw new InvalidOperationException("Not a valid URL.");
    }

    public static implicit operator string(WebsiteUrl websiteUrl) => websiteUrl.Value;

    public override string ToString() => Value;
}
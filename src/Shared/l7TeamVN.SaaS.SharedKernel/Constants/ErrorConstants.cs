using l7TeamVN.SaaS.SharedKernel.Results;

namespace l7TeamVN.SaaS.SharedKernel.Constants;

public static class ErrorConstants
{
    public static readonly Error None = new(string.Empty, string.Empty);

    public static readonly Error NullValue = new("Error.NullValue", "The specified result value is null.");
}

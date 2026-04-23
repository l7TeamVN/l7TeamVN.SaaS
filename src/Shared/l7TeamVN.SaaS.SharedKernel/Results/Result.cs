using l7TeamVN.SaaS.SharedKernel.Constants;

namespace l7TeamVN.SaaS.SharedKernel.Results;

public class Result
{
    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public Error[] Errors { get; }
    public Error Error => Errors.FirstOrDefault() ?? ErrorConstants.None;

    protected Result(bool isSuccess, params Error[] errors)
    {
        if (isSuccess && !errors.Contains(ErrorConstants.None))
        {
            throw new InvalidOperationException("A successful result cannot contain an error.");
        }

        if (!isSuccess && errors.Contains(ErrorConstants.None))
        {
            throw new InvalidOperationException("A failed result must contain an error.");
        }

        IsSuccess = isSuccess;

        Errors = errors;
    }

    public static Result Success() => new(true, ErrorConstants.None);

    public static Result Failure(params Error[] errors) => new(false, errors);

    public static Result<TValue> Success<TValue>(TValue value) => new(value, true, ErrorConstants.None);

    public static Result<TValue> Failure<TValue>(params Error[] errors) => new(default, false, errors);

    public static Result<TValue> Create<TValue>(TValue? value) => value is not null ? Success(value) : Failure<TValue>(ErrorConstants.NullValue);
}

public class Result<TValue> : Result
{
    private readonly TValue? _value;

    protected internal Result(TValue? value, bool isSuccess, params Error[] errors)
        : base(isSuccess, errors)
    {
        _value = value;
    }

    public TValue Value => IsSuccess ? _value! : throw new InvalidOperationException("The value of a failed result cannot be accessed.");

    public static implicit operator Result<TValue>(TValue? value) => Create(value);
}

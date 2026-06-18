using System;
using Oxide.Ext.UiFramework.Guards;

namespace Oxide.Ext.UiFramework.Types.Results;

public class Result<T>
{
    public T Value { get; }
    public string Error { get; }
    public Exception Exception { get; }
    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;

    private Result(T value, bool isSuccess, string error, Exception exception)
    {
        Value = value;
        IsSuccess = isSuccess;
        Error = error;
        Exception = exception;
    }

    public static Result<T> Success(T value) => new(value, true, null, null);
    public static Result<T> Failure(string error) => new(default, false, error ?? throw new ArgumentNullException(nameof(error)), null);
    public static Result<T> Failure(Exception exception) => new(default, false, exception?.ToString() ?? throw new ArgumentNullException(nameof(exception)), exception);

    public static implicit operator Result<T>(T value) => Success(value);
    public static implicit operator T(Result<T> result) => result.IsSuccess ? result.Value : throw new InvalidOperationException($"Result was not successful. {result.Error}");

    public R Match<R>(Func<T, R> onSuccess, Func<Result<T>, R> onFailure)
    {
        Guard.IsNotNull(onSuccess );
        Guard.IsNotNull(onFailure);
        return IsSuccess ? onSuccess(Value) : onFailure(this);
    }
}
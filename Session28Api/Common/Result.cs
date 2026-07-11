namespace Session28Api.Common;

public enum ResultError
{
    None,
    Unauthorized,
    NotFound,
    Conflict
}

public class Result<T>
{
    private Result(bool isSuccess, T? value, ResultError error, string? message)
    {
        IsSuccess = isSuccess;
        Value = value;
        Error = error;
        Message = message;
    }

    public bool IsSuccess { get; }

    public T? Value { get; }

    public ResultError Error { get; }

    public string? Message { get; }

    public static Result<T> Success(T value) => new(true, value, ResultError.None, null);

    public static Result<T> Failure(ResultError error, string message) => new(false, default, error, message);
}

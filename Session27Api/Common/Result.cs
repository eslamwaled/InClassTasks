namespace Session27Api.Common;

public class Result<T>
{
    private Result(bool isSuccess, T? value, string? error, int statusCode)
    {
        IsSuccess = isSuccess;
        Value = value;
        Error = error;
        StatusCode = statusCode;
    }

    public bool IsSuccess { get; }
    public T? Value { get; }
    public string? Error { get; }
    public int StatusCode { get; }

    public static Result<T> Success(T value) =>
        new(true, value, null, StatusCodes.Status200OK);

    public static Result<T> Unauthorized(string error) =>
        new(false, default, error, StatusCodes.Status401Unauthorized);

    public static Result<T> Forbidden(string error) =>
        new(false, default, error, StatusCodes.Status403Forbidden);

    public static Result<T> Conflict(string error) =>
        new(false, default, error, StatusCodes.Status409Conflict);

    public static Result<T> NotFound(string error) =>
        new(false, default, error, StatusCodes.Status404NotFound);
}

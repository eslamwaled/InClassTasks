namespace Session26Api.Common;

public enum ResultError
{
    None,
    Conflict,
    Unauthorized
}

public class Result<T>
{
    public bool Success { get; }
    public ResultError Error { get; }
    public string? ErrorMessage { get; }
    public T? Data { get; }

    private Result(bool success, T? data, ResultError error, string? errorMessage)
    {
        Success = success;
        Data = data;
        Error = error;
        ErrorMessage = errorMessage;
    }

    public static Result<T> Ok(T data) => new(true, data, ResultError.None, null);

    public static Result<T> Fail(ResultError error, string errorMessage) => new(false, default, error, errorMessage);
}

using Microsoft.AspNetCore.Mvc;
using Session28Api.Common;

namespace Session28Api.Controllers;

/// <summary>
/// Base controller that translates service results into HTTP responses.
/// </summary>
[ApiController]
public abstract class ApiControllerBase : ControllerBase
{
    /// <summary>
    /// Maps a service <see cref="Result{T}"/> to 200, 401, 404 or 409.
    /// </summary>
    /// <typeparam name="T">The type of the successful payload.</typeparam>
    /// <param name="result">The result returned by the service layer.</param>
    /// <returns>The HTTP response matching the result state.</returns>
    protected ActionResult ToActionResult<T>(Result<T> result)
    {
        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }

        return result.Error switch
        {
            ResultError.Unauthorized => Unauthorized(new { message = result.Message }),
            ResultError.NotFound => NotFound(new { message = result.Message }),
            ResultError.Conflict => Conflict(new { message = result.Message }),
            _ => BadRequest(new { message = result.Message })
        };
    }
}

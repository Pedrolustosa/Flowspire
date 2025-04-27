using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Flowspire.Application.Common;

namespace Flowspire.API.Common;

public static class ControllerHelper
{
    public static async Task<IActionResult> ExecuteAsync<T>(
        Func<Task<T>> action,
        ILogger logger,
        ControllerBase controller,
        string successMessage = "Operation completed successfully.")
    {
        try
        {
            var result = await action();
            var response = ApiResponse<T>.SuccessResponse(successMessage, result);
            return controller.Ok(response);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred during request processing.");

            var errorResponse = ApiResponse<T>.ErrorResponse(ex.Message, new List<string> { ex.InnerException?.Message ?? ex.Message });
            return controller.BadRequest(errorResponse);
        }
    }

    public static async Task<IActionResult> ExecuteAsync(
        Func<Task> action,
        ILogger logger,
        ControllerBase controller,
        string successMessage = "Operation completed successfully.")
    {
        try
        {
            await action();
            var response = ApiResponse<string>.SuccessResponse(successMessage, null);
            return controller.Ok(response);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred during request processing.");

            var errorResponse = ApiResponse<string>.ErrorResponse(ex.Message, new List<string> { ex.InnerException?.Message ?? ex.Message });
            return controller.BadRequest(errorResponse);
        }
    }
}

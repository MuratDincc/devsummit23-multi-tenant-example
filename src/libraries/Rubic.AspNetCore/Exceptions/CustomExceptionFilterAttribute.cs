using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System.Net;

namespace Rubic.AspNetCore.Exceptions;

public class CustomExceptionFilterAttribute : ExceptionFilterAttribute
{
    public const string LogRequestOnExceptionKey = "LogRequestOnException";
    private const string ErrorMessage = "global.error";

    private readonly ILogger<CustomExceptionFilterAttribute> _logger;

    public CustomExceptionFilterAttribute(ILogger<CustomExceptionFilterAttribute> logger)
    {
        _logger = logger;
    }

    public override async Task OnExceptionAsync(ExceptionContext context)
    {
        var exception = context.Exception;

        var status = HttpStatusCode.InternalServerError;

        string message;
        var messageArgs = Array.Empty<object>();
        var code = string.Empty;

        IEnumerable<ErrorResultDetail> details = null;

        switch (exception)
        {
            case StatusException statusException:
                status = statusException.Status.ToHttpStatusCode();
                code = statusException.Code;
                details = statusException.Details;
                message = exception.Message;
                messageArgs = statusException.MessageArgs;
                break;
            default:
                message = exception != null ? exception.ToString() : ErrorMessage;
                break;
        }

        // await LogAsync(exception, context);

        context.HttpContext.Response.StatusCode = (int)status;

        context.Result = new ObjectResult(new RawErrorResult
        {
            Message = message,
            MessageArgs = messageArgs,
            Code = code,
            Details = details
        });
    }

    private async Task LogAsync(Exception exception, ExceptionContext context)
    {
        var request = context.HttpContext?.Request;

        var message = $"{exception.Message} {0} {1}";

        var args = new Dictionary<string, string>
        {
            {"RequestPath", request.Path},
            {"RequestQueryString", request.QueryString.Value ?? string.Empty}
        };

        if (context.HttpContext.Items.TryGetValue(LogRequestOnExceptionKey, out var enabled) && (bool)(enabled ?? false))
        {
            var method = request?.Method;
            if (method == HttpMethods.Post || method == HttpMethods.Put || method == HttpMethods.Patch)
            {
                try
                {
                    var requestBody = await GetRequestBodyAsync(context);

                    args.Add("RequestBody", requestBody);

                    LogByExceptionType(exception, context, $"{message} {2}", args);

                    return;
                }
                catch (Exception e)
                {
                    _logger.LogWarning(e, "Something went wrong while logging the request body.");
                }
            }
        }

        LogByExceptionType(exception, context, message, args);
    }

    private void LogByExceptionType(Exception exception, ExceptionContext context, string message, params object[] args)
    {
        if (context.HttpContext.Response.StatusCode == StatusCodes.Status500InternalServerError)
            _logger.LogError(new EventId(), exception, message, args);
        else
            _logger.LogWarning(new EventId(), exception, message, args);
    }

    private async Task<string> GetRequestBodyAsync(ExceptionContext context)
    {
        var stream = context.HttpContext.Request.Body;

        if (stream.CanSeek)
            stream.Seek(0, SeekOrigin.Begin);

        using var reader = new StreamReader(stream);
        return await reader.ReadToEndAsync();
    }
}

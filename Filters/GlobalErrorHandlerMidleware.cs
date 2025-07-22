
using Azure;
using Examination_System.Common;
using Examination_System.Exceptions;
using System.Diagnostics;
using System.Text.Json;
using Examination_System.Localizations;
namespace Examination_System.Filters
{
    public class GlobalErrorHandlerMidleware : IMiddleware
    {
        private readonly ILogger<GlobalErrorHandlerMidleware> _logger;
        private readonly IWebHostEnvironment _environment;
        private readonly SharedResources _localization;

        public GlobalErrorHandlerMidleware(
            ILogger<GlobalErrorHandlerMidleware> logger,
            IWebHostEnvironment environment,
            SharedResources localization)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _environment = environment ?? throw new ArgumentNullException(nameof(environment));
            _localization = localization ?? throw new ArgumentNullException(nameof(localization));
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            // Get the unique trace identifier for this request
            var traceId = context.TraceIdentifier;

            try
            {
                // Log the start of the request with trace ID
                _logger.LogInformation($"Request started. TraceId: {traceId}, Path: {context.Request.Path}, Method: {context.Request.Method}");

                await next(context);

                // Log successful completion
                _logger.LogInformation($"Request completed successfully. TraceId: {traceId}, StatusCode: {context.Response.StatusCode}");
            }
            catch (BaseAppException appEx)
            {
                await HandleApplicationExceptionAsync(context, appEx, traceId);
            }
            catch (UnauthorizedAccessException unauthorizedEx)
            {
                await HandleUnauthorizedExceptionAsync(context, unauthorizedEx, traceId);
            }
            catch (Exception ex)
            {
                await HandleUnexpectedExceptionAsync(context, ex, traceId);
            }
        }

        private async Task HandleApplicationExceptionAsync(HttpContext context, BaseAppException exception, string traceId)
        {
            var logLevel = exception is BusinessLogicException ? LogLevel.Warning : LogLevel.Error;

            // Include trace ID in the log message
            _logger.Log(
                logLevel,
                exception,
                $"Application Exception. TraceId: {traceId}, Message: {exception.Message}, ErrorCode: {exception.ErrorCode}, RequestPath: {context.Request.Path}");

            // Include trace ID in response only in development mode
            var response = GeneralResponse<bool>.Response(
                data: false,
                Message: exception.Message,
                isSuccess: false,
                ErrorCode: exception.ErrorCode.ToString());

            await WriteErrorResponseAsync(context, response,exception.StatusCode, traceId);
        }

        private async Task HandleUnauthorizedExceptionAsync(HttpContext context, UnauthorizedAccessException exception, string traceId)
        {
            // Include trace ID in the log message
            _logger.LogWarning(
                exception,
                "Unauthorized Access Exception. TraceId: {TraceId}, Message: {Message}, RequestPath: {RequestPath}",
                traceId,
                exception.Message,
                context.Request.Path);

           // var response = GeneralResponse<bool>.Response(
             //   _localization.AccessDenied,
               // ErrorCode.UnauthorizedAccess,
                //_environment.IsDevelopment() ? traceId : null);


            var response = GeneralResponse<bool>.Response(
                data : false,
                Message: _localization.AccessDenied,
                isSuccess: false,
                ErrorCode: ErrorCode.UnauthorizedAccess.ToString());

            await WriteErrorResponseAsync(context, response, StatusCodes.Status403Forbidden, traceId);
        }

        private async Task HandleUnexpectedExceptionAsync(HttpContext context, Exception exception, string traceId)
        {
            // Include trace ID in the log message
            _logger.LogError(
                exception,
                "Unexpected error occurred. TraceId: {TraceId}, RequestPath: {RequestPath}",
                traceId,
                context.Request.Path);

            // var response = GeneralResponse<bool>.Response(
            //   _environment.IsDevelopment()
            //     ? $"An unexpected error occurred. TraceId: {traceId}"
            //   : _localization.UnexpectedError,
            //ErrorCode.InternalServerError,
            //_environment.IsDevelopment() ? traceId : null);

            var response = GeneralResponse<bool>.Response(
               data: false,
               Message: _localization.UnexpectedError,
               isSuccess: false,
               ErrorCode: ErrorCode.UnauthorizedAccess.ToString());

            await WriteErrorResponseAsync(context, response, StatusCodes.Status500InternalServerError, traceId);
        }

        private async Task WriteErrorResponseAsync<T>(HttpContext context, GeneralResponse<T> response, int statusCode, string traceId)
        {
            context.Response.StatusCode = statusCode;
            context.Response.ContentType = "application/json";

            // Add trace ID to response headers for debugging
            context.Response.Headers.Add("X-Trace-Id", traceId);

            var jsonOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = _environment.IsDevelopment()
            };

            await context.Response.WriteAsJsonAsync(response, jsonOptions);
        }
    }
}

using System.Text;
using BlogHung.Infrastructure.Logging;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace BlogHung.Infrastructure.Hosting.Middlewares
{
    /// <summary>
    /// 
    /// </summary>
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly List<string> allowedContentType = new List<string>() { "application/json", "text/plain" };

        /// <summary>
        /// 
        /// </summary>
        /// <param name="next"></param>
        public LoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Invoke(HttpContext context)
        {
            try
            {
                if (!context.Request.Headers.TryGetValue("CorrelationId", out var requestId))
                {
                    requestId = Guid.NewGuid().ToString();
                    context.Request.Headers.Add("CorrelationId", requestId);
                }
                LoggingHelper.SetProperty("CorrelationId", requestId.ToString());
                LoggingHelper.SetProperty("RequestTime", DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff"));
                // Lấy địa chỉ IP của client từ HttpContext
                string? ipAddress = context?.Connection?.RemoteIpAddress?.ToString();
                LoggingHelper.SetProperty("IpAddress", ipAddress ?? string.Empty);

                if (context.Request.ContentType != null &&
                    allowedContentType.Any(x => context.Request.ContentType.Trim().Replace(" ", "").Contains(x, StringComparison.OrdinalIgnoreCase)))
                {
                    var requestBodyContent = await ReadRequestBody(context.Request);

                    var requestBodyObject = JsonConvert.DeserializeObject<dynamic>(requestBodyContent);
                    LoggingHelper.SetProperty("Parameter", JsonConvert.SerializeObject(requestBodyObject));

                    var originalBodyStream = context.Response.Body;
                    using (var responseBody = new MemoryStream())
                    {
                        var response = context.Response;
                        response.Body = responseBody;

                        await _next(context);

                        var responseBodyContent = await ReadResponseBody(response);
                        await responseBody.CopyToAsync(originalBodyStream);
                        //LoggingHelper.SetProperty("ResponseData", responseBodyContent);
                    }
                }
                else
                {
                    await _next(context);
                }
                LoggingHelper.SetProperty("ResponseTime", DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff"));
                LoggingHelper.SetProperty("CustomLog", LoggingHelper.GetLogStep());
            }
            catch
            {
                await _next(context);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private async Task<string> ReadRequestBody(HttpRequest request)
        {
            request.EnableBuffering();
            string bodyText = string.Empty;
            using (var reader = new StreamReader(
            request.Body,
            encoding: Encoding.UTF8,
            detectEncodingFromByteOrderMarks: false,
            leaveOpen: true))
            {
                var body = await reader.ReadToEndAsync();
                bodyText = body;
                request.Body.Position = 0;
            }

            return await Task.FromResult(bodyText);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        private async Task<string> ReadResponseBody(HttpResponse response)
        {
            response.Body.Seek(0, SeekOrigin.Begin);
            var bodyAsText = await new StreamReader(response.Body).ReadToEndAsync();
            response.Body.Seek(0, SeekOrigin.Begin);

            return bodyAsText;
        }
    }
}

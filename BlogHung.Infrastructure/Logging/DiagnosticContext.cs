using Microsoft.AspNetCore.Http;
using Serilog;

namespace BlogHung.Infrastructure.Logging
{
    /// <summary>
    /// 
    /// </summary>
    public static class DiagnosticContext
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="diagnosticContext"></param>
        /// <param name="httpContext"></param>
        public static void EnrichFromRequest(
            IDiagnosticContext diagnosticContext, HttpContext httpContext)
        {
            try
            {
             /*   string accountName = string.Empty;
                try
                {
                    accountName = CoreUtility.GetAccountName()?.ToLower() ?? "None";
                }
                catch
                {
                    accountName = "None";
                }*/

                var request = httpContext.Request;
                diagnosticContext.Set("Host", request.Host);
                diagnosticContext.Set("Source", "1");
                diagnosticContext.Set("timestamp", DateTimeOffset.Now.ToString("O")); // Chỉnh đổi trường "DateTime" thành "@timestamp"
                diagnosticContext.Set("DateTime", DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"));
                diagnosticContext.Set("SourceType", "USER-LOG");
                diagnosticContext.Set("Protocol", request.Protocol);
                diagnosticContext.Set("Scheme", request.Scheme);
                if (request.QueryString.HasValue) diagnosticContext.Set("QueryString", request.QueryString.Value);
                diagnosticContext.Set("ContentType", httpContext.Response.ContentType ?? "None");
                //diagnosticContext.Set("User", accountName.ToLower());

                var HeaderAuthorization = "Authorization";
                string[] arr = {
                    HeaderAuthorization.ToLower()
                };
                diagnosticContext.Set("Header", request.Headers.Where(x => Array.IndexOf(arr, x.Key.ToLower()) > -1));

                var endpoint = httpContext.GetEndpoint();
                if (endpoint is object)
                {
                    var temp = endpoint.DisplayName.IndexOf("(");
                    string folder = temp > 0 ? endpoint.DisplayName.Substring(0, temp).Trim() : endpoint.DisplayName;
                    diagnosticContext.Set("LogFolder", folder.ToLower());
                    diagnosticContext.Set("EndpointName", httpContext.Request.Path);
                }
            }
            catch (Exception ex)
            {
                string temp = string.Empty;
                try
                {
                    diagnosticContext.Set("ErrorLogging", ex.ToString());
                }
                catch (Exception ex2)
                {
                    temp = ex2.ToString();
                }
            }
        }
    }
}

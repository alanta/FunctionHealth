using System.Threading.Tasks;
using System.Threading;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;

namespace FunctionHealth
{
    public class HealthCheck
    {
        private readonly HealthCheckService _healthCheckService;

        public HealthCheck(HealthCheckService healthCheckService)
        {
            _healthCheckService = healthCheckService;
        }

        [FunctionName(nameof(Health))]
        public async Task<IActionResult> Health(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)]
            HttpRequest req, CancellationToken cancellationToken, ILogger log)
        {
            var report = await _healthCheckService.CheckHealthAsync(_ => true, cancellationToken);

            int statusCode;
            switch (report.Status)
            {
                case HealthStatus.Healthy:
                    statusCode = 200;
                    break;
                default:
                    statusCode = 503;
                    break;
            }

            // prevent caching of responses
            var headers = req.HttpContext.Response.Headers;
            headers[HeaderNames.CacheControl] = "no-store, no-cache";
            headers[HeaderNames.Pragma] = "no-cache";
            headers[HeaderNames.Expires] = "Thu, 01 Jan 1970 00:00:00 GMT";

            return new ContentResult()
            {
                Content = report.Status.ToString(),
                ContentType = "text/plain",
                StatusCode = statusCode
            };
        }
    }
}

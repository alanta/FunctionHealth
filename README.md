# Health checks for Azure Functions

Health checks don't work in Azure functions, right? Well they do, kinda.
You can use the many Healthchecks available for ASP.NET Core with functions using a little hack and by replacing the [HealthCheckMiddleware](https://github.com/aspnet/Diagnostics/blob/master/src/Microsoft.AspNetCore.Diagnostics.HealthChecks/HealthCheckMiddleware.cs) with a function.

## Warranty

None. We're relying on ASP.NET internals to make this work.
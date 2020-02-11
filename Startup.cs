using FunctionHealth;
using System.IO;
using System.Reflection;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;

[assembly: FunctionsStartup(typeof(Startup))]

namespace FunctionHealth
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddHealthChecks()
                .ForFunctions()
                .AddCheck("Example", () => HealthCheckResult.Healthy("Example is OK!"), tags: new[] {"example"});
        }
    }
}
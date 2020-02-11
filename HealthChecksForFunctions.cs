using FunctionHealth;
using System.Linq;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(Startup))]

namespace FunctionHealth
{
    public static class HealthChecksForFunctions
    {
        public static IHealthChecksBuilder ForFunctions(this IHealthChecksBuilder builder)
        {
            builder.Services.Remove(builder.Services.First(d =>
                d.ImplementationType != null && d.ImplementationType.Name == "HealthCheckPublisherHostedService"));

            return builder;
        }
    }
}
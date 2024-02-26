using Serilog.Context;

namespace Dotnet.Sales.WebApi
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseSwagger(this IApplicationBuilder app, IHostEnvironment hostingEnvironment)
        {
            if (hostingEnvironment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint($"/swagger/v1/swagger.json", $"v1");
                    c.SwaggerEndpoint($"/swagger/v2/swagger.json", $"v2");
                });
            }
            return app;
        }

        public static IApplicationBuilder UseCorrelationIdLogger(this IApplicationBuilder app)
        {
            return app.Use(async (context, next) =>
            {
                context.Request.Headers.TryGetValue("X-Correlation-Id", out var correlationIds);
                var correlationId = correlationIds.FirstOrDefault() ?? Guid.NewGuid().ToString();
                using (LogContext.PushProperty("CorrelationId", correlationId))
                {
                    await next();
                }
            });
        }
    }
}

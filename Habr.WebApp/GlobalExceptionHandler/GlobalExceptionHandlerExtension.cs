namespace Habr.WebApp.GlobalExceptionHandler
{
    public static class GlobalExceptionHandlerExtension
    {
        public static IServiceCollection AddGlobalExceptionHandler(this IServiceCollection services)
        {
            services.AddSingleton<IExceptionMapper, ExceptionToProblemDetailsMapper>();
            services.AddExceptionHandler<DefaultGlobalExceptionHandler>();

            return services;
        }

        public static IApplicationBuilder UseGlobalExceptionHandler(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
            return app;
        }
    }
}

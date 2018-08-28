using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Mvc.Navigation.Providers;

namespace Mvc.Navigation
{
    public static class ApplicationBuilder
    {

        public static IServiceCollection AddNavigation(this IServiceCollection services)
        {
            services.AddSingleton<TreeIndex>();
            services.AddTransient<TreeConstructor>();

            services.AddTransient<ITreeBuilder, DefaultMvcTreeBuilder>();

            services.AddTransient<NavigationProvider>();
            services.AddTransient<BreadcrumbProvider>();
            services.AddTransient<SitemapProvider>();

            return services;
        }

        public static IApplicationBuilder UseNavigation(this IApplicationBuilder app)
        {
            var treeConstructor = app.ApplicationServices.GetService<TreeConstructor>();

            treeConstructor.Build();

            return app;
        }
    }
}

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Mvc.Navigation.Builders;
using Mvc.Navigation.Filters;
using Mvc.Navigation.Helpers;
using Mvc.Navigation.Providers;

namespace Mvc.Navigation
{
    public static class ApplicationBuilder
    {

        public static IServiceCollection AddNavigation(this IServiceCollection services)
        {
            services.AddSingleton<TreeIndex>();
            services.AddTransient<ITreeManager, TreeManager>();

            services.AddTransient<ITreeBuilder, DefaultMvcTreeBuilder>();

            services.AddTransient<INavigationFilter, AuthenticationFilter>();

            services.AddTransient<INavigationProvider, NavigationProvider>();
            services.AddTransient<IBreadcrumbProvider, BreadcrumbProvider>();
            services.AddTransient<ISitemapProvider, SitemapProvider>();

            services.AddSingleton<ActivePathHelper>();

            return services;

            /*
             *
             * c<HomeController>(a=> a.Index)
             *  .Children(
             *      a=> a.Child1,
             *      a=> a.Child2,
             * )
             *
             */
        }

        public static IApplicationBuilder UseNavigation(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var treeConstructor = scope.ServiceProvider.GetService<ITreeManager>();

                treeConstructor.Build();
            }

            return app;
        }
    }
}

using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Mvc.Navigation.Demo
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddHttpContextAccessor();
            services.AddNavigation();
            services.AddSingleton<ITreeBuilder, MyClass>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc();

            app.UseNavigation();
        }

        public class MyClass :ITreeBuilder
        {
            public IEnumerable<TreeElement> GetElements()
            {
                yield return new TreeElement
                {
                    Name = "Home",
                    Path = "/home",
                    Children = new[]
                    {
                        new TreeElement
                        {
                            Name = "test",
                            Path = "/test"
                        }
                    }
                };

                yield return new TreeElement
                {
                    Name = "Contact",
                    Path = "/contact",
                    Children = new[]
                    {
                        new TreeElement
                        {
                            Name = "Support",
                            Path = "/contact/support"
                        }
                    }
                };
            }
        }
    }
}

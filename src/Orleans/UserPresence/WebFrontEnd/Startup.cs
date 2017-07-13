using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Owin.Builder;
using Orleans;
using Orleans.Runtime.Configuration;
using Owin;


namespace WebFrontEnd
{
    using AppFunc = Func<IDictionary<string, object>, Task>;

    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc();

            ClientConfiguration clientConfig = ClientConfiguration.LocalhostSilo();
            IClusterClient client = new ClientBuilder().UseConfiguration(clientConfig).Build();
            client.Connect().Wait();

            services.AddSingleton(client);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseMvc();

            // This is connection point of SignalR using OWIN
            // We'll remove this approach for now, using just REST for signaling user presence
            //app.UseSignalR();
        }
    }


    //public static class AppBuilderExtensions
    //{
    //    public static IApplicationBuilder UseAppBuilder(this IApplicationBuilder app, Action<IAppBuilder> configure)
    //    {
    //        app.UseOwin(addToPipeline =>
    //        {
    //            addToPipeline(next =>
    //            {
    //                var appBuilder = new AppBuilder();
    //                appBuilder.Properties["builder.DefaultApp"] = next;

    //                configure(appBuilder);

    //                return appBuilder.Build<AppFunc>();
    //            });
    //        });

    //        return app;
    //    }

    //    public static void UseSignalR(this IApplicationBuilder app)
    //    {
    //        app.UseAppBuilder(appBuilder => appBuilder.MapSignalR());
    //    }
    //}
}

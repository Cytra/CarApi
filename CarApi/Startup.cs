using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using CarApi.Core.Services;
using CarApi.Middlewares;
using CarApi.Model;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Converters;
using Swashbuckle.AspNetCore.Filters;


namespace CarApi
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public AppSettings AppSettings { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            AppSettings = configuration.GetSection("AppSettings").Get<AppSettings>();
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IAutoPliusService, AutoPliusService>();
            services.AddScoped<IAutoPliusProvider, AutoPliusProvider>();
            services.AddHttpClient("autoPlius", client =>
            {
                client.BaseAddress = new Uri("https://m.autoplius.lt");
                client.Timeout = new TimeSpan(0, 0, 30);
                client.DefaultRequestHeaders.Clear();
            });

            services.AddOptions<AppSettings>().Bind(Configuration.GetSection("AppSettings"));

            services.AddControllers()
                .AddNewtonsoftJson(opt =>
                {
                    opt.SerializerSettings.Converters.Add(new StringEnumConverter());
                });
            services.AddSwaggerGen(c =>
            {
                c.ExampleFilters();
            });
            services.AddSwaggerExamplesFromAssemblyOf<Startup>();

            services.AddCors(options => options.AddPolicy("AllowEverything", builder => builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()));

            services.AddHealthChecks();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
            app.UseHealthChecks("/healthz");
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Cars");
            });
        }
    }
}

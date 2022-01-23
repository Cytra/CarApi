using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarApi.Data.Contexts;
using Microsoft.Extensions.Configuration;
using Stocks.Middleware;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.EntityFrameworkCore;


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

            //Log.Logger = new LoggerConfiguration()
            //    .MinimumLevel.Warning()
            //    .WriteTo.MicrosoftTeams(AppSettings.MsTeamsWebHook)
            //    .CreateLogger();
        }
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddHttpClient("highInterest", client =>
            //{
            //    client.BaseAddress = new Uri("https://highshortinterest.com");
            //    client.Timeout = new TimeSpan(0, 0, 30);
            //    client.DefaultRequestHeaders.Clear();
            //});

            services.AddOptions<AppSettings>().Bind(Configuration.GetSection("AppSettings"));

            services.AddDbContext<CarContext>(options =>
            {
                options.UseSqlServer(AppSettings.SqlConnectionString,
                    options => options.EnableRetryOnFailure());
            });


            services.AddSwaggerGen(c =>
            {
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMiddleware<ExceptionMiddleware>();
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Car API");
            });
        }
    }
}

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using CarApi.Core.Services;
using CarApi.Data.Contexts;
using CarApi.Data.Repositories;
using Microsoft.Extensions.Configuration;
using Stocks.Middleware;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
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

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddOptions<AppSettings>().Bind(Configuration.GetSection("AppSettings"));

            services.AddDbContext<CarContext>(options =>
            {
                options.UseSqlServer(AppSettings.SqlConnectionString,
                    options => options.EnableRetryOnFailure());
            });

            services.AddControllers()
                .AddNewtonsoftJson(opt =>
                {
                    //opt.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.RoundtripKind;
                    //opt.SerializerSettings.Formatting = Formatting.None;
                    //opt.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                    opt.SerializerSettings.Converters.Add(new StringEnumConverter());
                    //JsonConvert.DefaultSettings = () => opt.SerializerSettings;
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
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
            //app.UseCors("AllowEverything");
            //app.UseHttpsRedirection();
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

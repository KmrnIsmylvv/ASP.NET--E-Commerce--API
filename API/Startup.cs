using API.Extensions;
using API.Helpers;
using API.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace API
{
    public class Startup
    {
        private readonly IConfiguration _config;

        public Startup(IConfiguration config)
        {
            _config = config;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MappingProfiles));

            services.AddControllers();

            services.AddApplicationServices(_config);
            services.AddSwaggerDocumentation();
            services.AddIdentityService(_config);

            services.AddSingleton<IConnectionMultiplexer>(c =>
            {
                var configuration = ConfigurationOptions.Parse(
                    _config.GetConnectionString("Redis"), true);

                return ConnectionMultiplexer.Connect(configuration);
            });

           
                
            services.AddCors(opt =>
            {
                opt.AddPolicy("CorsPolicy",
                    policy => { policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:4200"); });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMiddleware<ExceptionMiddleware>();

            app.UseStatusCodePagesWithReExecute("/errors/{0}");

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseStaticFiles();

            app.UseCors("CorsPolicy");

            app.AddSwaggerUI();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}
using Brukerfeil.Enode.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Brukerfeil.Enode.Common.Repositories;
using Brukerfeil.Enode.Services;
using Brukerfeil.Enode.Common.Services;
using Brukerfeil.Enode.API.Configurations;

namespace Brukerfeil.Enode.API
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
            services.AddControllers();
            services.AddMemoryCache();
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    builder =>
                    {
                        builder.WithOrigins("http://localhost:3000")
                                            .AllowAnyOrigin()
                                            .AllowAnyHeader()
                                            .AllowAnyMethod()
                                            .AllowAnyOrigin();
                    });
            });

            services.AddSingleton(ConfigProvider.Instance);
            services.AddTransient<ISortingService, SortingService>();
            services.AddTransient<IMessagesService, MessagesService>();
            services.AddTransient<IConfigService, ConfigService>();
            services.AddHttpClient<IElementsMessageRepository, ElementsMessageRepository>();
            services.AddHttpClient<IDifiMessageRepository, DifiMessageRepository>();
            services.AddTransient<IMessageMergeService, MessageMergeService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

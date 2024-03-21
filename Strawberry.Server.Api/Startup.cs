using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Strawberry.Server.Api.Helpers;
using Strawberry.Server.Database;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;

namespace Strawberry.Server.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            DatabaseContext.ConnectionString = configuration.GetConnectionString("MySql");
            SMSHelper.Init("doctoryoomin", "deb687deaee0712e3e945e1f24186b9d", "02-858-1588");
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Strawberry.Server.Api", Version = "v1" });
            });
            services.AddDbContext<DatabaseContext>();
            services.AddSingleton<FirebaseHelper>();
            services.AddSingleton<ImageHelper>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IHostApplicationLifetime hostApplicationLifetime, DatabaseContext db)
        {
            // ��ȭ�� ����
            {
                var cultureInfo = new CultureInfo("ko-KR");
                CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
                CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;
            }


            /// ���� ���� �� ���� �̺�Ʈ ���
            {
                hostApplicationLifetime.ApplicationStarted.Register(ServerStarted);
                hostApplicationLifetime.ApplicationStopped.Register(ServerStopped);
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseStaticFiles();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Strawberry.Server.Api v1"));
            db.Initialize();
        }

        private void ServerStarted()
        {
            ServerTimerHelper.Start();
        }

        public void ServerStopped()
        {
            ServerTimerHelper.Stop();
        }
    }
}

using Hangfire;
using Hangfire.Console;
using Hangfire.PostgreSql;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Localization;
using Repres.Application.Extensions;
using Repres.Infrastructure.Extensions;
using Repres.Infrastructure.Jobs;
using Repres.Server.Extensions;
using Repres.Server.Filters;
using Repres.Server.Managers.Preferences;
using Repres.Server.Middlewares;
using System;
using System.IO;
using System.Threading;

namespace Repres.Server
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private readonly IConfiguration _configuration;

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddSignalR();
            services.AddLocalization(options =>
            {
                options.ResourcesPath = "Resources";
            });
            services.AddCurrentUserService();
            services.AddSerialization();
            services.AddDatabase(_configuration);
            services.AddServerStorage(); //TODO - should implement ServerStorageProvider to work correctly!
            services.AddScoped<ServerPreferenceManager>();
            services.AddServerLocalization();
            services.AddIdentity();
            services.AddJwtAuthentication(services.GetApplicationSettings(_configuration));
            services.AddApplicationLayer();
            services.AddApplicationServices();
            services.AddRepositories();
            services.AddThirdPartyServices(_configuration);
            services.AddExtendedAttributesUnitOfWork();
            services.AddSharedInfrastructure(_configuration);
            services.RegisterSwagger();
            services.AddInfrastructureMappings();
            //services.AddHangfire(x => x.UseSqlServerStorage(_configuration.GetConnectionString("DefaultConnection")));
            services.AddHangfire(x =>
            {
#if DEBUG
                x.UsePostgreSqlStorage(_configuration.GetConnectionString("DefaultConnection")).WithJobExpirationTimeout(TimeSpan.FromDays(7));
#else
                x.UsePostgreSqlStorage(DatabaseUrlParser.Parse(Environment.GetEnvironmentVariable("DATABASE_URL"))).WithJobExpirationTimeout(TimeSpan.FromDays(7));
#endif
                x.UseConsole();
            });
            services.AddHangfireServer(opt => opt.WorkerCount = 2);
            services.AddControllers().AddValidators();
            services.AddExtendedAttributesValidators();
            services.AddExtendedAttributesHandlers();
            services.AddRazorPages().AddRazorRuntimeCompilation();
            services.AddApiVersioning(config =>
            {
                config.DefaultApiVersion = new ApiVersion(1, 0);
                config.AssumeDefaultVersionWhenUnspecified = true;
                config.ReportApiVersions = true;
            });
            services.AddLazyCache();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IStringLocalizer<Startup> localizer)
        {
            app.UseCors();
            app.UseExceptionHandling(env);
            //app.UseHttpsRedirection();
            app.UseMiddleware<ErrorHandlerMiddleware>();
            app.UseBlazorFrameworkFiles();
            app.UseStaticFiles();
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"Files")),
                RequestPath = new PathString("/Files")
            });
            app.UseRequestLocalizationByCulture();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseHangfireDashboard("/jobs", new DashboardOptions
            {
                DashboardTitle = localizer["BlazorHero Jobs"],
                Authorization = new[] { new HangfireAuthorizationFilter() }
            });
            app.UseEndpoints();
            app.ConfigureSwagger();
            app.Initialize(_configuration);

            RecurringJob.AddOrUpdate<ApiProccessExecution>("API Process Job", x => x.Execute(null, CancellationToken.None), "0 0 * * *");
            RecurringJob.AddOrUpdate<GoogleCalcExportExecution>("Google Cal Export Job", x => x.Execute(null, CancellationToken.None), "0 2 * * *");
            RecurringJob.AddOrUpdate<DatabasePurgeExecution>("Database Data Purge Job", x => x.Execute(null, CancellationToken.None), "0 4 * * *");
            RecurringJob.AddOrUpdate<PruneSummariesExecution>("Database Prune Job", x => x.Execute(null, CancellationToken.None), "0 0 5 31 2 ?");
            //RecurringJob.AddOrUpdate<DatabaseStatusExecution>("Database Status Job", x => x.Execute(null, CancellationToken.None), "0 0 5 31 2 ?");
            //RecurringJob.AddOrUpdate<HangfireResetExecution>("Hangfire Reset Job", x => x.Execute(null, CancellationToken.None), "0 0 1 * *");
        }
    }
}
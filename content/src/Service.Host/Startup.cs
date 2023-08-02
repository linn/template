namespace Linn.Template.Service.Host
{
    using System.IdentityModel.Tokens.Jwt;
    using System.IO;

    using Linn.Common.Authentication.Host.Extensions;
    using Linn.Common.Configuration;
    using Linn.Common.Logging;
    using Linn.Template.IoC;
    using Linn.Template.Service.Extensions;
    using Linn.Template.Service.Host.Negotiators;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Diagnostics;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.FileProviders;
    using Microsoft.Extensions.Hosting;

    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            services.AddCors();
            services.AddSingleton<IViewLoader, ViewLoader>();
            services.AddSingleton<IResponseNegotiator, UniversalResponseNegotiator>();
            services.AddCredentialsExtensions();
            services.AddSqsExtensions();
            services.AddLog();

            services.AddFacade();
            services.AddServices();
            services.AddPersistence();
            services.AddHandlers();
            services.AddMessageDispatchers();

            services.AddLinnAuthentication(
                options =>
                    {
                        options.Authority = ConfigurationManager.Configuration["AUTHORITY_URI"];
                        options.CallbackPath = new PathString("/template/signin-oidc");
                        options.CookiePath = "/template";
                    });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseStaticFiles(new StaticFileOptions
                                       {
                                           RequestPath = "/template/build",
                                           FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "client", "build"))
                                       });
            }
            else
            {
                app.UseStaticFiles(new StaticFileOptions
                                       {
                                           RequestPath = "/template/build",
                                           FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "app", "client", "build"))
                                       });
            }

            app.UseAuthentication();

            app.UseBearerTokenAuthentication();
            app.UseExceptionHandler(
                c => c.Run(async context =>
                    {
                        var exception = context.Features
                            .Get<IExceptionHandlerPathFeature>()
                            ?.Error;

                        var log = app.ApplicationServices.GetService<ILog>();
                        log.Error(exception?.Message, exception);

                        var response = new { error = $"{exception?.Message}  -  {exception?.StackTrace}" };
                        await context.Response.WriteAsJsonAsync(response);
                    }));
            app.UseRouting();
            app.UseEndpoints(builder => { builder.MapEndpoints(); });
        }
    }
}

namespace Linn.Template.Service.Host
{
    using System.IdentityModel.Tokens.Jwt;

    using Linn.Common.Authentication.Host.Extensions;
    using Linn.Common.Configuration;

    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Authentication.OpenIdConnect;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.HttpOverrides;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;

    using Nancy;
    using Nancy.Owin;

    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();

            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            services.AddLinnAuthentication(
                options =>
                    {
                        options.Authority = ConfigurationManager.Configuration["AUTHORITY_URI"];
                        options.CallbackPath = new PathString("/template/signin-oidc");
                    });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();

            app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseForwardedHeaders(new ForwardedHeadersOptions
                                        {
                                            ForwardedHeaders = ForwardedHeaders.XForwardedProto
                                        });

            app.UseAuthentication();

            app.UseBearerTokenAuthentication();

            app.UseOwin(x => x.UseNancy(
                config =>
                    {
                        config.PassThroughWhenStatusCodesAre(HttpStatusCode.Unauthorized, HttpStatusCode.Forbidden);
                    }));

            app.Use((context, next) => context.ChallengeAsync(OpenIdConnectDefaults.AuthenticationScheme));
        }
    }
}

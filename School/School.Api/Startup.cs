﻿using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Microsoft.Practices.Unity;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Owin;
using School.Api.Helpers;
using School.Api.Security;
using School.Business.Services;
using School.Data.DataContexts;
using School.Data.Repositories;
using School.Domain.Contracts.Repositories;
using School.Domain.Contracts.Services;
using School.Domain.Models;
using System;
using System.Web.Http;

namespace School.Api
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();

            // Configure Dependency Injection
            var container = new UnityContainer();
            container.RegisterType<SchoolContext, SchoolContext>();

            container.RegisterType<ICourseRepository, CourseRepository>();
            container.RegisterType<IPersonRepository, PersonRepository>();
            container.RegisterType<IUserRepository, UserRepository>();

            container.RegisterType<ICourseService, CourseService>();
            container.RegisterType<IPersonService, PersonService>();
            container.RegisterType<IUserService, UserService>();

            config.DependencyResolver = new UnityResolver(container);

            ConfigureWebApi(config);
            ConfigureOAuth(app, container.Resolve<IUserService>());

            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            app.UseWebApi(config);
        }

        public static void ConfigureWebApi(HttpConfiguration config)
        {
            // Removes XML
            var formatters = config.Formatters;
            formatters.Remove(formatters.XmlFormatter);

            // JSON Ident
            var jsonSettings = formatters.JsonFormatter.SerializerSettings;
            jsonSettings.Formatting = Formatting.Indented;
            jsonSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            // Changes serialization
            formatters.JsonFormatter.SerializerSettings.PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.Objects;

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }

        public void ConfigureOAuth(IAppBuilder app, IUserService userService)
        {
            OAuthAuthorizationServerOptions OAuthServerOptions = new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/api/auth"),
                AccessTokenExpireTimeSpan = TimeSpan.FromHours(1),
                Provider = new AuthorizationServerProvider(userService),
                RefreshTokenProvider = new TokenProvider(TokenType.RefreshToken, userService)
            };

            // Token Generation
            app.UseOAuthAuthorizationServer(OAuthServerOptions);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
        }
    }
}
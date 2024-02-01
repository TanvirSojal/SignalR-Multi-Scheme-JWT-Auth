using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using SignalRServer.Events;
using SignalRServer.Handlers;

namespace SignalRServer.Extensions
{
    public static class AuthenticationExtensions
    {
        public static IServiceCollection AddCustomAuthentications(this IServiceCollection services)
        {
            // Reference: https://albertromkes.com/2022/12/09/support-multiple-jwt-authorities-in-a-net-core-application/

            var combinedScheme = "HELLO_OR_WORLD";

            var helloScheme = "Hello";
            var worldScheme = "World";

            services
                .AddAuthentication(combinedScheme) // instead of setting any one scheme as default, we create a combined scheme
                .AddScheme<JwtBearerOptions, HelloJwtBearerHandler>(helloScheme, options =>
                {
                    options.Authority = "hello_authority";
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.ASCII.GetBytes("qwertyuiopasdfghjklzxcvbnm123456")),

                        ValidateIssuer = false,
                        ValidateLifetime = false,
                        ValidateAudience = false,
                    };

                    options.Events = new CustomJwtBearerEvents("Hello");
                })
                .AddScheme<JwtBearerOptions, WorldJwtBearerHandler>(worldScheme, options =>
                {
                    options.Authority = "world_authority";
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.ASCII.GetBytes("qwertyuiopasdfghjklzxcvbnm123456")),

                        ValidateIssuer = false,
                        ValidateLifetime = false,
                        ValidateAudience = false,
                    };

                    options.Events = new CustomJwtBearerEvents("World");
                })
                .AddPolicyScheme(combinedScheme, combinedScheme, options =>
                {
                    var fallbackScheme = "World";

                    options.ForwardDefaultSelector = context =>
                    {
                        // here we need to decide which scheme to use based on something. Authority (?)

                        // Return fallback scheme if first scheme is not appropriate
                        return fallbackScheme; // forcefully returning fallback scheme so "Hello" is skipped
                    };
                });

            return services;
        }
    }
}

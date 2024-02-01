using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using SignalRServer.Handlers;
using SignalRServer.Models;

namespace SignalRServer.Extensions
{
    public static class AuthenticationExtensions
    {
        public static IServiceCollection AddCustomAuthentications(this IServiceCollection services)
        {
            // Reference: https://albertromkes.com/2022/12/09/support-multiple-jwt-authorities-in-a-net-core-application/

            var secretKey = "qwertyuiopasdfghjklzxcvbnm123456"; // used in both Hello and World

            services
                .AddAuthentication(AuthSchemes.HELLO_OR_WORLD_SCHEME) // instead of setting any one scheme as default, we create a combined scheme
                .AddScheme<JwtBearerOptions, HelloJwtBearerHandler>(AuthSchemes.HELLO_SCHEME, options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.ASCII.GetBytes(secretKey)),

                        ValidateIssuer = false,
                        ValidateLifetime = false,
                        ValidateAudience = false,
                    };
                })
                .AddScheme<JwtBearerOptions, WorldJwtBearerHandler>(AuthSchemes.WORLD_SCHEME, options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.ASCII.GetBytes(secretKey)),

                        ValidateIssuer = false,
                        ValidateLifetime = false,
                        ValidateAudience = false,
                    };
                })
                .AddPolicyScheme(AuthSchemes.HELLO_OR_WORLD_SCHEME, AuthSchemes.HELLO_OR_WORLD_SCHEME, options =>
                {
                    var fallbackScheme = AuthSchemes.WORLD_SCHEME;

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

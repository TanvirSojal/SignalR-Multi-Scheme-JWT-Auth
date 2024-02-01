using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using System.Text.Encodings.Web;

namespace SignalRServer.Handlers
{
    public class HelloJwtBearerHandler : JwtBearerHandler
    {
        public HelloJwtBearerHandler(IOptionsMonitor<JwtBearerOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock) : base(options, logger, encoder, clock)
        {
        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            return base.HandleAuthenticateAsync();
        }
    }
}

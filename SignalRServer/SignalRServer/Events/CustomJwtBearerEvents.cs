using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace SignalRServer.Events
{
    public class CustomJwtBearerEvents : JwtBearerEvents
    {
        public string Issuer { get; }

        public CustomJwtBearerEvents(string issuer)
        {
            Issuer = issuer;
        }

        public override Task MessageReceived(MessageReceivedContext context)
        {
            if (Issuer == "Hello")
            {
                context.NoResult();
            }

            return Task.CompletedTask;
        }
    }
}

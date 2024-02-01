using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR.Client;

namespace SignalRClient.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConnectionController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> CreateConnection()
        {
            var jwt = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJpc3MiOiJPbmxpbmUgSldUIEJ1aWxkZXIiLCJpYXQiOjE3MDY3NzkwNzAsImV4cCI6MTczODMxNTA3MCwiYXVkIjoid3d3LmV4YW1wbGUuY29tIiwic3ViIjoianJvY2tldEBleGFtcGxlLmNvbSIsIkdpdmVuTmFtZSI6IkpvaG5ueSIsIlN1cm5hbWUiOiJSb2NrZXQiLCJFbWFpbCI6Impyb2NrZXRAZXhhbXBsZS5jb20iLCJSb2xlIjpbIk1hbmFnZXIiLCJQcm9qZWN0IEFkbWluaXN0cmF0b3IiXX0.IGLtekt0X6erGQqFDsJdSjEFrdKKLAafLW46ocU-1hM";
            
            var connection = new HubConnectionBuilder()
                .WithUrl("https://localhost:44325/Message", option =>
                {
                    option.AccessTokenProvider = async () => await Task.FromResult(jwt);
                })
                .Build();

            await connection.StartAsync();

            await connection.InvokeAsync("SendMessage", connection.ConnectionId, "Hello World");

            return Ok();
        }
    }
}

using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace SingnalR_Example.SingnalRHub
{
  
    public class ItemHub : Hub
    {
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
    }

}

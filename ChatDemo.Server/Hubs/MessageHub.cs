using Microsoft.AspNetCore.SignalR;

namespace ChatDemo.Server.Hubs
{
    public class MessageHub : Hub
    {
        public Task SendMessage(string user, string message)
        {
            Console.WriteLine($"服务器接收到消息，内容:{user},{message}");
            return Clients.All.SendAsync("ReceiveMessage", user, message);
        }

        /// <summary>
        /// Called when a new connection is established with the hub.
        /// </summary>
        /// <returns>A <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous connect.</returns>
        public override Task OnConnectedAsync()
        {
            Console.WriteLine($"客户[{Context.ConnectionId}]连接成功.");
            return base.OnConnectedAsync();
        }
    }
}
